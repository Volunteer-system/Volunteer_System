using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer_WPF.Model
{
    class Human_assessment_result_Model
    {
        public int Assessment_result_ID { get; set; }
        public string Assessment_result { get; set; }

        public List<Human_assessment_result_Model> SelectHuman_assessment_result()
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n in dbContext.Human_assessment_result
                    select n;

            List <Human_assessment_result_Model> human_Assessment_Result_Models = new List<Human_assessment_result_Model>();
            foreach (var row in q)
            {
                Human_assessment_result_Model human_Assessment_Result_Model = new Human_assessment_result_Model();
                human_Assessment_Result_Model.Assessment_result_ID = row.Assessment_result_ID;
                human_Assessment_Result_Model.Assessment_result = row.Assessment_result;
                human_Assessment_Result_Models.Add(human_Assessment_Result_Model);
            }

            return human_Assessment_Result_Models;
        }
    }
}
