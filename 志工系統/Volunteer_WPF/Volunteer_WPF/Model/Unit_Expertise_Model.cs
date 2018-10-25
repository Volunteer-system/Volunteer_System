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
    }
}
