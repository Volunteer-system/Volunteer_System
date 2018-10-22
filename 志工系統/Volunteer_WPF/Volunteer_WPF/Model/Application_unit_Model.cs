using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer_WPF.Model
{
    class Application_unit_Model
    {
        public string Application_unit { get; set; }

        public List<string> SelectApplication_unit_name()
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n in dbContext.Application_unit
                    select new { application_unit = n.Application_unit1};

            List<string> application_units = new List<string>();
            foreach (var row in q)
            {
                application_units.Add(row.application_unit);
            }

            return application_units;
        }
    }
}
