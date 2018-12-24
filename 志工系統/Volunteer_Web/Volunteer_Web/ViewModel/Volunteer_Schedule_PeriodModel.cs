using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Volunteer_Web.Models;

namespace Volunteer_Web.ViewModel
{
    public class Volunteer_Schedule_PeriodModel
    {
        public int Service_period_no { get; set; }

        public string Service_period { get; set; }
      public  List<Volunteer_Schedule_PeriodModel> Getservice_period()
        {
            VolunteerEntities db = new VolunteerEntities();
            List<Volunteer_Schedule_PeriodModel> VP = new List<Volunteer_Schedule_PeriodModel>();
            var FP = from s in db.Service_period1
                     select new { Service_period_no = s.Service_period_no, Service_period = s.Service_period };
            foreach (var p in FP)
            {
                VP.Add(new Volunteer_Schedule_PeriodModel()
                {
                    Service_period_no = p.Service_period_no,
                    Service_period = p.Service_period
                });
            }
            return VP;
        }
    }
}