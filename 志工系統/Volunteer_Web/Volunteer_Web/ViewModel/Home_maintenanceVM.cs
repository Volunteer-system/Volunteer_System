using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Volunteer_Web.Models;

namespace Volunteer_Web.ViewModel
{
    public class Home_maintenanceVM
    {
        public IEnumerable<Indexphoto_VM> Indexphotos { get; set; }
        public IEnumerable<Indexvideolink_VM> Indexvideolinks { get; set; }
        public IEnumerable<Experience_VM> Experiences { get; set; }

        public void SelectHome_maintenance()
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q1 = from n in dbContext.Indexphotoes
                    select n;
            List<Indexphoto_VM> indexphotos = new List<Indexphoto_VM>();
            foreach (var row in q1)
            {
                Indexphoto_VM indexphoto = new Indexphoto_VM();
                indexphoto.Indexphoto_no = row.Indexphoto_no;
                indexphoto.Indexphoto_name = row.Indexphoto1;
                indexphoto.Issued = (bool)row.Issued;
                indexphotos.Add(indexphoto);
            }
            Indexphotos = indexphotos;

            var q2 = from n in dbContext.Indexvideolinks
                     select n;
            List<Indexvideolink_VM> indexvideolinks = new List<Indexvideolink_VM>();
            foreach (var row in q2)
            {
                Indexvideolink_VM indexvideolink = new Indexvideolink_VM();
                indexvideolink.Indexvideolink_no = row.Indexvideolink_no;
                indexvideolink.Videolink = row.Videolink;
                indexvideolink.Issued = (bool)row.Issued;
                indexvideolinks.Add(indexvideolink);
            }
            Indexvideolinks = indexvideolinks;

            var q3 = from n1 in dbContext.Experiences
                    join n2 in dbContext.Volunteers
                    on n1.Volunteer_no equals n2.Volunteer_no
                    select new
                    {
                        Experience_no = n1.Experience_no,
                        Experience = n1.Experience1,
                        Volunteer = n2.Chinese_name,
                        Experience_content = n1.Experience_content,
                        Experience_photo = n1.Experience_photo,
                        Issued = n1.Issued
                    };
            List<Experience_VM> experiences = new List<Experience_VM>();
            foreach (var row in q3)
            {
                Experience_VM experience = new Experience_VM();
                experience.Experience_no = row.Experience_no;
                experience.Experience = row.Experience;
                experience.Volunteer = row.Volunteer;
                experience.Experience_content = row.Experience_content;
                experience.Experience_photo = row.Experience_photo;
                experience.Issued = (bool)row.Issued;
                experiences.Add(experience);
            }
            Experiences = experiences;
        }
    }

    public class Indexphoto_VM
    {
        public int Indexphoto_no { get; set; }
        public string Indexphoto_name { get; set; }
        public bool Issued { get; set; }
    }

    public class Indexvideolink_VM
    {
        public int Indexvideolink_no { get; set; }
        public string Videolink { get; set; }
        public bool Issued { get; set; }
    }
}