using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer_WPF.Model
{
    class Volunteer_supervision_Model
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string email { get; set; }

        public List<Volunteer_supervision_Model> Selectsupervision()
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n in dbContext.Volunteer_supervision
                    select n;
            
            List <Volunteer_supervision_Model> supervisions = new List<Volunteer_supervision_Model>();
            foreach (var row in q)
            {
                Volunteer_supervision_Model volunteer_Supervision_Model = new Volunteer_supervision_Model();
                volunteer_Supervision_Model.ID = row.supervision_ID;
                volunteer_Supervision_Model.Phone = row.supervision_phone.ToString();
                volunteer_Supervision_Model.Name = row.supervision_Name;
                volunteer_Supervision_Model.Address = row.supervision_address;
                volunteer_Supervision_Model.email = row.supervision_address;
                supervisions.Add(volunteer_Supervision_Model);
            }

            return supervisions;
        }
    }
}
