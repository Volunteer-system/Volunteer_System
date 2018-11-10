using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer_WPF.Model
{
    class Apply_result_Model
    {
        public int Apply_ID { get; set; }
        public int result { get; set; }

        public List<string> SelectApply_result_byApply_ID(int apply_ID)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n1 in dbContext.Apply_result
                    join n2 in dbContext.Human_assessment_result
                    on n1.result_ID equals n2.Assessment_result_ID
                    where n1.Apply_ID == apply_ID
                    select new
                    {
                        Assessment_result = n2.Assessment_result
                    };

            List<string> Assessment_results = new List<string>();
            foreach (var row in q)
            {
                Assessment_results.Add(row.Assessment_result);
            }

            return Assessment_results;
        }

    }
}
