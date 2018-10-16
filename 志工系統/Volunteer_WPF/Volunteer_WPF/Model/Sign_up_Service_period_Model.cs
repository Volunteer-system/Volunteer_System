using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer_WPF.Model
{
    class Sign_up_Service_period_Model
    {
        //志願順序
        public string Wish_order { get; set; }
        //服務時段
        public string Service_period { get; set; }

        public List<string> SelectSign_up_Service_periodbySignup_no(int Signup_no)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            List<string> List_Service_period = new List<string>();

            var q = from n1 in dbContext.Sign_up_Service_period
                    join n2 in dbContext.Service_period1
                    on n1.Service_period_no equals n2.Service_period_no
                    where n1.Sign_up_no == Signup_no
                    select new
                    {
                        Wish_order = n1.Wish_order.ToString(),
                        Service_period = n2.Service_period.ToString()
                    };
                        
            foreach (var row in q)
            {
                List_Service_period.Add(row.Wish_order + " " +row.Service_period);                
            }

            return List_Service_period;
        }
    }
}
