using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer_WPF.Model;

namespace Volunteer_WPF.View_Model
{
    class Manpower_apply_ViewModel
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

        public List<Manpower_apply_ViewModel> SelectManpower_apply_byStage(string stage, string type, DateTime startdate, DateTime enddate)
        {
            Manpower_apply_Model manpower_Apply_Model = new Manpower_apply_Model();
            List<Manpower_apply_Model> Manpower_applys = manpower_Apply_Model.SelectManpower_apply_byStage(stage, type, startdate, enddate);
            List<Manpower_apply_ViewModel> apply_ViewModels = new List<Manpower_apply_ViewModel>();

            foreach (var row in Manpower_applys)
            {
                Manpower_apply_ViewModel manpower_Apply_ViewModel = new Manpower_apply_ViewModel();
                manpower_Apply_ViewModel.Apply_ID = row.Apply_ID;
                manpower_Apply_ViewModel.Application_unit = row.Application_unit;
                manpower_Apply_ViewModel.Applicant = row.Applicant;
                manpower_Apply_ViewModel.Apply_date = row.Apply_date;
                manpower_Apply_ViewModel.Applicant_phone = row.Applicant_phone;
                manpower_Apply_ViewModel.Work_place = row.Work_place;
                manpower_Apply_ViewModel.Apply_description = row.Apply_description;
                manpower_Apply_ViewModel.unit_Supervisor = row.unit_Supervisor;
                manpower_Apply_ViewModel.unit_heads = row.unit_heads;
                manpower_Apply_ViewModel.Supervision = row.Supervision;
                manpower_Apply_ViewModel.Reply_date = row.Reply_date;
                manpower_Apply_ViewModel.Repply_description = row.Repply_description;
                manpower_Apply_ViewModel.Supervision_heads = row.Supervision_heads;
                manpower_Apply_ViewModel.Application_number = row.Application_number;
                manpower_Apply_ViewModel.Reply_number = row.Reply_number;

                apply_ViewModels.Add(manpower_Apply_ViewModel);
            }

            return apply_ViewModels;
        }
    }
}
