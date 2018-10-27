using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer_WPF.Model
{
    class Unit_service_period_Model
    {
        //服務組別
        public string Service_period { get; set; }
        //人數
        public string Volunteer_number { get; set; }

        public List<Unit_service_period_Model> SelectUnit_service_period_byApplication_unit_no(int Application_unit_no)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n1 in dbContext.Service_period
                    join n2 in dbContext.Service_period1
                    on n1.Service_period_no equals n2.Service_period_no
                    where n1.Application_unit_no == Application_unit_no
                    select new
                    {
                        Service_period = n2.Service_period,
                        Volunteer_number = n1.Volunteer_number
                    };

            List<Unit_service_period_Model> Unit_service_periods = new List<Unit_service_period_Model>();
            foreach (var row in q)
            {
                Unit_service_period_Model unit_Service_Period_Model = new Unit_service_period_Model();
                unit_Service_Period_Model.Service_period = row.Service_period;
                unit_Service_Period_Model.Volunteer_number = row.Volunteer_number.ToString();
                Unit_service_periods.Add(unit_Service_Period_Model);
            }

            return Unit_service_periods;
        }
    }
}
