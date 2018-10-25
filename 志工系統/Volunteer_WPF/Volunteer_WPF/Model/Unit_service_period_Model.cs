using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer_WPF.Model
{
    class Unit_service_period_Model
    {
        public string Service_period { get; set; }

        public List<string> SelectUnit_service_period_byApplication_unit_no(int Application_unit_no)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n1 in dbContext.Service_period
                    join n2 in dbContext.Service_period1
                    on n1.Service_period_no equals n2.Service_period_no
                    where n1.Application_unit_no == Application_unit_no
                    select new
                    {
                        Service_period = n2.Service_period
                    };

            List<string> Unit_service_periods = new List<string>();
            foreach (var row in q)
            {
                Unit_service_periods.Add(row.Service_period);
            }

            return Unit_service_periods;
        }
    }
}
