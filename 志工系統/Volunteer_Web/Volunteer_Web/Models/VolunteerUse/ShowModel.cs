using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Volunteer_Web.Models.VolunteerUse
{
    public class ShowModel
    {
        public IEnumerable<ActivityHistoryModel> ActivityHistoryModel { get; set; }
        public ActivityDetail ActivityDetail { get; set; }
    }
}