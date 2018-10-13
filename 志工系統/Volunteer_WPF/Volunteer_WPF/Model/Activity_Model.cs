using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer_WPF.Model
{
    class Activity_Model
    {
        public string Activity_no { get; set; }
        public string Activity_name { get; set; }

        //擴充
        public string Undertaker { get; set; }

        public List<Activity_Model> SelectVolunteer_ActivitybyVolunteerno(int Volunteer_no)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n1 in dbContext.Volunteer
                    join n2 in dbContext.Activity1
                    on n1.Volunteer_no equals n2.Volunteer_no
                    join n3 in dbContext.Activities
                    on n2.Activity_no equals n3.Activity_no
                    join n4 in dbContext.Volunteer_supervision
                    on n3.Undertaker equals n4.supervision_ID
                    where n1.Volunteer_no == Volunteer_no
                    select new
                    {
                        Activity_no = n3.Activity_no,
                        Activity_name = n3.Activity_name,
                        Undertaker = n4.supervision_Name
                    };

            List<Activity_Model> activity_list = new List<Activity_Model>();
            foreach (var row in q)
            {
                Activity_Model activity_Model = new Activity_Model();
                activity_Model.Activity_no = row.Activity_no.ToString();
                activity_Model.Activity_name = row.Activity_name.ToString();
                activity_Model.Undertaker = row.Undertaker.ToString();

                activity_list.Add(activity_Model);
            }

            return activity_list;
        }
    }
}
