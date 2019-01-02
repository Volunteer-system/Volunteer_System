using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer_WPF.Model;

namespace Volunteer_WPF.View_Model
{
    class Activity_volunteer_list_ViewModel
    {
        public int Activity_no { get; set; }
        public int Volunteer_no { get; set; }
        public string Volunteer { get; set; }
        public string Group { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Vegetarian { get; set; }

        public List<Activity_volunteer_list_ViewModel> SelectVolunteer_list(string activity_name, DateTime start_date)
        {
            List<Activity_volunteer_list_ViewModel> Activity_volunteer_list = new List<Activity_volunteer_list_ViewModel>();

            Activity_volunteer_list_Model activity_Volunteer_List_Model = new Activity_volunteer_list_Model();
            foreach (var row in activity_Volunteer_List_Model.SelectVolunteer_list(activity_name, start_date))
            {
                Activity_volunteer_list_ViewModel activity_Volunteer_List_ViewModel = new Activity_volunteer_list_ViewModel();
                activity_Volunteer_List_ViewModel.Activity_no = row.Activity_no;
                activity_Volunteer_List_ViewModel.Volunteer_no = row.Volunteer_no;
                activity_Volunteer_List_ViewModel.Volunteer = row.Volunteer;
                activity_Volunteer_List_ViewModel.Phone = row.Phone;
                activity_Volunteer_List_ViewModel.Vegetarian = row.Vegetarian;

                Activity_volunteer_list.Add(activity_Volunteer_List_ViewModel);
            }

            return Activity_volunteer_list;
        }

        public void SelectVolunteer_byvolunteer_name(string name)
        {
            Volunteer_Model volunteer_Model = new Volunteer_Model();
            foreach (var row in volunteer_Model.SelectVolunteer_byName(name))
            {
                
                this.Volunteer_no = int.Parse(row.Volunteer_no);
                this.Volunteer = row.Chinese_name;
                if (!string.IsNullOrEmpty(row.Mobile_no))
                {
                    this.Phone = row.Mobile_no;
                }
                else
                {
                    this.Phone = row.Phone_no;
                }

                break;
            }
        }

        public void InsertVoluteer_list(List<int> insert_list, int activity_no)
        {
            Activity_volunteer_list_Model activity_Volunteer_List_Model = new Activity_volunteer_list_Model();
            activity_Volunteer_List_Model.InsertVoluteer_list(insert_list, activity_no);
        }

        public void DeleteVoluteer_list(List<int> delete_list, int activity_no)
        {
            Activity_volunteer_list_Model activity_Volunteer_List_Model = new Activity_volunteer_list_Model();
            activity_Volunteer_List_Model.DeleteVoluteer_list(delete_list, activity_no);
        }
    }
}
