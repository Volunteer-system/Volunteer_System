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
        //志工清單
        public List<Volunteer_List> Volunteer_Lists { get; set; }
        //專長需求
        public List<string> Expertises { get; set; }
        //運用單位服務時段
        public List<string> Service_Periods { get; set; }

        public void SelectApplication_unit_byApplication_unit(string Application_unit)
        {
            Application_unit_Model application_Unit_Model = new Application_unit_Model();
            application_Unit_Model.SelectApplication_unit_byApplication_unit(Application_unit);
            Application_unit_no = application_Unit_Model.Application_unit_no.ToString();
            Application_unit = application_Unit_Model.Application_unit;
            Group = application_Unit_Model.Group;
            Application_phone_no = application_Unit_Model.Application_phone_no;
            Principal = application_Unit_Model.Principal;
            Principal_phone_no = application_Unit_Model.Principal_phone_no;
            Application_address = application_Unit_Model.Application_address;
            Work_content = application_Unit_Model.Work_content;
            Total_volunteers = application_Unit_Model.Total_volunteers;

            Unit_volunteer_list_Model unit_Volunteer_List_Model = new Unit_volunteer_list_Model();
            List<Unit_volunteer_list_Model> unit_Volunteer_List_Models = unit_Volunteer_List_Model.Selectvolunteer_list_byApplication_unit(application_Unit_Model.Application_unit_no);
            List<Volunteer_List> tmp_Volunteer_Lists = new List<Volunteer_List>();
            foreach (var row in unit_Volunteer_List_Models)
            {
                Volunteer_List volunteer_List = new Volunteer_List();
                volunteer_List.Chinese_name = row.Chinese_name;
                volunteer_List.English_name = row.English_name;
                volunteer_List.Sex = row.Sex;
                volunteer_List.Birthday = row.Birthday;
                volunteer_List.IDcrad_no = row.IDcrad_no;
                volunteer_List.Medical_record_no = row.Medical_record_no;
                volunteer_List.Identity_type = row.Identity_type;
                volunteer_List.Seniority = row.Seniority;
                volunteer_List.Vest_no = row.Vest_no;
                volunteer_List.Education = row.Education;

                tmp_Volunteer_Lists.Add(volunteer_List);
            }
            Volunteer_Lists = tmp_Volunteer_Lists;

            Unit_expertise_Model unit_Expertise_Model = new Unit_expertise_Model();
            List<string> unit_Expertises = unit_Expertise_Model.SelectUnit_ExpertisebyApplication_unit_no(application_Unit_Model.Application_unit_no);
            List<string> tmp_expertises = new List<string>();
            foreach (var row in unit_Expertises)
            {
                tmp_expertises.Add(row);
            }
            Expertises = tmp_expertises;

            Unit_service_period_Model unit_Service_Period_Model = new Unit_service_period_Model();
            List<string> unit_Service_Periods = unit_Service_Period_Model.SelectUnit_service_period_byApplication_unit_no(application_Unit_Model.Application_unit_no);
            List<string> tmp_Service_Periods = new List<string>();
            foreach (var row in unit_Service_Periods)
            {
                tmp_Service_Periods.Add(row);
            }
            Service_Periods = tmp_Service_Periods;
        }

        public List<Application_unit_ViewModel> SelectApplication_unit(string name, string group_name, int membersmin, int membersmax)
        {
            Application_unit_Model application_Unit_Model = new Application_unit_Model();
            List<Application_unit_Model> Application_unit_Models = application_Unit_Model.SelectApplication_unit(name, group_name, membersmin, membersmax);
            List<Application_unit_ViewModel> application_Unit_ViewModels = new List<Application_unit_ViewModel>();
            foreach (var row in Application_unit_Models)
            {
                Application_unit_ViewModel application_Unit_ViewModel = new Application_unit_ViewModel();
                application_Unit_ViewModel.Application_unit_no = row.Application_unit_no.ToString();
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

    public class Volunteer_List
    {
        //中文姓名
        public string Chinese_name { get; set; }
        //英文姓名(同護照)
        public string English_name { get; set; }
        //性別
        public string Sex { get; set; }
        //生日
        public string Birthday { get; set; }
        //身分證字號(護照編號)
        public string IDcrad_no { get; set; }
        //病歷號
        public string Medical_record_no { get; set; }
        //身分類別
        public string Identity_type { get; set; }
        //加入日期
        public string Seniority { get; set; }
        //志工背心號碼
        public string Vest_no { get; set; }
        //學歷
        public string Education { get; set; }
    }
}
