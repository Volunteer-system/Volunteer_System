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

        public void InsertApply_result(int apply_ID, List<string> insert_Result)
        {
            List<int> Result_no = new List<int>();
            VolunteerEntities dbContext = new VolunteerEntities();
            foreach (var row in insert_Result)
            {
                var q = from n in dbContext.Human_assessment_result
                        where n.Assessment_result == row
                        select n;
                foreach (var row2 in q)
                {
                    Result_no.Add(row2.Assessment_result_ID);
                }
            }

            dbContext.Apply_result.ToList();
            foreach (var row in Result_no)
            {
                dbContext.Apply_result.Local.Add(new Apply_result
                {
                    Apply_ID = apply_ID,
                    result_ID = row
                });
            }

            dbContext.SaveChanges();
        }

        public void DeleteApply_result(int apply_ID, List<string> delete_Result)
        {
            List<int> Result_no = new List<int>();
            VolunteerEntities dbContext = new VolunteerEntities();
            foreach (var row in delete_Result)
            {
                var q = from n in dbContext.Human_assessment_result
                        where n.Assessment_result == row
                        select n;
                foreach (var row2 in q)
                {
                    Result_no.Add(row2.Assessment_result_ID);
                }
            }
            foreach (var row in Result_no)
            {
                var results = (from n in dbContext.Apply_result
                         where n.Apply_ID == apply_ID &&
                               n.result_ID == row
                         select n).FirstOrDefault();
                dbContext.Apply_result.Remove(results);
            }

            dbContext.SaveChanges();
        }

    }
}
