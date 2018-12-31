using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Volunteer_WPF.Model;

namespace Volunteer_WPF.View_Model
{
    class Shift_schedule_detail_ViewModel
    {
        public int Volunteer_no { get; set; }
        public string Chinese_name { get; set; }
        public string Identity_type_name { get; set; }
        public byte[] Photo { get; set; }

        public List<Shift_schedule_detail_ViewModel> SelectStay_Volunteer_byService_Period(int application_unit_no, int service_period_no)
        {
            List<Shift_schedule_detail_ViewModel> Stay_Volunteers = new List<Shift_schedule_detail_ViewModel>();

            Service_period_volunteer_Model service_Period_Volunteer_Model = new Service_period_volunteer_Model();
            List<Service_period_volunteer_Model> Service_period_volunteers = service_Period_Volunteer_Model.SelectService_period_volunteer(application_unit_no, service_period_no, "續留");
            foreach (var row in Service_period_volunteers)
            {
                Shift_schedule_detail_ViewModel shift_Schedule_Detail_ViewModel = new Shift_schedule_detail_ViewModel();
                shift_Schedule_Detail_ViewModel.Volunteer_no = row.Volunteer_no;
                shift_Schedule_Detail_ViewModel.Chinese_name = row.Volunteer;
                shift_Schedule_Detail_ViewModel.Identity_type_name = row.Type;
                shift_Schedule_Detail_ViewModel.Photo = row.Photo;

                Stay_Volunteers.Add(shift_Schedule_Detail_ViewModel);
            }
            return Stay_Volunteers;
        }

        public List<Shift_schedule_detail_ViewModel> SelectNew_Volunteer_byService_Period(int application_unit_no, int service_period_no)
        {
            List<Shift_schedule_detail_ViewModel> New_Volunteers = new List<Shift_schedule_detail_ViewModel>();

            Service_period_volunteer_Model service_Period_Volunteer_Model = new Service_period_volunteer_Model();
            List<Service_period_volunteer_Model> Service_period_volunteers = service_Period_Volunteer_Model.SelectService_period_volunteer(application_unit_no, service_period_no, "新申請");
            foreach (var row in Service_period_volunteers)
            {
                Shift_schedule_detail_ViewModel shift_Schedule_Detail_ViewModel = new Shift_schedule_detail_ViewModel();
                shift_Schedule_Detail_ViewModel.Volunteer_no = row.Volunteer_no;
                shift_Schedule_Detail_ViewModel.Chinese_name = row.Volunteer;
                shift_Schedule_Detail_ViewModel.Identity_type_name = row.Type;
                shift_Schedule_Detail_ViewModel.Photo = row.Photo;

                New_Volunteers.Add(shift_Schedule_Detail_ViewModel);
            }
            return New_Volunteers;
        }

        public List<Shift_schedule_detail_ViewModel> SelectLeave_Volunteer_byService_Period(int application_unit_no, int service_period_no)
        {
            List<Shift_schedule_detail_ViewModel> Leave_Volunteers = new List<Shift_schedule_detail_ViewModel>();

            Service_period_volunteer_Model service_Period_Volunteer_Model = new Service_period_volunteer_Model();
            List<Service_period_volunteer_Model> Service_period_volunteers = service_Period_Volunteer_Model.SelectService_period_volunteer(application_unit_no, service_period_no, "移除");
            foreach (var row in Service_period_volunteers)
            {
                Shift_schedule_detail_ViewModel shift_Schedule_Detail_ViewModel = new Shift_schedule_detail_ViewModel();
                shift_Schedule_Detail_ViewModel.Volunteer_no = row.Volunteer_no;
                shift_Schedule_Detail_ViewModel.Chinese_name = row.Volunteer;
                shift_Schedule_Detail_ViewModel.Identity_type_name = row.Type;
                shift_Schedule_Detail_ViewModel.Photo = row.Photo;

                Leave_Volunteers.Add(shift_Schedule_Detail_ViewModel);
            }
            return Leave_Volunteers;
        }

        public List<Shift_schedule_detail_ViewModel> SelectVolunteer_byIdentity_type(string Identity_type)
        {
            Volunteer_Model volunteer_Model = new Volunteer_Model();

            List<Shift_schedule_detail_ViewModel> Schedule_Detail_ViewModels = new List<Shift_schedule_detail_ViewModel>();
            foreach (var row in volunteer_Model.SelectVolunteer_byIdentity_type(Identity_type))
            {
                Shift_schedule_detail_ViewModel Schedule_Detail_ViewModel = new Shift_schedule_detail_ViewModel();
                Schedule_Detail_ViewModel.Volunteer_no = int.Parse(row.Volunteer_no);
                Schedule_Detail_ViewModel.Chinese_name = row.Chinese_name;
                Schedule_Detail_ViewModel.Identity_type_name = row.Identity_type;
                Schedule_Detail_ViewModel.Photo = row._Photo;
                Schedule_Detail_ViewModels.Add(Schedule_Detail_ViewModel);
            }

            return Schedule_Detail_ViewModels;
        }

        public List<Shift_schedule_detail_ViewModel> SelectVolunteer_byName(string name)
        {
            Volunteer_Model volunteer_Model = new Volunteer_Model();
            List<Shift_schedule_detail_ViewModel> Schedule_Detail_ViewModels = new List<Shift_schedule_detail_ViewModel>();
            foreach (var row in volunteer_Model.SelectVolunteer_byName(name))
            {
                Shift_schedule_detail_ViewModel shift_Schedule_Detail_ViewModel = new Shift_schedule_detail_ViewModel();
                shift_Schedule_Detail_ViewModel.Volunteer_no = int.Parse(row.Volunteer_no);
                shift_Schedule_Detail_ViewModel.Chinese_name = row.Chinese_name;
                shift_Schedule_Detail_ViewModel.Identity_type_name = row.Identity_type;
                shift_Schedule_Detail_ViewModel.Photo = row._Photo;

                Schedule_Detail_ViewModels.Add(shift_Schedule_Detail_ViewModel);
            }

            return Schedule_Detail_ViewModels;
        }

        public void UpdateSchedule_volunteer(List<Shift_schedule_detail_ViewModel> stay_lists, List<Shift_schedule_detail_ViewModel> new_list, List<Shift_schedule_detail_ViewModel> leave_list, int unit_id, int service_period_no)
        {
            //存服務意願TABLE
            Service_period_volunteer_Model service_Period_Volunteer_Model = new Service_period_volunteer_Model();
            //續留
            List<Service_period_volunteer_Model> stay_period_volunteers = new List<Service_period_volunteer_Model>();
            foreach (var row in stay_lists)
            {
                Service_period_volunteer_Model service_Period_Volunteer = new Service_period_volunteer_Model();
                service_Period_Volunteer.Volunteer_no = row.Volunteer_no;
                service_Period_Volunteer.Volunteer = row.Chinese_name;
                service_Period_Volunteer.Type = row.Identity_type_name;
                service_Period_Volunteer.Stage = "續留";

                stay_period_volunteers.Add(service_Period_Volunteer);
            }            
            //新申請
            List<Service_period_volunteer_Model> new_period_volunteers = new List<Service_period_volunteer_Model>();
            foreach (var row in new_list)
            {
                Service_period_volunteer_Model service_Period_Volunteer = new Service_period_volunteer_Model();
                service_Period_Volunteer.Volunteer_no = row.Volunteer_no;
                service_Period_Volunteer.Volunteer = row.Chinese_name;
                service_Period_Volunteer.Type = row.Identity_type_name;
                service_Period_Volunteer.Stage = "新申請";

                new_period_volunteers.Add(service_Period_Volunteer);
            }            
            //移除
            List<Service_period_volunteer_Model> leave_period_volunteers = new List<Service_period_volunteer_Model>();
            foreach (var row in leave_list)
            {
                Service_period_volunteer_Model service_Period_Volunteer = new Service_period_volunteer_Model();
                service_Period_Volunteer.Volunteer_no = row.Volunteer_no;
                service_Period_Volunteer.Volunteer = row.Chinese_name;
                service_Period_Volunteer.Type = row.Identity_type_name;
                service_Period_Volunteer.Stage = "移除";

                leave_period_volunteers.Add(service_Period_Volunteer);
            }
            
            service_Period_Volunteer_Model.UpdateService_period_volunteer(stay_period_volunteers, new_period_volunteers, leave_period_volunteers, unit_id, service_period_no);
            //存總班表TABLE
            Shift_schedule_Model shift_Schedule_Model = new Shift_schedule_Model();
            shift_Schedule_Model.UpdateShift_schedule(unit_id, service_period_no);
        }
    }
}
