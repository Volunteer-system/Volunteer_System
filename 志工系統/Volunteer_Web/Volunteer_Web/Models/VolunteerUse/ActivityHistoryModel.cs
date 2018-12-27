using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Volunteer_Web.Models.VolunteerUse
{
    public class ActivityHistoryModel
    {
        public int Activity_no { get; set; }
        public string Activity_name { get; set; }

        public Nullable<System.DateTime> Activity_startdate { get; set; }
        public Nullable<System.DateTime> Activity_enddate { get; set; }
        public Nullable<int> Member { get; set; }


        public string Stage { get; set; }

        public Nullable<int> Activity_photo_id { get; set; }

        public List<ActivityHistoryModel> GetHistoryActivity(int id)
        {           
            List<ActivityHistoryModel> LAHM = new List<ActivityHistoryModel>();
            using (VolunteerEntities db = new VolunteerEntities())
            {
                var allactivity = from v in db.Activity1
                                  join a in db.Activities
                                  on v.Activity_no equals a.Activity_no
                                  //join s in db.Stages
                                  //on v.Stage equals s.Stage_ID
                                  where v.Volunteer_no == id
                                  select new { a.Activity_no, a.Activity_name, a.Activity_startdate, a.Member, a.Activity_Photo_id, v.Stage, a.Activity_enddate/*,s.Stage1*/ };
                foreach (var a in allactivity)
                {
                    LAHM.Add(new ActivityHistoryModel()
                    {
                        Activity_no = a.Activity_no,
                        Activity_name = a.Activity_name,
                        Activity_startdate = a.Activity_startdate,
                        Activity_enddate = a.Activity_enddate,
                        Member = a.Member,
                        Stage = a.Stage,
                        Activity_photo_id = a.Activity_Photo_id
                    });
                }
            }
            return LAHM;
        }       
    }
}