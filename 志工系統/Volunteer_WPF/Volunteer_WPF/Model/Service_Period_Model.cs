using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer_WPF.Model
{
    class Service_Period_Model
    {
        public int Service_period_no { get; set; }
        public string Service_period { get; set; }
        public string Time { get; set; }

        public void SelectService_periodby_Service_period_no(int Service_period_no)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n in dbContext.Service_period1
                    where n.Service_period_no == Service_period_no
                    select n;

            foreach (var row in q)
            {
                Service_period_no = row.Service_period_no;
                Service_period = row.Service_period;
                Time = row.Time;
            }

        }
    }
}
