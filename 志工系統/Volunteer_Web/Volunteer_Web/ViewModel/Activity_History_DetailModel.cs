using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Volunteer_Web.Models;

namespace Volunteer_Web.ViewModel
{
    public class Activity_History_DetailModel
    {
        public int Activity_no { get; set; }
        public string Activity_name { get; set; }
        public string Activity_type { get; set; }
        public string Group_name { get; set; }
        public string supervision_Name { get; set; }
        public int? supervision_phone { get; set; }
        public string supervision_email { get; set; }
        public string lecturer { get; set; }
        public string Place { get; set; }
        //public int Activity_type_ID { get; set; }
        //public Nullable<int> Group_no { get; set; }
        public string Activity_startdate { get; set; }
        public string Activity_enddate { get; set; }
        public string Summary { get; set; }//Activity_photo
        public int Activity_photo_id { get; set; }
        public List<int> GetPhotoID(int no)
        {
            List<int> photo = new List<int>();
            using (VolunteerEntities db = new VolunteerEntities())
            {             
                var q = from p in db.Activity_photo
                        where p.Activity_id == no
                        select new { Activity_photo_id = p.Activity_photo_id };
                foreach (var p in q)
                {
                    photo.Add(p.Activity_photo_id);
                }
            }
            return photo;
        }
        public List<Activity_History_DetailModel> GetActivies(int id)
        {
            List<Activity_History_DetailModel> AHD = new List<Activity_History_DetailModel>();
            using (VolunteerEntities db = new VolunteerEntities())
            {               
                var acts = from a in db.Activities
                           join at in db.Activity_type on a.Activity_type_ID equals at.Activity_type_ID
                           join g in db.Service_group on a.Group_no equals g.Group_no
                           join s in db.Volunteer_supervision on a.Undertaker equals s.supervision_ID
                           where a.Activity_no == id
                           select new { Activity_no = a.Activity_no, Activity_name = a.Activity_name, Activity_type1 = at.Activity_type1, Group_name = g.Group_name, supervision_Name = s.supervision_Name, supervision_phone = s.supervision_phone, supervision_email = s.supervision_email, lecturer = a.lecturer, Place = a.Place, Activity_startdate = a.Activity_startdate, Activity_enddate = a.Activity_enddate, Summary = a.Summary };
                foreach (var a in acts)
                {
                    AHD.Add(new Activity_History_DetailModel()
                    {
                        Activity_no = a.Activity_no,
                        Activity_name = a.Activity_name,
                        Activity_type = a.Activity_type1,
                        Group_name = a.Group_name,
                        supervision_Name = a.supervision_Name,
                        supervision_phone = a.supervision_phone,
                        supervision_email = a.supervision_email,
                        lecturer = a.lecturer,
                        Place = a.Place,
                        Activity_startdate = a.Activity_startdate.Value.ToShortDateString(),
                        Activity_enddate = a.Activity_enddate.Value.ToShortDateString(),
                        Summary = a.Summary
                    });
                }
            }
            return AHD;
        }
        //public Nullable<int> Undertaker { get; set; }
        //public string Undertake_phone { get; set; }
        //public string Undertake_email { get; set; }
        //public string lecturer { get; set; }
        //public Nullable<int> Member { get; set; }
        //public Nullable<int> Spare { get; set; }
        //public string Place { get; set; }
        //public string Summary { get; set; }
        //public Nullable<int> Activity_Photo_id { get; set; }
        //public Nullable<bool> Meal { get; set; }
    }
}