using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer_WPF.Model
{
    class Service_group_Model
    {
        public string Group_name { get; set; }

        public List<string> SelectService_group()
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n in dbContext.Service_group
                    select new { Group_name = n.Group_name };
            List<string> Group_names = new List<string>();
            foreach (var row in q)
            {
                Group_names.Add(row.Group_name);
            }

            return Group_names;
        }
    }
}
