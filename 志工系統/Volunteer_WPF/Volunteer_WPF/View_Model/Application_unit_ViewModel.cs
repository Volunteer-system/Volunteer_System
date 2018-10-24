using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer_WPF.Model;

namespace Volunteer_WPF.View_Model
{
    class Application_unit_ViewModel
    {
        //運用單位編號
        public string Application_unit_no { get; set; }
        //運用單位
        public string Application_unit { get; set; }
        //組別
        public string Group { get; set; }
        //運用單位聯絡電話
        public string Application_phone_no { get; set; }
        //負責人
        public string Principal { get; set; }
        //負責人聯絡電話
        public string Principal_phone_no { get; set; }
        //運用單位地址
        public string Application_address { get; set; }
        //工作內容描述
        public string Work_content { get; set; }
        //志工總人數
        public string Total_volunteers { get; set; }

        public List<Application_unit_ViewModel> SelectApplication_unit(string name, string group_name, int membersmin, int membersmax)
        {
            Application_unit_Model application_Unit_Model = new Application_unit_Model();
            List <Application_unit_Model> Application_unit_Models = application_Unit_Model.SelectApplication_unit(name, group_name, membersmin, membersmax);
            List<Application_unit_ViewModel> application_Unit_ViewModels = new List<Application_unit_ViewModel>();
            foreach (var row in Application_unit_Models)
            {
                Application_unit_ViewModel application_Unit_ViewModel = new Application_unit_ViewModel();
                application_Unit_ViewModel.Application_unit_no = row.Application_unit_no;
                application_Unit_ViewModel.Application_unit = row.Application_unit;
                application_Unit_ViewModel.Group = row.Group;
                application_Unit_ViewModel.Application_phone_no = row.Application_phone_no;
                application_Unit_ViewModel.Principal = row.Principal;
                application_Unit_ViewModel.Principal_phone_no = row.Principal_phone_no;
                application_Unit_ViewModel.Application_address = row.Application_address;
                application_Unit_ViewModel.Work_content = row.Work_content;
                application_Unit_ViewModel.Total_volunteers = row.Total_volunteers;

                application_Unit_ViewModels.Add(application_Unit_ViewModel);
            }

            return application_Unit_ViewModels;
        }
    }
}
