using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer_WPF.Model
{
    class Activity_type_Model
    {
        public string Activity_type { get; set; }

        public List<Activity_type_Model> SelectActivity_type()
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n in dbContext.Activity_type
                    select new { activity_type = n.Activity_type1};

            List<Activity_type_Model> activity_Type_Models = new List<Activity_type_Model>();
            foreach (var row in q)
            {
                Activity_type_Model activity_Type_Model = new Activity_type_Model();
                activity_Type_Model.Activity_type = row.activity_type;
                activity_Type_Models.Add(activity_Type_Model);
            }

            return activity_Type_Models;
        }

    }
}
