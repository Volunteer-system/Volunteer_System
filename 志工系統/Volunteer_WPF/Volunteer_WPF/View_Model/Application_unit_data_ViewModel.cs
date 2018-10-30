using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer_WPF.Model;

namespace Volunteer_WPF.View_Model
{
    class Application_unit_data_ViewModel
    {
        //運用單位編號
        public int Application_unit_no { get; set; }
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
        //志工清單
        public List<Volunteer_List> Volunteer_Lists { get; set; }
        //專長需求
        public List<string> Expertises { get; set; }
        //運用單位服務時段
        public List<Unit_service_period> Service_Periods { get; set; }

        public void SelectApplication_unit_byApplication_unit(string application_unit)
        {
            Application_unit_ViewModel application_Unit_ViewModel = new Application_unit_ViewModel();
            application_Unit_ViewModel.SelectApplication_unit_byApplication_unit(application_unit);

            Application_unit_no = application_Unit_ViewModel.Application_unit_no;
            Application_unit = application_Unit_ViewModel.Application_unit;
            Group = application_Unit_ViewModel.Group;
            Application_phone_no = application_Unit_ViewModel.Application_phone_no;
            Principal = application_Unit_ViewModel.Principal;
            Principal_phone_no = application_Unit_ViewModel.Principal_phone_no;
            Application_address = application_Unit_ViewModel.Application_address;
            Work_content = application_Unit_ViewModel.Work_content;
            Total_volunteers = application_Unit_ViewModel.Total_volunteers;
            Volunteer_Lists = application_Unit_ViewModel.Volunteer_Lists;
            Expertises = application_Unit_ViewModel.Expertises;
            Service_Periods = application_Unit_ViewModel.Service_Periods;
        }

        public void CommitApplication_unit(string Commit_type, Application_unit_data_ViewModel application_Unit_Data_ViewModel, List<string> Insert_list, List<string> Delete_list)
        {
            Application_unit_Model application_Unit_Model = new Application_unit_Model();
            Unit_expertise_Model unit_Expertise_Model = new Unit_expertise_Model();
            if (application_Unit_Data_ViewModel.Application_unit_no > 0)
            {
                application_Unit_Model.Application_unit_no = application_Unit_Data_ViewModel.Application_unit_no;
            }            
            application_Unit_Model.Application_unit = application_Unit_Data_ViewModel.Application_unit;
            application_Unit_Model.Group = application_Unit_Data_ViewModel.Group;
            application_Unit_Model.Application_phone_no = application_Unit_Data_ViewModel.Application_phone_no;
            application_Unit_Model.Application_address = application_Unit_Data_ViewModel.Application_address;
            application_Unit_Model.Principal = application_Unit_Data_ViewModel.Principal;
            application_Unit_Model.Principal_phone_no = application_Unit_Data_ViewModel.Principal_phone_no;
            application_Unit_Model.Work_content = application_Unit_Data_ViewModel.Work_content;

            switch (Commit_type)
            {
                case "新增":
                    application_Unit_Model.InsertApplication_unit(application_Unit_Model);                    
                    break;
                case "修改":
                    application_Unit_Model.UpdateApplication_unit(application_Unit_Model);
                    unit_Expertise_Model.DeleteUnit_Expertise(application_Unit_Model.Application_unit_no, Delete_list);
                    break;
            };

            unit_Expertise_Model.InsertUnit_Expertise(application_Unit_Model.Application_unit_no, Insert_list);

        }       
    }
}
