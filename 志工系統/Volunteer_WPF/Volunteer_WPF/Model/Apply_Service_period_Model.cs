using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer_WPF.Model
{
    class Apply_Service_period_Model
    {

        public void InsertService_period(int apply_id)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n1 in dbContext.Apply_Service_period
                    join n2 in dbContext.Manpower_apply
                    on n1.Apply_ID equals n2.Apply_ID
                    where n1.Apply_ID == apply_id
                    select new
                    {
                        Service_period_no = n1.Service_period_no,
                        Volunteer_number = n1.Volunteer_number,
                        Application_unit_no = n2.Application_unit_no
                    };

            foreach (var row in q)
            {
                var q2 = (from n in dbContext.Service_period
                          where n.Application_unit_no == row.Application_unit_no
                          select n).ToList();

                var _period = q2.Where(p => p.Application_unit_no == row.Application_unit_no && p.Service_period_no == row.Service_period_no);

                if (_period.Count() == 0)
                {
                    Service_period service_Period = new Service_period();
                    service_Period.Application_unit_no = row.Application_unit_no;
                    service_Period.Service_period_no = row.Service_period_no;
                    service_Period.Volunteer_number = row.Volunteer_number;
                    dbContext.Service_period.Add(service_Period);
                }
                else
                {
                    _period.First().Volunteer_number = row.Volunteer_number;
                }                
            }

            dbContext.SaveChanges();
        }
    }
}
