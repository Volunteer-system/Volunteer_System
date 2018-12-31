using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer_WPF.Model
{
    class Shift_schedule_Model
    {

        public void UpdateShift_schedule(int unit_id, int service_period_no)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q1 = (from n1 in dbContext.Service_period2
                     join n2 in dbContext.Stages
                     on n1.Stage equals n2.Stage_ID
                     where (n2.Stage1 == "續留" || n2.Stage1 == "新申請") &&
                           n1.Application_unit == unit_id &&
                           n1.Service_period_no == service_period_no
                     select n1).ToList();

            var q2 = (from n in dbContext.Shift_schedule
                      where n.Application_unit_no == unit_id &&
                            n.Service_period_no == service_period_no
                      select n).ToList();

            foreach (var row in q1)
            {
                if (q2.Where(p => p.Volunteer_no == row.Volunteer_no).Count() > 0)
                {
                    continue;
                }
                else
                {
                    Shift_schedule shift_Schedule = new Shift_schedule();
                    shift_Schedule.Volunteer_no = row.Volunteer_no;
                    shift_Schedule.Application_unit_no = (int)row.Application_unit;
                    shift_Schedule.Service_period_no = row.Service_period_no;
                    shift_Schedule.year = DateTime.Now.Year;
                    shift_Schedule.Wish_order = 1;
                    dbContext.Shift_schedule.Add(shift_Schedule);
                }
            }

            foreach (var row in q2)
            {
                if (q1.Where(p => p.Volunteer_no == row.Volunteer_no).Count() > 0)
                {
                    continue;
                }
                else
                {
                    var schedule = (from n in dbContext.Shift_schedule
                                    where n.Application_unit_no == unit_id &&
                                          n.Service_period_no == service_period_no &&
                                          n.Volunteer_no == row.Volunteer_no
                                    select n).FirstOrDefault();
                    dbContext.Shift_schedule.Remove(schedule);
                }
            }

            dbContext.SaveChanges();
        }
    }
}
