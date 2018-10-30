using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer_WPF.Model
{
    class Unit_expertise_Model
    {
        public string Expertise { get; set; }

        public List<string> SelectUnit_ExpertisebyApplication_unit_no(int Application_unit_no)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n2 in dbContext.Expertises
                    join n3 in dbContext.Expertise1
                    on n2.Expertise_no equals n3.Expertise_no
                    where n2.Application_unit_no == Application_unit_no
                    select new
                    {
                        Expertise = n3.Expertise
                    };

            List<string> Expertises = new List<string>();
            foreach (var row in q)
            {
                Expertises.Add(row.Expertise);
            }

            return Expertises;
        }

        public void InsertUnit_Expertise(int unit_no, List<string> Insert_list)
        {
            List<int> Expertise_no = new List<int>();
            VolunteerEntities dbContext = new VolunteerEntities();
            foreach (var row in Insert_list)
            {
                var q = from n in dbContext.Expertise1
                        where n.Expertise == row
                        select n;
                foreach (var row2 in q)
                {
                    Expertise_no.Add(row2.Expertise_no);
                }
            }

            dbContext.Expertises.ToList();
            foreach (var row in Expertise_no)
            {
                dbContext.Expertises.Local.Add(new Expertise
                {
                    Application_unit_no = unit_no,
                    Expertise_no = row
                });
            }

            dbContext.SaveChanges();
        }
        

        public void DeleteUnit_Expertise(int unit_no, List<string> Delete_list)
        {
            List<int> Expertise_no = new List<int>();
            VolunteerEntities dbContext = new VolunteerEntities();
            foreach (var row in Delete_list)
            {
                var q = from n in dbContext.Expertise1
                        where n.Expertise == row
                        select n;
                foreach (var row2 in q)
                {
                    Expertise_no.Add(row2.Expertise_no);
                }
            }

            foreach (var row in Expertise_no)
            {
                var Unit_expertise = (from n in dbContext.Expertises
                                     where n.Expertise_no == row && n.Application_unit_no == unit_no
                                     select n).FirstOrDefault();
                dbContext.Expertises.Remove(Unit_expertise);
            }

            dbContext.SaveChanges();
        }
    }
}
