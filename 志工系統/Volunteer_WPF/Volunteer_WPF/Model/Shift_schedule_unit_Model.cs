using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer_WPF.Model
{
    class Shift_schedule_unit_Model
    {
        public int Unit_no { get; set; }
        public int Service_period_no { get; set; }
        public string Service_period { get; set; }
        public string Time { get; set; }
        public int Actual_number { get; set; }
        public int Volunteer_number { get; set; }

        public List<Shift_schedule_unit_Model> Selectshift_schedule_unit_byunit_id(int unit_id)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n1 in dbContext.Service_period
                    join n2 in dbContext.Service_period1
                    on n1.Service_period_no equals n2.Service_period_no
                    where n1.Application_unit_no == unit_id
                    select new
                    {
                        Unit_no = n1.Application_unit_no,
                        Service_period_no = n1.Service_period_no,
                        Service_period = n2.Service_period,
                        Time = n2.Time,
                        Volunteer_number = n1.Volunteer_number
                    };

            List<Shift_schedule_unit_Model> shift_Schedule_Units = new List<Shift_schedule_unit_Model>();
            foreach (var row in q)
            {
                Shift_schedule_unit_Model shift_Schedule_Unit = new Shift_schedule_unit_Model();
                shift_Schedule_Unit.Unit_no = row.Unit_no;
                shift_Schedule_Unit.Service_period_no = row.Service_period_no;
                shift_Schedule_Unit.Service_period = row.Service_period;
                shift_Schedule_Unit.Time = row.Time;
                shift_Schedule_Unit.Volunteer_number = (int)row.Volunteer_number;

                var count = (from n1 in dbContext.Shift_schedule
                             join n2 in dbContext.Service_period1
                             on n1.Service_period_no equals n2.Service_period_no
                             where n1.Application_unit_no == unit_id &&
                                   n2.Service_period == row.Service_period &&
                                   n2.Time == row.Time
                             select n1).Count();
                shift_Schedule_Unit.Actual_number = count;

                shift_Schedule_Units.Add(shift_Schedule_Unit);
            }

            return shift_Schedule_Units;
        }        
    }
}
