using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer_WPF.Model
{
    class Service_period_volunteer_Model
    {
        public int Volunteer_no { get; set; }
        public string Volunteer { get; set; }
        public string Type { get; set; }
        public byte[] Photo { get; set; }
        public string Stage { get; set; }

        public List<Service_period_volunteer_Model> SelectService_period_volunteer(int application_unit_no, int service_period_no, string stage_type)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n1 in dbContext.Service_period2
                    join n2 in dbContext.Volunteer
                    on n1.Volunteer_no equals n2.Volunteer_no
                    join n3 in dbContext.Stages
                    on n1.Stage equals n3.Stage_ID
                    join n4 in dbContext.Identity_type
                    on n2.Identity_type equals n4.Identity_type1
                    where n1.Application_unit == application_unit_no &&
                          n1.Service_period_no == service_period_no &&
                          n3.Stage1 == stage_type &&
                          n3.Stage_type == "排班意願"
                    select new
                    {
                        Volunteer_no = n1.Volunteer_no,
                        Volunteer = n2.Chinese_name,
                        Type = n4.Identity_type_name,
                        Photo = n2.Photo,
                        Stage = n3.Stage1
                    };

            List<Service_period_volunteer_Model> service_Period_Volunteer_Models = new List<Service_period_volunteer_Model>();
            foreach (var row in q)
            {
                Service_period_volunteer_Model service_Period_Volunteer_Model = new Service_period_volunteer_Model();
                service_Period_Volunteer_Model.Volunteer_no = row.Volunteer_no;
                service_Period_Volunteer_Model.Volunteer = row.Volunteer;
                service_Period_Volunteer_Model.Type = row.Type;
                service_Period_Volunteer_Model.Photo = row.Photo;
                service_Period_Volunteer_Model.Stage = row.Stage;

                service_Period_Volunteer_Models.Add(service_Period_Volunteer_Model);
            }

            return service_Period_Volunteer_Models;
        }

        //要傳入資料清單、運用單位、服務時段
        List<Service_period_volunteer> update_list = new List<Service_period_volunteer>();
        List<Service_period_volunteer> insert_list = new List<Service_period_volunteer>();
        public void UpdateService_period_volunteer(List<Service_period_volunteer_Model> stay_Period_Volunteers, List<Service_period_volunteer_Model> new_Period_Volunteers, List<Service_period_volunteer_Model> leave_Period_Volunteers, int unit_id, int service_period_no)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var Stage_nos = (from n in dbContext.Stages
                             where n.Stage_type == "排班意願"
                             select n).ToList();

            var q = (from n1 in dbContext.Service_period2
                     join n2 in dbContext.Stages
                     on n1.Stage equals n2.Stage_ID
                     where n1.Service_period_no == service_period_no &&
                           n1.Application_unit == unit_id
                     select new Service_period_volunteer
                     {
                         Volunteer_no = n1.Volunteer_no,
                         Stage_id = (int)n1.Stage,
                         Stage = n2.Stage1
                     }).ToList();

            SwitchService_period_volunteer(stay_Period_Volunteers,q, Stage_nos);
            SwitchService_period_volunteer(new_Period_Volunteers, q, Stage_nos);
            SwitchService_period_volunteer(leave_Period_Volunteers, q, Stage_nos);

            foreach (var row in update_list)
            {
                var q2 = (from n in dbContext.Service_period2
                          where n.Volunteer_no == row.Volunteer_no &&
                                n.Service_period_no == service_period_no &&
                                n.Application_unit == unit_id
                          select n).First();
                q2.Stage = row.Stage_id;
            }

            foreach (var row in insert_list)
            {
                Service_period2 service_Period2 = new Service_period2();
                service_Period2.Volunteer_no = row.Volunteer_no;
                service_Period2.Service_period_no = service_period_no;
                service_Period2.Application_unit = unit_id;
                service_Period2.Stage = row.Stage_id;

                dbContext.Service_period2.Add(service_Period2);
            }

            dbContext.SaveChanges();
        }

        public void SwitchService_period_volunteer(List<Service_period_volunteer_Model> Period_Volunteers, List<Service_period_volunteer> q, List<Stage> stage_nos)
        {
            foreach (var row in Period_Volunteers)
            {
                if (q.Where(p => p.Volunteer_no == row.Volunteer_no && p.Stage == row.Stage).Count() > 0)
                {
                    continue;
                }
                else if (q.Where(p => p.Volunteer_no == row.Volunteer_no).Count() > 0)
                {
                    Service_period_volunteer service_Period_Volunteer = new Service_period_volunteer();
                    service_Period_Volunteer.Volunteer_no = row.Volunteer_no;
                    service_Period_Volunteer.Stage_id = stage_nos.Where(p => p.Stage1 == row.Stage).Select(p => p.Stage_ID).First();
                    service_Period_Volunteer.Stage = row.Stage;
                    update_list.Add(service_Period_Volunteer);
                }
                else
                {
                    Service_period_volunteer service_Period_Volunteer = new Service_period_volunteer();
                    service_Period_Volunteer.Volunteer_no = row.Volunteer_no;
                    service_Period_Volunteer.Stage_id = stage_nos.Where(p => p.Stage1 == row.Stage).Select(p => p.Stage_ID).First();
                    service_Period_Volunteer.Stage = row.Stage;
                    insert_list.Add(service_Period_Volunteer);
                }
            }
        }
    }

    class Service_period_volunteer
    {
        public int Volunteer_no { get; set; }
        public int Stage_id { get; set; }
        public string Stage { get; set; }
    }
}
