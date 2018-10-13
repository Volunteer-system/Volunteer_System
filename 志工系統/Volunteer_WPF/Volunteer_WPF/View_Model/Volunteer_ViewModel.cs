using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Volunteer_WPF.Model;

namespace Volunteer_WPF.View_Model
{
    class Volunteer_ViewModel
    {
        Volunteer_Model volunteer_Model = new Volunteer_Model();

        //志工編號
        public string Volunteer_no
        {
            get { return volunteer_Model.Volunteer_no; }
            set { volunteer_Model.Volunteer_no = value; }
        }
        public string Chinese_name
        {
            get {return volunteer_Model.Chinese_name; }
            set { volunteer_Model.Chinese_name = value; }
        }
        public string English_name
        {
            get { return volunteer_Model.English_name; }
            set { volunteer_Model.English_name = value; }
        }
        public string Sex
        {
            get { return volunteer_Model.Sex; }
            set { volunteer_Model.Sex = value; }
        }
        public string Birthday
        {
            get { return volunteer_Model.Birthday; }
            set { volunteer_Model.Birthday = value; }
        }
        public string IDcrad_no
        {
            get { return volunteer_Model.IDcrad_no; }
            set { volunteer_Model.IDcrad_no = value; }
        }
        public string Medical_record_no
        {
            get { return volunteer_Model.Medical_record_no; }
            set { volunteer_Model.Medical_record_no = value; }
        }
        public string Identity_type
        {
            get { return volunteer_Model.Identity_type; }
            set { volunteer_Model.Identity_type = value; }
        }
        public string Seniority
        {
            get { return volunteer_Model.Seniority; }
            set { volunteer_Model.Seniority = value; }
        }
        public string Join_date
        {
            get { return volunteer_Model.Join_date; }
            set { volunteer_Model.Join_date = value; }
        }
        public string Leave_date
        {
            get { return volunteer_Model.Leave_date; }
            set { volunteer_Model.Leave_date = value; }
        }
        public string Leave_reason
        {
            get { return volunteer_Model.Leave_reason; }
            set { volunteer_Model.Leave_reason = value; }
        }
        public string Phone_no
        {
            get { return volunteer_Model.Phone_no; }
            set { volunteer_Model.Phone_no = value; }
        }
        public string Mobile_no
        {
            get { return volunteer_Model.Mobile_no; }
            set { volunteer_Model.Mobile_no = value; }
        }
        public string Vest_no
        {
            get { return volunteer_Model.Vest_no; }
            set { volunteer_Model.Vest_no = value; }
        }
        public string Postal_code
        {
            get { return volunteer_Model.Postal_code; }
            set { volunteer_Model.Postal_code = value; }
        }
        public string Address
        {
            get { return volunteer_Model.Address; }
            set { volunteer_Model.Address = value; }
        }
        public string Education
        {
            get { return volunteer_Model.Education; }
            set { volunteer_Model.Education = value; }
        }
        public string Lssuing_unit_no
        {
            get { return volunteer_Model.Lssuing_unit_no; }
            set { volunteer_Model.Lssuing_unit_no = value; }
        }
        public string Lssuing_unit
        {
            get { return volunteer_Model.Lssuing_unit; }
            set { volunteer_Model.Lssuing_unit = value; }
        }
        public string Service_manual_no
        {
            get { return volunteer_Model.Service_manual_no; }
            set { volunteer_Model.Service_manual_no = value; }
        }
        public string Personality_scale
        {
            get { return volunteer_Model.Personality_scale; }
            set { volunteer_Model.Personality_scale = value; }
        }
        public BitmapImage Photo
        {
            get { return volunteer_Model.Photo; }
            set { volunteer_Model.Photo = value; }
        }

        //============================================================================================================
        //合併項目
        //組別
        public string Group
        {
            get { return volunteer_Model.Group; }
            set { volunteer_Model.Group = value; }
        }
        //專長
        public string Experise
        {
            get { return volunteer_Model.Experise; }
            set { volunteer_Model.Experise = value; }
        }
        //過往幹部經歷
        public string Leader_list { get; set; }
        //專長清單
        public string Experise_list { get; set; }

        public List<Volunteer_ViewModel> Search_Volunteer(string Name1, string Group1, string Expertise1)
        {
            //建立等等回傳
            List<Volunteer_ViewModel> the_get = new List<Volunteer_ViewModel>();
            Volunteer_Model volunteer = new Volunteer_Model();
            List<Volunteer_Model> volunteer_Models = volunteer.Search_Volunteer(Name1, Group1, Expertise1);

            foreach (var item in volunteer_Models)
            {
                Volunteer_ViewModel _ViewModel = new Volunteer_ViewModel();
                _ViewModel.Volunteer_no = item.Volunteer_no;
                _ViewModel.Chinese_name = item.Chinese_name;
                _ViewModel.English_name = item.English_name;
                _ViewModel.Sex = item.Sex;
                _ViewModel.Birthday = item.Birthday;
                _ViewModel.IDcrad_no = item.IDcrad_no;
                _ViewModel.Medical_record_no = item.Medical_record_no;
                _ViewModel.Identity_type = item.Identity_type;
                _ViewModel.Seniority = item.Seniority;
                _ViewModel.Join_date = item.Join_date;
                _ViewModel.Leave_date = item.Leave_date;
                _ViewModel.Leave_reason = item.Leave_reason;
                _ViewModel.Phone_no = item.Phone_no;
                _ViewModel.Mobile_no = item.Mobile_no;
                _ViewModel.Vest_no = item.Vest_no;
                _ViewModel.Postal_code = item.Postal_code;
                _ViewModel.Address = item.Address;
                _ViewModel.Education = item.Education;
                _ViewModel.Lssuing_unit_no = item.Lssuing_unit_no;
                _ViewModel.Service_manual_no = item.Service_manual_no;
                _ViewModel.Personality_scale = item.Personality_scale;
                the_get.Add(_ViewModel);
            }

            return the_get;

        }

        public void SelectVolunteer_byVolunteerno(int Volunteer_no)
        {
            volunteer_Model.SelectVolunteer_byVolunteerno(Volunteer_no);

            Chinese_name = volunteer_Model.Chinese_name;
            English_name = volunteer_Model.English_name;
            Sex = volunteer_Model.Sex;
            Birthday = volunteer_Model.Birthday;
            IDcrad_no = volunteer_Model.IDcrad_no;
            Medical_record_no = volunteer_Model.Medical_record_no;
            Identity_type = volunteer_Model.Identity_type;
            Seniority = volunteer_Model.Seniority;
            Join_date = volunteer_Model.Join_date;
            Leave_date = volunteer_Model.Leave_date;
            Leave_reason = volunteer_Model.Leave_reason;
            Phone_no = volunteer_Model.Phone_no;
            Mobile_no = volunteer_Model.Mobile_no;
            Vest_no = volunteer_Model.Vest_no;
            Postal_code = volunteer_Model.Postal_code;
            Address = volunteer_Model.Address;
            Education = volunteer_Model.Education;
            Lssuing_unit = volunteer_Model.Lssuing_unit;
            Service_manual_no = volunteer_Model.Service_manual_no;
            Personality_scale = volunteer_Model.Personality_scale;
            Photo = volunteer_Model.Photo;

            List<string> list_Position = volunteer_Model.SelectVolunteer_LeaderbyVolunteerno(Volunteer_no);
            Leader_list = null;
            if (list_Position.Count > 0)
            {
                foreach (var row in list_Position)
                {
                    Leader_list += row + "->";
                }
            }
            
            List<string> list_Expertise = volunteer_Model.SelectVolunteer_ExpertisebyVolunteerno(Volunteer_no);
            Experise_list = null;
            if (list_Expertise.Count > 0)
            {
                foreach (var row in list_Expertise)
                {
                    Experise_list += row + ", ";
                }
            }           

        }        
    }
}
