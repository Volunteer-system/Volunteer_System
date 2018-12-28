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
        public BitmapImage Photo { get; set; }        

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
                Schedule_Detail_ViewModel.Photo = row.Photo;
                Schedule_Detail_ViewModels.Add(Schedule_Detail_ViewModel);
            }

            return Schedule_Detail_ViewModels;
        }
    }
}
