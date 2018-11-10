using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer_WPF.Model
{
    class Human_assessment_Model
    {
        public int Assessment_ID { get; set; }
        public string Assessment_name { get; set; }

        public List<Human_assessment_Model> SelectHuman_assessment()
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n in dbContext.Human_assessment
                    select n;

            List < Human_assessment_Model > human_Assessment_Models = new List<Human_assessment_Model>();
            foreach (var row in q)
            {
                Human_assessment_Model human_Assessment_Model = new Human_assessment_Model();
                human_Assessment_Model.Assessment_ID = row.Assessment_ID;
                human_Assessment_Model.Assessment_name = row.Assessment_name;
                human_Assessment_Models.Add(human_Assessment_Model);
            }

            return human_Assessment_Models;
        }

    }
}
