using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Volunteer_Web.Models;
namespace Volunteer_Web.ViewModel
{
    public class Activity_Photo_Schedule_typeVM
    {

        //public IEnumerable<Activity_Schedule> activity_schedules { get; set; }


        //public Activity activity { get; set; }
        //public Activity_photo activity_photo { get; set; }
        ////public IEnumerable<Activity_Schedule> activity_schedules { get; set; }
        //public Activity_type activity_types { get; set; }
        public IEnumerable<Activity> activity { get; set; }
        public IEnumerable<Activity_photo> activity_photo { get; set; }
        public IEnumerable<Activity_type> activity_types { get; set; }
        public IEnumerable<activity_volunteerNo_VM> acvno_VM { get; set; }
    }
}