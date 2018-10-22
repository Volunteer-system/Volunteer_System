using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer_WPF.Model
{
    class Event_category_Model
    {
        public string event_category { get; set; }

        public List<string> Selectevent_category()
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n in dbContext.event_category
                    select new { event_category = n.event_category1 };

            List<string> event_categorys = new List<string>();
            foreach (var row in q)
            {
                event_categorys.Add(row.event_category);
            }

            return event_categorys;
        }
    }
}
