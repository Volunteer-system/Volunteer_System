using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Volunteer_Web.Models;

namespace Volunteer_Web.ViewModel
{
    public class Volunteer_Index_Schedule
    {
        public int Volunteer_no { get; set; }
        public string Service_period_name { get; set; }
        public string Srevice_group_name { get; set; }
        public string Application_unit_name { get; set; }
        public DateTime Worktime { get; set; }
        public List<Volunteer_Index_Schedule> GetWorkTime(int userid, int fmonth)
        {  
            List<Volunteer_Index_Schedule> LVIS = new List<Volunteer_Index_Schedule>();
            using (VolunteerEntities db = new VolunteerEntities())
            {
                var fa = (from s in db.Service_period2
                         where s.Volunteer_no == userid
                         select new { Service_period_name = s.Service_period1.Service_period, Srevice_group_name = s.Service_group.Group_name, Application_unit_name = s.Application_unit1.Application_unit1 }).ToList();
                for (int i = 0; i < fa.Count; i += 1)
                {  
                    LVIS.Add(new Volunteer_Index_Schedule()
                    {
                        Service_period_name = fa[i].Service_period_name,
                        Srevice_group_name = fa[i].Srevice_group_name,
                        Application_unit_name = fa[i].Application_unit_name
                        //TODO 查詢某日的各週日期
                    });
                }
                
            }           
            return LVIS;
        }
    }
}