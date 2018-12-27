using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Volunteer_Web.Models;

namespace Volunteer_Web.ViewModel
{
    public class Volunteer_Schedule_saveModel
    {
        public int Volunteer_no { get; set; }
        public int Service_period_no { get; set; }
        //public Nullable<int> Stage { get; set; }
        public string GStage { get; set; }
        public Nullable<int> Srevice_group { get; set; }
        public Nullable<int> Application_unit { get; set; }
        public void Insert_Volunteer_Service_period(List<Volunteer_Schedule_saveModel> SM)
        {
            using (VolunteerEntities db = new VolunteerEntities())
            {
                for (int i = 0; i < SM.Count; i += 1)
                {
                    var stage_type = SM[i].GStage;
                    if (stage_type != "續留")
                    {
                        var stage = (from s in db.Stages
                                     where s.Stage1 == stage_type && s.Stage_type == "排班意願"
                                     select s.Stage_ID).ToList();
                        Service_period2 sm = new Service_period2()
                        {
                            Volunteer_no = SM[i].Volunteer_no,
                            Service_period_no = SM[i].Service_period_no,
                            Srevice_group = SM[i].Srevice_group,
                            Application_unit = SM[i].Application_unit,
                            Stage = stage[0]
                        };
                        db.Service_period2.Add(sm);
                    }
                }
                db.SaveChanges();
            }
        }
    }
}