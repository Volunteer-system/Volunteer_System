﻿using System;
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
                        Experience_photo = n1.Experience_photo
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
                        Experience_photo = n1.Experience_photo
                    };

            foreach (var row in q)
            {
                Experience_no = row.Experience_no;
                Experience = row.Experience;
                Volunteer = row.Volunteer;
                Experience_content = row.Experience_content;
                Experience_photo = row.Experience_photo;
            }
        }

        public void UpdateExperience(Experience_VM experience)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n in dbContext.Experiences
                    where n.Experience_no == experience.Experience_no
                    select n;

            var q2 = from n in dbContext.Volunteers
                     where n.Chinese_name == experience.Volunteer
                     select new
                     {
                         Volunteer_no = n.Volunteer_no
                     };

            foreach (var row in q)
            {
                row.Experience1 = experience.Experience;
                row.Volunteer_no = Convert.ToInt32(q2.First());
                row.Experience_content = experience.Experience_content;
                row.Experience_photo = experience.Experience_photo;
                row.Issued = experience.Issued;
            }

            dbContext.SaveChanges();
        }
    }
}