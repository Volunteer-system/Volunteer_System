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

        public void InsertUnit_service_period(int unit_no,List<Unit_service_period_Model> unit_Service_Period_Models)
        {
            List<Unit_service_period_Model> Service_Period_nos = new List<Unit_service_period_Model>();
            VolunteerEntities dbContext = new VolunteerEntities();
            foreach (var row in unit_Service_Period_Models)
            {
                var q1 = from n in dbContext.Service_period1
                        where n.Service_period == row.Service_period
                        select n;
                foreach (var row1 in q1)
                {
                    Unit_service_period_Model unit_Service_Period_Model = new Unit_service_period_Model();
                    unit_Service_Period_Model.Volunteer_number = row.Volunteer_number;
                    unit_Service_Period_Model.Service_period = row1.Service_period_no.ToString();
                    Service_Period_nos.Add(unit_Service_Period_Model);
                }
            }

            var q = from n in dbContext.Service_period
                    where n.Application_unit_no == unit_no
                    select n;

            foreach (var row1 in q)
            {
                foreach (var row2 in Service_Period_nos)
                {
                    if (row1.Service_period_no.ToString() == row2.Service_period)
                    {
                        row1.Volunteer_number = int.Parse(row2.Volunteer_number);
                    }
                }
            }

            dbContext.SaveChanges();
        }

        public void DeleteUnit_service_period(int unit_no,List<Unit_service_period_Model> unit_Service_Period_Models)
        {
            List<int> Service_Period_nos = new List<int>();
            VolunteerEntities dbContext = new VolunteerEntities();
            foreach (var row in unit_Service_Period_Models)
            {
                var q = from n in dbContext.Service_period1
                        where n.Service_period == row.Service_period
                        select n;
                foreach (var row1 in q)
                {
                    Service_Period_nos.Add(row1.Service_period_no);
                }
            }

            dbContext.Service_period.ToList();
            foreach (var row in Service_Period_nos)
            {
                var Unit_Service_period = (from n in dbContext.Service_period
                                           where n.Service_period_no == row && n.Application_unit_no == unit_no
                                           select n).FirstOrDefault();
                dbContext.Service_period.Remove(Unit_Service_period);
            }

            dbContext.SaveChanges();
        }
    }
}
