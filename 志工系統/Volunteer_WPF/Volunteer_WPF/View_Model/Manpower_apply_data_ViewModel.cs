using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer_WPF.Model;

namespace Volunteer_WPF.View_Model
{
    class Manpower_apply_data_ViewModel
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

        public void SelectManpower_apply_byApply_ID(string type, int apply_id)
        {
            Manpower_apply_detail_ViewModel manpower_Apply_Detail_ViewModel = new Manpower_apply_detail_ViewModel();
            switch (type)
            {
                case "新申請":
                    Manpower_apply_Model manpower_Apply_Model = new Manpower_apply_Model();
                    manpower_Apply_Model.SelectManpower_apply_byApply_ID(apply_id);

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
                    break;
                case "修改":
                    manpower_Apply_Detail_ViewModel.SelectManpower_apply_byApply_ID(apply_id);

                    Apply_ID = manpower_Apply_Detail_ViewModel.Apply_ID;
                    Application_unit = manpower_Apply_Detail_ViewModel.Application_unit;
                    Applicant = manpower_Apply_Detail_ViewModel.Applicant;
                    Apply_date = manpower_Apply_Detail_ViewModel.Apply_date;
                    Applicant_phone = manpower_Apply_Detail_ViewModel.Applicant_phone;
                    Work_place = manpower_Apply_Detail_ViewModel.Work_place;
                    Apply_description = manpower_Apply_Detail_ViewModel.Apply_description;
                    unit_Supervisor = manpower_Apply_Detail_ViewModel.unit_Supervisor;
                    unit_heads = manpower_Apply_Detail_ViewModel.unit_heads;
                    Supervision = manpower_Apply_Detail_ViewModel.Supervision;
                    Reply_date = manpower_Apply_Detail_ViewModel.Reply_date;
                    Repply_description = manpower_Apply_Detail_ViewModel.Repply_description;
                    Supervision_heads = manpower_Apply_Detail_ViewModel.Supervision_heads;
                    Application_number = manpower_Apply_Detail_ViewModel.Application_number;
                    Reply_number = manpower_Apply_Detail_ViewModel.Reply_number;
                    Apply_assessments = manpower_Apply_Detail_ViewModel.Apply_assessments;
                    Apply_result = manpower_Apply_Detail_ViewModel.Apply_result;
                    break;
            }
        }

        public void UpdateManpower_apply(Manpower_apply_data_ViewModel Apply_Data_ViewModel, List<string> insert_Assessment, List<string> delete_Assessment, List<string> insert_Result, List<string> delete_Result)
        {
            Apply_assessment_Model apply_Assessment_Model = new Apply_assessment_Model();
            apply_Assessment_Model.InsertApply_assessment(Apply_Data_ViewModel.Apply_ID, insert_Assessment);
            apply_Assessment_Model.DeleteApply_assessment(Apply_Data_ViewModel.Apply_ID, delete_Assessment);

            Apply_result_Model apply_Result_Model = new Apply_result_Model();
            apply_Result_Model.InsertApply_result(Apply_Data_ViewModel.Apply_ID,insert_Result);
            apply_Result_Model.DeleteApply_result(Apply_Data_ViewModel.Apply_ID, delete_Result);

            Manpower_apply_Model manpower_Apply_Model = new Manpower_apply_Model();
            manpower_Apply_Model.UpdateManpower_apply(Apply_Data_ViewModel.Apply_ID, int.Parse(Apply_Data_ViewModel.Reply_number), Apply_Data_ViewModel.Repply_description);

        }

        public void UpdateManpower_apply_byreject(int Apply_ID)
        {
            Manpower_apply_Model manpower_Apply_Model = new Manpower_apply_Model();
        }
    }
}
