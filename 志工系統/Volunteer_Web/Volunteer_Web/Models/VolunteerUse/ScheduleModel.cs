using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Volunteer_Web.Models.VolunteerUse
{
    public class ScheduleModel
    {
        public int Volunteer_no { get; set; }
        public int Service_period_no { get; set; }
        public string Service_period_name { get; set; }
        public Nullable<int> Stage { get; set; }
        public string Stage_name { get; set; }
        public Nullable<int> Srevice_group { get; set; }
        public string Srevice_group_name { get; set; }
        public Nullable<int> Application_unit { get; set; }
        public string Application_unit_name { get; set; }
        public List<ScheduleModel> GetSchedule(int id)
        {    List<ScheduleModel> LS = new List<ScheduleModel>();
            using (VolunteerEntities db = new VolunteerEntities())
            {
                
                var q = from s in db.Service_period2
                        join st in db.Stages on s.Stage equals st.Stage_ID
                        join sp in db.Service_period1 on s.Service_period_no equals sp.Service_period_no
                        join sg in db.Service_group on s.Srevice_group equals sg.Group_no
                        join sa in db.Application_unit on s.Application_unit equals sa.Application_unit_no
                        where s.Volunteer_no == id && st.Stage_type == "排班意願"
                        select new
                        {
                            Volunteer_no = s.Volunteer_no,
                            Service_period_no = s.Service_period_no,
                            Service_period_name = sp.Service_period,
                            Stage = s.Stage,
                            Stage_name = st.Stage1,
                            Srevice_group = s.Srevice_group,
                            Srevice_group_name = sg.Group_name,
                            Application_unit = s.Application_unit,
                            Application_unit_name = sa.Application_unit1
                        };
                foreach (var s in q)
                {
                    LS.Add(new ScheduleModel()
                    {
                        Volunteer_no = s.Volunteer_no,
                        Service_period_no = s.Service_period_no,
                        Service_period_name = s.Service_period_name,
                        Srevice_group = s.Srevice_group,
                        Srevice_group_name = s.Srevice_group_name,
                        Application_unit = s.Application_unit,
                        Application_unit_name = s.Application_unit_name,
                        Stage = s.Stage,
                        Stage_name = s.Stage_name
                    });
                }
            }
            return LS;
        }
        //public void Insert_Volunteer_Service_period(List<ScheduleModel> SM)
        //{
        //    VolunteerEntities db = new VolunteerEntities();
        //    for (int i = 0; i < SM.Count; i += 1)
        //    {
        //        var stage_type = SM[i].GStage;
        //        if (stage_type != "續留")
        //        {
        //            var stage = (from s in db.Stages
        //                         where s.Stage1 == stage_type
        //                         select s.Stage_ID).ToList();
        //            Service_period2 sm = new Service_period2()
        //            {
        //                Volunteer_no = SM[i].Volunteer_no,
        //                Service_period_no = SM[i].Service_period_no,
        //                Srevice_group = SM[i].Srevice_group,
        //                Application_unit = SM[i].Application_unit,
        //                Stage = stage[0]
        //            };
        //            db.Service_period2.Add(sm);
        //        }
        //    }
        //    db.SaveChanges();
        //}
    }
}