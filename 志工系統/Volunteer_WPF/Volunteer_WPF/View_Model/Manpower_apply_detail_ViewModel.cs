using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer_WPF.Model;

namespace Volunteer_WPF.View_Model
{
    class Manpower_apply_detail_ViewModel
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
        //評量項目
        public List<string> Apply_assessments { get; set; }
        //評量結果
        public List<string> Apply_result { get; set; }

        public void SelectManpower_apply_byApply_ID(int apply_ID,string stage)
        {
            Manpower_apply_Model manpower_Apply_Model = new Manpower_apply_Model();
            if (stage == "新申請")
            {
                manpower_Apply_Model.SelectNNewManpower_apply_byApply_ID(apply_ID);
            }
            else
            {
                manpower_Apply_Model.SelectManpower_apply_byApply_ID(apply_ID);
            }            
            Apply_ID = manpower_Apply_Model.Apply_ID;
            Application_unit = manpower_Apply_Model.Application_unit;
            Applicant = manpower_Apply_Model.Applicant;
            Apply_date = manpower_Apply_Model.Apply_date;
            Applicant_phone = manpower_Apply_Model.Applicant_phone;
            Work_place = manpower_Apply_Model.Work_place;
            Apply_description = manpower_Apply_Model.Apply_description;
            unit_Supervisor = manpower_Apply_Model.unit_Supervisor;
            unit_heads = manpower_Apply_Model.unit_heads;
            Supervision = manpower_Apply_Model.Supervision;
            Reply_date = manpower_Apply_Model.Reply_date;
            Repply_description = manpower_Apply_Model.Repply_description;
            Supervision_heads = manpower_Apply_Model.Supervision_heads;
            Application_number = manpower_Apply_Model.Application_number;
            Reply_number = manpower_Apply_Model.Reply_number;

            Apply_assessment_Model apply_Assessment_Model = new Apply_assessment_Model();
            Apply_assessments = apply_Assessment_Model.SelectApply_assessment_byApply_ID(apply_ID);

            Apply_result_Model apply_Result_Model = new Apply_result_Model();
            Apply_result = apply_Result_Model.SelectApply_result_byApply_ID(apply_ID);
        }

    }
}
