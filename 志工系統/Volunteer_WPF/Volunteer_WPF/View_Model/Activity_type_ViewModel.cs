using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer_WPF.Model;

namespace Volunteer_WPF.View_Model
{
    class Activity_type_ViewModel
    {
        public string Activity_type { get; set; }

        public List<string> SelectActivity_type()
        {
            Activity_type_Model activity_Type_Model = new Activity_type_Model();
            List<Activity_type_Model> activity_Type_Models = activity_Type_Model.SelectActivity_type();
            List<string> activity_Types = new List<string>();
            foreach (var row in activity_Type_Models)
            {
                activity_Types.Add(row.Activity_type);
            }

            return activity_Types;
        }
    }
}
