using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Volunteer_Web.Models.VolunteerUse
{
    public class WorkHoursModel
    { 
        public string Application_unit { get; set; }
        public int Service_period_no { get; set; }
        //public int year { get; set; }
        //public int Volunteer_no { get; set; }
        //public int Wish_order { get; set; }
        public List<WorkHoursModel> GetWorkHours(int id ,int year)
        {
            VolunteerEntities db = new VolunteerEntities();
            List<WorkHoursModel> LWM = new List<WorkHoursModel>();
            var works = from s in db.Shift_schedule
                        join a in db.Application_unit on s.Application_unit_no equals a.Application_unit_no
                        join v in db.Volunteers on s.Volunteer_no equals v.Volunteer_no
                        where v.Volunteer_no == id && (year == 0? true: s.year == year)
                        select new { a.Application_unit1, s.Service_period_no, s.year };
            foreach (var w in works)
            {
                LWM.Add(new WorkHoursModel()
                {
                    Application_unit = w.Application_unit1,
                    Service_period_no = w.Service_period_no,
                });
            }
            return LWM;
        }
    }
}