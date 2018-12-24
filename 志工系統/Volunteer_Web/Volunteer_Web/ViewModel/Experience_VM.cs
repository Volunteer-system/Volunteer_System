using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Volunteer_Web.Models;

namespace Volunteer_Web.ViewModel
{
    public class Supervision_Experience_VM
    {
        public IEnumerable<Service_group> Service_groups { get; set; }
        public IEnumerable<Experience_VM> Experiences { get; set; }

        public IEnumerable<Experience_VM> SelectExperience_byGroup(int Group_no)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n1 in dbContext.Experiences
                    join n2 in dbContext.Volunteers
                    on n1.Volunteer_no equals n2.Volunteer_no
                    join n3 in dbContext.Service_Group1
                    on n1.Volunteer_no equals n3.Volunteer_no
                    where ((Group_no == 0) ? true : n3.Group_no == Group_no)
                    select new
                    {
                        Experience_no = n1.Experience_no,
                        Experience = n1.Experience1,
                        Volunteer = n2.Chinese_name,
                        Experience_content = n1.Experience_content,
                        Experience_photo = n1.Experience_photo,
                        Issued = n1.Issued,
                        Home_issued = n1.Home_issued
                    };
            List<Experience_VM> experience_VMs = new List<Experience_VM>();
            foreach (var row in q)
            {
                Experience_VM experience_VM = new Experience_VM();
                experience_VM.Experience_no = row.Experience_no;
                experience_VM.Experience = row.Experience;
                experience_VM.Volunteer = row.Volunteer;
                experience_VM.Experience_content = row.Experience_content;
                experience_VM.Experience_photo = row.Experience_photo;
                experience_VM.Issued = (bool)row.Issued;
                experience_VM.Home_issued = (bool)row.Home_issued;
                experience_VMs.Add(experience_VM);
            }
            IEnumerable<Experience_VM> Experience_VMs = experience_VMs;
            return Experience_VMs;
        }
    }

    public class Experience_VM
    {
        public int Experience_no { get; set; }
        public string Experience { get; set; }
        public string Volunteer { get; set; }
        public string Experience_content { get; set; }
        public string Experience_photo { get; set; }
        public bool Issued { get; set; }
        public bool Home_issued { get; set; }

        public List<Experience_VM> SelectExperience()
        {
            VolunteerEntities dbContext = new VolunteerEntities();
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
                         Issued = n1.Issued,
                         Home_issued = n1.Home_issued
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
                experience.Home_issued = (bool)row.Home_issued;
                experiences.Add(experience);
            }
            return experiences;
        }

        public IEnumerable<Experience_VM> SelectExperiencebyHome_Issued()
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n1 in dbContext.Experiences
                    join n2 in dbContext.Volunteers
                    on n1.Volunteer_no equals n2.Volunteer_no
                    where n1.Home_issued == true
                    select new
                    {
                        Experience_no = n1.Experience_no,
                        Experience = n1.Experience1,
                        Volunteer = n2.Chinese_name,
                        Experience_content = n1.Experience_content,
                        Experience_photo = n1.Experience_photo,
                        Issued = n1.Issued,
                        Home_issued = n1.Home_issued
                    };

            List<Experience_VM> experience_VMs = new List<Experience_VM>();
            bool first = true;
            foreach (var row in q)
            {
                if (first)
                {
                    Experience_VM experience_VM = new Experience_VM();
                    experience_VM.Experience_no = row.Experience_no;
                    experience_VM.Experience = row.Experience;
                    experience_VM.Volunteer = row.Volunteer;
                    experience_VM.Experience_content = row.Experience_content;
                    experience_VM.Experience_photo = row.Experience_photo;
                    experience_VM.Issued = (bool)row.Issued;
                    experience_VM.Home_issued = (bool)Home_issued;
                    experience_VMs.Add(experience_VM);
                    first = false;
                }                
            }
            IEnumerable<Experience_VM> experiences = experience_VMs;

            return experiences;
        }

        public void SelectExperiencebyExperience_no(int experience_no)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n1 in dbContext.Experiences
                    join n2 in dbContext.Volunteers
                    on n1.Volunteer_no equals n2.Volunteer_no
                    where n1.Experience_no == experience_no
                    select new
                    {
                        Experience_no = n1.Experience_no,
                        Experience = n1.Experience1,
                        Volunteer = n2.Chinese_name,
                        Experience_content = n1.Experience_content,
                        Experience_photo = n1.Experience_photo,
                        Issued = n1.Issued,
                        Home_issued = n1.Home_issued
                    };

            foreach (var row in q)
            {
                Experience_no = row.Experience_no;
                Experience = row.Experience;
                Volunteer = row.Volunteer;
                Experience_content = row.Experience_content;
                Experience_photo = row.Experience_photo;
                Home_issued = (bool)row.Home_issued;
                Issued = (bool)row.Issued;
            }
        }

        public void UpdateExperience(Experience_VM experience, string Filename)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n in dbContext.Experiences
                    where n.Experience_no == experience.Experience_no
                    select n;

            var q2 = (from n in dbContext.Volunteers
                     where n.Chinese_name == experience.Volunteer
                     select new
                     {
                         Volunteer_no = n.Volunteer_no
                     }).First();
            

            foreach (var row in q)
            {
                row.Experience1 = experience.Experience;
                row.Volunteer_no = q2.Volunteer_no;
                row.Experience_content = experience.Experience_content;
                if (!string.IsNullOrEmpty(Filename))
                {
                    row.Experience_photo = Filename;
                }                
                row.Issued = false;
                row.Home_issued = false;
            }

            dbContext.SaveChanges();
        }

        public void UpdateExperience_Home_issued(int id)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n in dbContext.Experiences
                    where n.Experience_no == id
                    select n;
            foreach (var row in q)
            {
                if ((bool)row.Home_issued)
                {
                    row.Home_issued = false;
                }
                else
                {
                    row.Home_issued = true;
                }
            }
            dbContext.SaveChanges();
        }

        public void InsertExperience(Experience_VM _experience, string Filename)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            int VolunteerNo = 0;
            foreach (var row in dbContext.Volunteers.Where(p => p.Chinese_name == _experience.Volunteer))
            {
                VolunteerNo = row.Volunteer_no;
            }

            Experience experience = new Experience();
            experience.Experience1 = _experience.Experience;
            experience.Volunteer_no = VolunteerNo;
            experience.Experience_content = _experience.Experience_content;
            experience.Experience_photo = Filename;
            experience.Issued = false;
            experience.Home_issued = false;

            dbContext.Experiences.Add(experience);
            dbContext.SaveChanges();
        }
    }
}