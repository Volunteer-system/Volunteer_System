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

        //首頁維護
        public void SelectHome_maintenance()
        {
            SelectIndexphotoe();

            SelectIndexvideolinks();

            Experience_VM experience_VM = new Experience_VM();
            Experiences = experience_VM.SelectExperience();           
        }
        //首頁讀取
        public void SelectHome_maintenance_inHome()
        {
            SelectIndexphotoe();

            SelectIndexvideolinks();

            Experience_VM experience_VM = new Experience_VM();
            Experiences = experience_VM.SelectExperiencebyHome_Issued();
        }

        public void SelectIndexphotoe()
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
        }

        public void SelectIndexvideolinks()
        {
            VolunteerEntities dbContext = new VolunteerEntities();
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
        }

        public void Insert_indexphoto(string Filename)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            Indexphoto indexphoto = new Indexphoto();
            indexphoto.Indexphoto1 = Filename;
            indexphoto.Issued = false;
            dbContext.Indexphotoes.Add(indexphoto);
            dbContext.SaveChanges();
        }

        public void Updata_indexphoto(int id)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n in dbContext.Indexphotoes
                    where n.Indexphoto_no == id
                    select n;
            foreach (var row in q)
            {
                if ((bool)row.Issued)
                {
                    row.Issued = false;
                }
                else
                {
                    row.Issued = true;
                }
                dbContext.SaveChanges();
            }
        }

        public void Insert_indexvideolink(string Filename)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            Indexvideolink indexvideolink = new Indexvideolink();
            indexvideolink.Videolink = Filename;
            indexvideolink.Issued = false;
            dbContext.Indexvideolinks.Add(indexvideolink);
            dbContext.SaveChanges();
        }

        public void Updata_indexvideolink(int id)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n in dbContext.Indexvideolinks
                    where n.Indexvideolink_no == id
                    select n;
            foreach (var row in q)
            {
                if ((bool)row.Issued)
                {
                    row.Issued = false;
                }
                else
                {
                    row.Issued = true;
                }
                dbContext.SaveChanges();
            }
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