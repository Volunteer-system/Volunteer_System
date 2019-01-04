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
        public Volunteer_Index_Schedule GetSchedule(int volunteer_no, string Service_period_name,string day)
        {
            Volunteer_Index_Schedule VIS = new Volunteer_Index_Schedule();
            using (VolunteerEntities db = new VolunteerEntities())
            {
                var s = (from sp in db.Service_period2
                        join sp1 in db.Service_period1 on sp.Service_period_no equals sp1.Service_period_no
                        where sp.Volunteer_no == volunteer_no && sp1.Service_period == Service_period_name
                        select new { Service_period_name = sp1.Service_period, Srevice_group_name = sp.Service_group.Group_name, Application_unit_name = sp.Application_unit1.Application_unit1 }).FirstOrDefault();
                VIS.Volunteer_no = volunteer_no;
                VIS.Service_period_name = s.Service_period_name;
                VIS.Srevice_group_name = s.Srevice_group_name;
                VIS.Application_unit_name = s.Application_unit_name;
                VIS.Worktime =Convert.ToDateTime(day);
            }
                return VIS;
        }
        public List<Volunteer_Index_Schedule> GetWorkTime(int userid, string fmonth)
        {  
            List<Volunteer_Index_Schedule> LVIS = new List<Volunteer_Index_Schedule>();
            using (VolunteerEntities db = new VolunteerEntities())
            {
                var fa = (from s in db.Service_period2
                         where s.Volunteer_no == userid && db.Stages.Where(sg=>s.Stage == sg.Stage_ID && sg.Stage1=="續留"&& sg.Stage_type=="排班意願").Any()
                         select new { Service_period_name = s.Service_period1.Service_period, Srevice_group_name = s.Service_group.Group_name, Application_unit_name = s.Application_unit1.Application_unit1 }).ToList();
                for (int i = 0; i < fa.Count; i += 1)
                {
                    List<DateTime> alldate = new List<DateTime>();
                    switch (fa[i].Service_period_name.Substring(2,1)) {
                        case "一":
                            alldate = getAllMonthTheDay(1, fmonth);
                            break;
                        case "二":
                            alldate = getAllMonthTheDay(2, fmonth);
                            break;
                        case "三":
                            alldate = getAllMonthTheDay(3, fmonth);
                            break;
                        case "四":
                            alldate = getAllMonthTheDay(4, fmonth);
                            break;
                        case "五":
                            alldate = getAllMonthTheDay(5, fmonth);
                            break;
                        case "六":
                            alldate = getAllMonthTheDay(6, fmonth);
                            break;
                        default:
                            alldate = getAllMonthTheDay(0, fmonth);
                            break;
                    };
                    for (int j = 0; j < alldate.Count; j += 1)
                    {
                        LVIS.Add(new Volunteer_Index_Schedule()
                        {    Volunteer_no = userid,
                            Service_period_name = fa[i].Service_period_name,
                            Srevice_group_name = fa[i].Srevice_group_name,
                            Application_unit_name = fa[i].Application_unit_name,
                            Worktime = alldate[j]
                            //TODO 查詢某日的各週日期
                        });
                    }
                }
                
            }           
            return LVIS;
        }

        private List<DateTime> getAllMonthTheDay(int v, string fmonth)
        {
            DateTime FD =Convert.ToDateTime(fmonth);//字串轉成日期
            DateTime Thefirst= FD.AddDays(1 - Convert.ToInt32(FD.DayOfWeek.ToString("d")));//取得1號的日期
            //DateTime Thelast = FD.AddMonths(1).AddDays(-1);
            DateTime NeedDayone = Thefirst.AddDays(v-1);
            List<DateTime> dt = new List<DateTime>();
            for (int i = 0;i<=5;i+=1)
            {
                int week = 7;
                dt.Add(NeedDayone.AddDays(week*i));
            }               
                return dt;
        }
    }
}