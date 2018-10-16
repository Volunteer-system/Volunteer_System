using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer_WPF.Model
{
    class Sign_up_interview_period_Model
    {
        public string interview_period { get; set; }

        public List<string> SelectSign_up_interview_periodbySignup_no(int Signup_no)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            List<string> List_interview_period = new List<string>();

            var q = from n1 in dbContext.Sign_up_interview_period
                    join n2 in dbContext.Service_period1
                    on n1.interview_period_no equals n2.Service_period_no
                    where n1.Sign_up_no == Signup_no
                    select new
                    {
                        interview_period = n2.Service_period.ToString()
                    };

            foreach (var row in q)
            {
                List_interview_period.Add(row.interview_period);
            }

            return List_interview_period;
        }
    }
}
