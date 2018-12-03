using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Volunteer_Web.Models;

namespace Volunteer_Web.ViewModel
{
    public class Visitor_Activity_VM
    {
        public IEnumerable<Activity_type> Activity_types { get; set; }
        public IEnumerable<Activity_VM> Visitor_Activity_VMs { get; set; }        

        public IEnumerable<Activity_VM> SelectActivity_byActivity_type(int Activity_type)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n1 in dbContext.Activities
                    join n2 in dbContext.Service_group
                    on n1.Group_no equals n2.Group_no
                    join n3 in dbContext.Volunteer_supervision
                    on n1.Undertaker equals n3.supervision_ID
                    where n1.Activity_type_ID == Activity_type
                    select new
                    {
                        Activity_no = n1.Activity_no,
                        Activity_name = n1.Activity_name,
                        Group_name = n2.Group_name,
                        Activity_startdate = n1.Activity_startdate,
                        Activity_enddate = n1.Activity_enddate,
                        Undertake_unit = n1.Undertake_unit,
                        Undertaker = n3.supervision_Name,
                        Undertake_phone = n3.supervision_phone,
                        Undertake_email = n3.supervision_email,
                        lecturer = n1.lecturer,
                        Place = n1.Place,
                        Summary = n1.Summary,
                        Activity_Photo_id = n1.Activity_Photo_id
                    };

            List<Activity_VM> visitor_Activity_VMs = new List<Activity_VM>();
            foreach (var row in q)
            {
                Activity_VM Activity_VM = new Activity_VM();
                Activity_VM.Activity_no = row.Activity_no;
                Activity_VM.Activity_name = row.Activity_name;
                Activity_VM.Group_name = row.Group_name;
                Activity_VM.Activity_startdate = row.Activity_startdate.ToString();
                Activity_VM.Activity_enddate = row.Activity_enddate.ToString();
                Activity_VM.Undertaker = row.Undertaker;
                Activity_VM.Undertake_unit = row.Undertake_unit;
                Activity_VM.Undertake_phone = row.Undertake_phone.ToString();
                Activity_VM.Undertake_email = row.Undertake_email;
                Activity_VM.lecturer = row.lecturer;
                Activity_VM.Place = row.Place;
                Activity_VM.Summary = row.Summary;
                Activity_VM.Activity_Photo_id = (int)row.Activity_Photo_id;

                visitor_Activity_VMs.Add(Activity_VM);
            }

            IEnumerable<Activity_VM> Activity_VMs = visitor_Activity_VMs;
            return Activity_VMs;
        }
    }

    public class Activity_VM
    {
        public int Activity_no { get; set; }
        public string Activity_name { get; set; }
        public int Activity_type_ID { get; set; }
        public string Group_name { get; set; }
        public string Activity_startdate { get; set; }
        public string Activity_enddate { get; set; }
        public string Undertake_unit { get; set; }
        public string Undertaker { get; set; }
        public string Undertake_phone { get; set; }
        public string Undertake_email { get; set; }
        public string lecturer { get; set; }
        public Nullable<int> Member { get; set; }
        public Nullable<int> Spare { get; set; }
        public string Place { get; set; }
        public string Summary { get; set; }
        public int Activity_Photo_id { get; set; }
    }
}