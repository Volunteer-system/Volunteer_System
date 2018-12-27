using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Volunteer_Web.Models.VolunteerUse;

namespace Volunteer_Web.ViewModel
{
    public class Volunteer_Index_use
    {
        public IEnumerable<ActivityHistoryModel> ActivityHistoryModel { get; set; }
    }
}