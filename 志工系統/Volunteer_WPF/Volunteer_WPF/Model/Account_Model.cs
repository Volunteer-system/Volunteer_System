using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer_WPF.Model
{
    class Account_Model
    {
        public int    User_ID { get; set; }
        public string User { get; set; }
        public string Permission { get; set; }

        public bool SelectAccount(string Account_number, string Password)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
                
            //確認登入者是否為志工督導
            var q = from n1 in dbContext.accounts
                    join n2 in dbContext.Volunteer_supervision
                    on n1.User_ID equals n2.supervision_ID
                    where n1.Account_number == Account_number &&
                          n1.Password == Password
                    select new
                    {
                        User_ID = n1.User_ID,
                        User = n2.supervision_Name,
                        Permission = n1.Permission
                    };

            foreach (var row in q)
            {
                User_ID = (int)row.User_ID;
                User = row.User;
                Permission = row.Permission;
                return true;
            }
            
            //確認登入者是否為志工
            //確認登入者是否為運用單位

            return false;
        }

        public string Selectdirector()
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = (from n1 in dbContext.accounts
                     join n2 in dbContext.Volunteer_supervision
                     on n1.User_ID equals n2.supervision_ID
                     where n1.Permission == "director"
                     select new { supervision_Name = n2.supervision_Name }).First();

            return q.supervision_Name;
        }
    }
}
