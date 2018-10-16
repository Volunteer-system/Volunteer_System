using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer_WPF.Model
{
    class ExpertiseBasic_Model
    {
        public int Expertise_no { get; set; }
        public string Expertise_name { get; set; }

        public List<ExpertiseBasic_Model> GetExpertises()
        {
            global::Volunteer_WPF.Model.VolunteerEntities entities = new Model.VolunteerEntities();
            var q = from p in entities.Expertise1
                    select p;

            List<ExpertiseBasic_Model> expertiseBasic_Models = new List<ExpertiseBasic_Model>();
            foreach (var p in q )
            {
                ExpertiseBasic_Model _Model = new ExpertiseBasic_Model();
                _Model.Expertise_no=p.Expertise_no;
                _Model.Expertise_name = p.Expertise;
                expertiseBasic_Models.Add(_Model);

            }
            return expertiseBasic_Models;
        }
    }
}
