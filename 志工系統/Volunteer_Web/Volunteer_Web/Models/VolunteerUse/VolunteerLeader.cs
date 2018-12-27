using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Volunteer_Web.Models.VolunteerUse
{
    public class VolunteerLeader
    {
        public int Position_no { get; set; }

        public string Position_chinese { get; set; }

        public string Position_english { get; set; }

        public string Duties { get; set; }

        public Nullable<System.DateTime> Takeoffice_date { get; set; }

        public Nullable<System.DateTime> Resignation_date { get; set; }

        public List<VolunteerLeader> GetVolunteerLeader(int id)
        {
            List<VolunteerLeader> leaders = new List<VolunteerLeader>();
            using (VolunteerEntities db = new VolunteerEntities())
            {
                var leader = from vl in db.Leaders1
                             join lb in db.Leaders on vl.Position_no equals lb.Position_no
                             where vl.Volunteer_no == id
                             select new { lb.Position_chinese, lb.Position_english, lb.Duties, vl.Takeoffice_date, vl.Resignation_date };
                foreach (var l in leader)
                {
                    leaders.Add(new VolunteerLeader()
                    {
                        Position_chinese = l.Position_chinese,
                        Position_english = l.Position_english,
                        Duties = l.Duties,
                        Takeoffice_date = l.Takeoffice_date,
                        Resignation_date = l.Resignation_date
                    });
                }
            }
            return leaders;
        }
    }
}