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

        public void InsertApply_assessment(int apply_ID, List<string> insert_Assessment)
        {
            List<int> Assessment_no = new List<int>();
            VolunteerEntities dbContext = new VolunteerEntities();
            foreach (var row in insert_Assessment)
            {
                var q = from n in dbContext.Human_assessment
                        where n.Assessment_name == row
                        select n;
                foreach (var row2 in q)
                {
                    Assessment_no.Add(row2.Assessment_ID);
                }
            }

            dbContext.Apply_Assessment.ToList();
            foreach (var row in Assessment_no)
            {
                dbContext.Apply_Assessment.Local.Add(new Apply_Assessment
                {
                    Apply_ID = apply_ID,
                    Assessment_ID = row
                });
            }
            dbContext.SaveChanges();
        }

        public void DeleteApply_assessment(int apply_ID, List<string> delete_Assessment)
        {
            List<int> Assessment_no = new List<int>();
            VolunteerEntities dbContext = new VolunteerEntities();
            foreach (var row in delete_Assessment)
            {
                var q1 = from n in dbContext.Human_assessment
                        where n.Assessment_name == row
                        select n;
                foreach (var row2 in q1)
                {
                    Assessment_no.Add(row2.Assessment_ID);
                }
            }
            foreach (var row in Assessment_no)
            {
                var Assessments = (from n in dbContext.Apply_Assessment
                                   where n.Apply_ID == apply_ID &&
                                         n.Assessment_ID == row
                                   select n).FirstOrDefault();
                dbContext.Apply_Assessment.Remove(Assessments);
            }

            dbContext.SaveChanges();
        }
    }
}
