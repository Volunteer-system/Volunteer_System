using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Volunteer_Web.Models
{
    public class accountMethod
    {
        VolunteerEntities db = new VolunteerEntities();
        public string GetUserName(string Account_number, string Password)
        {
            string UserName = "";
            //判斷帳密有無符合
            var userID = (from a in db.accounts
                         where a.Account_number == Account_number && a.Password == Password
                         select a.User_ID).FirstOrDefault();
            if (userID != null)
            {   //判斷運用單位有無符合
                var ApplicationName = db.Application_unit.Where(d => d.Application_unit_no == userID).Select(d => d.Application_unit1).FirstOrDefault();
                //判斷志工督導有無符合
                var SupersionName = db.Volunteer_supervision.Where(s => s.supervision_ID == userID).Select(s => s.supervision_Name).FirstOrDefault();
                //判斷志工有無符合
                var VolunteersName = db.Volunteers.Where(v => v.Volunteer_no == userID).Select(v => v.Chinese_name + " " + v.English_name).FirstOrDefault();

                UserName = ApplicationName != null ? userID+" 運用單位 " + ApplicationName : SupersionName != null ? userID + " 志工督導 " + SupersionName : VolunteersName != null ? userID + " 志工 " + VolunteersName : "";
            }
            //if (UserName == "")
            //{
            //    UserName = "測試員";
            //}
            return UserName;
        }
    }
}