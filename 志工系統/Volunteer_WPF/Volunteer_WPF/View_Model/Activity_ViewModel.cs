using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer_WPF.Model;

namespace Volunteer_WPF.View_Model
{
    class Activity_ViewModel
    {
        public string Activity_no { get; set; }
        public string Activity_name { get; set; }

        //擴充
        public string Undertaker { get; set; }

        public List<Activity_ViewModel> SelectVolunteer_ActivitybyVolunteerno(int Volunteer_no)
        {
            Activity_Model activity_Model = new Activity_Model();
            List<Activity_ViewModel> activity_list = new List<Activity_ViewModel>();
            List<Activity_Model> activity_model_list = activity_Model.SelectVolunteer_ActivitybyVolunteerno(Volunteer_no);

            foreach (var row in activity_model_list)
            {
                Activity_ViewModel activity_ViewModel = new Activity_ViewModel();
                activity_ViewModel.Activity_no = row.Activity_no;
                activity_ViewModel.Activity_name = row.Activity_name;
                activity_ViewModel.Undertaker = row.Undertaker;

                activity_list.Add(activity_ViewModel);
            }

            return activity_list;
        }
       
    }
}
