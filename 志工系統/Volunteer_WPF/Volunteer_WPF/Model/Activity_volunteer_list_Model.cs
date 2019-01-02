using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer_WPF.Model;

namespace Volunteer_WPF.Model
{
    class Activity_volunteer_list_Model
    {
        public int Activity_no { get; set; }
        public int Volunteer_no { get; set; }
        public string Volunteer { get; set; }
        public string Group { get; set; }
        public string Phone { get; set; }
        public bool Vegetarian { get; set; }

        public List<Activity_volunteer_list_Model> SelectVolunteer_list(string activity_name, DateTime start_date)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n1 in dbContext.Activity1
                    join n2 in dbContext.Volunteer
                    on n1.Volunteer_no equals n2.Volunteer_no
                    join n3 in dbContext.Activities
                    on n1.Activity_no equals n3.Activity_no
                    where n3.Activity_name == activity_name &&
                          n3.Activity_startdate == start_date
                    select new
                    {
                        Activity_no = n1.Activity_no,
                        Volunteer_no = n1.Volunteer_no,
                        Volunteer = n2.Chinese_name,
                        Phone_no = n2.Phone_no,
                        Mobile_no = n2.Mobile_no,
                        Vegetarian = n1.Vegetarian
                    };

            List<Activity_volunteer_list_Model> activity_Volunteer_List = new List<Activity_volunteer_list_Model>();
            foreach (var row in q)
            {
                Activity_volunteer_list_Model activity_Volunteer_List_Model = new Activity_volunteer_list_Model();
                activity_Volunteer_List_Model.Activity_no = row.Activity_no;
                activity_Volunteer_List_Model.Volunteer_no = row.Volunteer_no;
                activity_Volunteer_List_Model.Volunteer = row.Volunteer;
                if (row.Mobile_no > 0)
                {
                    activity_Volunteer_List_Model.Phone = row.Mobile_no.ToString();
                } else
                {
                    activity_Volunteer_List_Model.Phone = row.Phone_no.ToString();
                }
                if (row.Vegetarian != null)
                {
                    activity_Volunteer_List_Model.Vegetarian = (bool)row.Vegetarian;
                }
                else
                {
                    activity_Volunteer_List_Model.Vegetarian = false;
                }
                

                activity_Volunteer_List.Add(activity_Volunteer_List_Model);
            }

            return activity_Volunteer_List;
        }

        public void InsertVoluteer_list(List<int> insert_list, int activity_no)
        {
            VolunteerEntities dbContext = new VolunteerEntities();

            foreach (var row in insert_list)
            {
                Activity1 activity1 = new Activity1();
                activity1.Activity_no = activity_no;
                activity1.Volunteer_no = row;
                activity1.Registration_date = DateTime.Now;
                activity1.Confirm_time = DateTime.Now;
                activity1.Stage = "成功";
                activity1.Vegetarian = false;

                dbContext.Activity1.Add(activity1);
            }

            dbContext.SaveChanges();
        }

        public void DeleteVoluteer_list(List<int> delete_list, int activity_no)
        {
            VolunteerEntities dbContext = new VolunteerEntities();

            foreach (var row in delete_list)
            {
                var q = (from n in dbContext.Activity1
                         where n.Activity_no == activity_no &&
                               n.Volunteer_no == row
                         select n).First();

                q.Stage = "駁回";
            }

            dbContext.SaveChanges();
        }
    }
}
