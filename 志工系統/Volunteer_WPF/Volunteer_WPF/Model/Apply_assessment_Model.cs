using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer_WPF.Model
{
    class Apply_assessment_Model
    {
        public int Apply_ID { get; set; }
        public string Assessment { get; set; }

        public List<string> SelectApply_assessment_byApply_ID(int apply_ID)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n1 in dbContext.Apply_Assessment
                    join n2 in dbContext.Human_assessment
                    on n1.Assessment_ID equals n2.Assessment_ID
                    where n1.Apply_ID == apply_ID
                    select new
                    {
                        Assessment = n2.Assessment_name
                    };

            List<string> Assessments = new List<string>();
            foreach (var row in q)
            {
                Assessments.Add(row.Assessment);
            }

            return Assessments;
        }
    }
}
