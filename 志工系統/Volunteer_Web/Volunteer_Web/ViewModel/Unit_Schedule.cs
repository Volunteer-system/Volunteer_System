using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Volunteer_Web.Models;

namespace Volunteer_Web.ViewModel
{
    public class Unit_Schedule
    {
        public Shift_schedule Schedule { get; set; }
        public IEnumerable<Shift_schedule> Schedules { get; set; }
        public Application_unit Applicationunit{ get; set; }
        public Service_period1 period1 { get; set; }
        public IEnumerable<Volunteer> volunteers { get; set; }
        public Volunteer volunteer { get; set; }


        //    public Unit_Schedule GetSchedule(int volunteer_no, string Service_period_name, string day,)
        //    {
        //        Unit_Schedule US= new Unit_Schedule();
        //        using (VolunteerEntities db = new VolunteerEntities())
        //        {
        //            var s = (from ss in db.Shift_schedule
        //                     join sp1 in db.Service_period1 on ss.Service_period_no equals sp1.Service_period_no
        //                     where ss.Application_unit_no==Session["UserID"]
        //                     select new { Service_period_name = sp1.Service_period, Srevice_group_name = sp.Service_group.Group_name, Application_unit_name = sp.Application_unit1.Application_unit1 }).FirstOrDefault();
        //US.Volunteer.Volunteer_no = volunteer_no;
        //            US.period1..Service_period_name = s.Service_period_name;
        //            US.Srevice_group_name = s.Srevice_group_name;
        //            US.Application_unit_name = s.Application_unit_name;
        //            US.Worktime =Convert.ToDateTime(day);
        //        }
        //            return US;
        //    }

    }
}