using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Volunteer_Web.Models;

namespace Volunteer_Web.ViewModel
{
    public class Volunteer_Schedule_applicationModel
    {
        public int Application_unit_no { get; set; }

        public string Application_unit1 { get; set; }
        public List<Volunteer_Schedule_applicationModel> Getapplication()
        { VolunteerEntities db = new VolunteerEntities();
            List<Volunteer_Schedule_applicationModel> VS = new List<Volunteer_Schedule_applicationModel>();
            var FV = from A in db.Application_unit
                     select new { Application_unit_no = A.Application_unit_no, Application_unit1 = A.Application_unit1 };
            foreach (var v in FV)
            {
                VS.Add(new Volunteer_Schedule_applicationModel()
                {
                    Application_unit_no = v.Application_unit_no,
                    Application_unit1 = v.Application_unit1
                });
            }
            return VS;
        }
    }
}