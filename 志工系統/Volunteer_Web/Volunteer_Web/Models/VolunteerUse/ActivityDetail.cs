using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Volunteer_Web.Models.VolunteerUse
{
    public class ActivityDetail
    {

        public int Activity_no { get; set; }
        public string Activity_name { get; set; }

        public string Activity_type { get; set; }

        public string GroupName { get; set; }

        public Nullable<System.DateTime> Activity_startdate { get; set; }

        public Nullable<System.DateTime> Activity_enddate { get; set; }

        public string Undertake_unit { get; set; }

        //public Nullable<int> Undertaker { get; set; }

        public string Undertake_phone { get; set; }

        public string Undertake_email { get; set; }

        public string lecturer { get; set; }

        public Nullable<int> Member { get; set; }

        public Nullable<int> Spare { get; set; }

        public string Place { get; set; }

        public string Summary { get; set; }

        public Nullable<int> Activity_photo_id { get; set; }
        //public List<Activity_photo> activity_photo { get; set; }
        public List<Activity_photo> GetActivityPhoto(int act_no)
        {
            List<Activity_photo> LAP = new List<Activity_photo>();
            using (VolunteerEntities db = new VolunteerEntities()) { 
                var q = db.Activity_photo.Where(n => n.Activity_id == act_no).Select(n => n);
            foreach (var p in q)
            {
                LAP.Add(new Activity_photo()
                {
                    Activity_photo_id = p.Activity_photo_id,
                    Activity_photo1 = p.Activity_photo1
                });
            }
            }
            return LAP;
        }
        public ActivityDetail GetActivity(int no)
        {
            ActivityDetail aa = null;
            using (VolunteerEntities db = new VolunteerEntities())
            {
                //List<ActivityDetail> LAD = new List<ActivityDetail>();
                var activity = (from a in db.Activities
                                join at in db.Activity_type on a.Activity_type_ID equals at.Activity_type_ID
                                join gp in db.Service_group on a.Group_no equals gp.Group_no
                                join sv in db.Volunteer_supervision on a.Undertaker equals sv.supervision_ID
                                //join p in db.Activity_photo on a.Activity_Photo_id equals p.Activity_photo_id
                                where a.Activity_no == no
                                select new { a.Activity_name, at.Activity_type1, gp.Group_name, a.Activity_startdate, a.Activity_enddate, sv.supervision_Name, sv.supervision_phone, sv.supervision_email, a.lecturer, a.Member, a.Spare, a.Place, a.Summary, a.Activity_Photo_id }).ToList();
                foreach (var a in activity)
                {
                    aa = new ActivityDetail()
                    {
                        Activity_name = a.Activity_name,
                        Activity_type = a.Activity_type1,
                        GroupName = a.Group_name,
                        Activity_startdate = a.Activity_startdate,
                        Activity_enddate = a.Activity_enddate,
                        Undertake_unit = a.supervision_Name,
                        Undertake_email = a.supervision_email,
                        Undertake_phone = a.supervision_phone.ToString(),
                        lecturer = a.lecturer,
                        Member = a.Member,
                        Spare = a.Spare,
                        Place = a.Place,
                        Summary = a.Summary,
                        Activity_photo_id = a.Activity_Photo_id
                    };
                }
            }
            return aa;
        }
    }
}