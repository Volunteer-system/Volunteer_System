using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer_WPF.Model
{
    class Manpower_apply_Model
    {
        //申請表ID
        public int Apply_ID { get; set; }
        //運用單位
        public string Application_unit { get; set; }
        //申請人
        public string Applicant { get; set; }
        //申請日期
        public string Apply_date { get; set; }
        //申請人電話
        public string Applicant_phone { get; set; }
        //值班地點
        public string Work_place { get; set; }
        //工作項目與流程
        public string Apply_description { get; set; }
        //單位主管
        public string unit_Supervisor { get; set; }
        //部門主管
        public string unit_heads { get; set; }
        //志工督導
        public string Supervision { get; set; }
        //評估日期
        public string Reply_date { get; set; }
        //評估建議
        public string Repply_description { get; set; }
        //志工督導主管
        public string Supervision_heads { get; set; }
        //申請人數
        public string Application_number { get; set; }
        //核定人數
        public string Reply_number { get; set; }

        public List<Manpower_apply_Model> SelectManpower_apply_byStage(string stage, string type, DateTime startdate, DateTime enddate)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n1 in dbContext.Manpower_apply
                    join n2 in dbContext.Application_unit
                    on n1.Application_unit_no equals n2.Application_unit_no
                    join n3 in dbContext.Stages
                    on n1.Apply_state equals n3.Stage_ID
                    where ((n3.Stage1 == stage) &&
                           ((type == "") ? true : n1.Apply_type == type) &&
                           ((startdate == DateTime.MinValue) ? true : n1.Apply_date >= startdate))
                    select new
                    {
                        Apply_ID = n1.Apply_ID,
                        Application_unit = n2.Application_unit1,
                        Applicant = n1.Applicant,
                        Apply_date = n1.Apply_date,
                        Applicant_phone = n1.Applicant_phone,
                        Work_place = n1.Work_place,
                        Apply_description = n1.Apply_description,
                        unit_Supervisor = n1.Application_unit_Supervisor,
                        unit_heads = n1.Application_unit_heads,
                        Reply_date = n1.Reply_date,
                        Repply_description = n1.Repply_description,
                        Application_number = n1.Application_number,
                        Reply_number = n1.Reply_number
                    };

            List<Manpower_apply_Model> Manpower_applys = new List<Manpower_apply_Model>();
            foreach (var row in q)
            {
                Manpower_apply_Model manpower_Apply_Model = new Manpower_apply_Model();
                manpower_Apply_Model.Apply_ID = row.Apply_ID;
                manpower_Apply_Model.Application_unit = row.Application_unit;
                manpower_Apply_Model.Applicant = row.Applicant;
                manpower_Apply_Model.Apply_date = row.Apply_date.ToString();
                manpower_Apply_Model.Applicant_phone = row.Applicant_phone;
                manpower_Apply_Model.Work_place = row.Work_place;
                manpower_Apply_Model.Apply_description = row.Repply_description;
                manpower_Apply_Model.unit_Supervisor = row.unit_Supervisor;
                manpower_Apply_Model.Application_number = row.Application_number.ToString();
                manpower_Apply_Model.Reply_number = row.Reply_number.ToString();

                Manpower_applys.Add(manpower_Apply_Model);
            }

            return Manpower_applys;
        }

        public void SelectManpower_apply_byApply_ID(int apply_ID)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n1 in dbContext.Manpower_apply
                    join n2 in dbContext.Application_unit   
                    on n1.Application_unit_no equals n2.Application_unit_no
                    join n3 in dbContext.Volunteer_supervision
                    on n1.Supervision_ID equals n3.supervision_ID
                    where n1.Apply_ID == apply_ID                           
                    select new
                    {
                        Apply_ID = n1.Apply_ID,
                        Application_unit = n2.Application_unit1,
                        Applicant = n1.Applicant,
                        Apply_date = n1.Apply_date,
                        Applicant_phone = n1.Applicant_phone,
                        Work_place = n1.Work_place,
                        Apply_description = n1.Apply_description,
                        unit_Supervisor = n1.Application_unit_Supervisor,
                        unit_heads = n1.Application_unit_heads,
                        Supervision = n3.supervision_Name,
                        Reply_date = n1.Reply_date,
                        Repply_description = n1.Repply_description,
                        Supervision_heads = n1.Supervision_heads,
                        Application_number = n1.Application_number,
                        Reply_number = n1.Reply_number
                    };
            foreach (var row in q)
            {
                Apply_ID = row.Apply_ID;
                Application_unit = row.Application_unit;
                Applicant = row.Applicant;
                Apply_date = row.Apply_date.ToString();
                Applicant_phone = row.Applicant_phone;
                Work_place = row.Work_place;
                Apply_description = row.Apply_description;
                unit_Supervisor = row.unit_Supervisor;
                unit_heads = row.unit_heads;
                Supervision = row.Supervision;
                Reply_date = row.Reply_date.ToString();
                Repply_description = row.Repply_description;
                Supervision_heads = row.Supervision_heads;
                Application_number = row.Application_number.ToString();
                Reply_number = row.Reply_number.ToString();
            }
        }

        public void UpdateManpower_apply(int apply_ID, int reply_number,string repply_description)
        {
            int processing = 0;
            int end_processing = 0;
            

            Apply_assessment_Model apply_Assessment_Model = new Apply_assessment_Model();
            List<string> Assessments = apply_Assessment_Model.SelectApply_assessment_byApply_ID(apply_ID);
            Apply_result_Model apply_Result_Model = new Apply_result_Model();
            List<string> Assessment_results = apply_Result_Model.SelectApply_result_byApply_ID(apply_ID);

            VolunteerEntities dbContext = new VolunteerEntities();

            var a = from n in dbContext.Stages
                    where n.Stage_type == "人力申請"
                    select new
                    {
                        Stage_ID = n.Stage_ID,
                        Stage = n.Stage1
                    };
            foreach (var row in a)
            {
                switch (row.Stage.ToString())
                {
                    case "處理中":
                        processing = row.Stage_ID;
                        break;
                    case "申請完成":
                        end_processing = row.Stage_ID;
                        break;
                }
            }

            var q = from n in dbContext.Manpower_apply
                    where n.Apply_ID == apply_ID
                    select n;
            foreach (var row in q)
            {
                row.Reply_number = reply_number;
                row.Repply_description = repply_description;
                if (reply_number > 0 &&
                    !string.IsNullOrEmpty(repply_description) &&
                    Assessments.Count() > 0 &&
                    Assessment_results.Count() > 0)
                {
                    row.Apply_state = end_processing;
                }
                else
                {
                    row.Apply_state = processing;
                }
            }

            dbContext.SaveChanges();
        }

        public void UpdateManpower_apply_byreject(int apply_ID)
        {
            int rejectapply = 0;

            VolunteerEntities dbContext = new VolunteerEntities();

            var a = from n in dbContext.Stages
                    where n.Stage_type == "人力申請"
                    select new
                    {
                        Stage_ID = n.Stage_ID,
                        Stage = n.Stage1
                    };
            foreach (var row in a)
            {
                switch (row.Stage.ToString())
                {
                    case "申請駁回":
                        rejectapply = row.Stage_ID;
                        break;
                }
            }

            var q = from n in dbContext.Manpower_apply
                    where n.Apply_ID == apply_ID
                    select n;
            foreach (var row in q)
            {
                row.Apply_state = rejectapply;
            }

            dbContext.SaveChanges();
        }
    }
}
