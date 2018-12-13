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
            string permission = "";
            string ApplicationName = "";
            string SupersionName = "";
            string VolunteersName = "";
            //判斷帳密有無符合
            var userID = (from a in db.accounts
                         where a.Account_number == Account_number && a.Password == Password
                         select new { User_ID = a.User_ID, Permission = a.Permission }).FirstOrDefault();
            if (userID.User_ID != null)
            {
                switch (userID.Permission)
                {
                    case "Volunteer_supervision":
                        permission = "志工督導";
                        SupersionName = db.Volunteer_supervision.Where(s => s.supervision_ID == userID.User_ID).Select(s => s.supervision_Name).FirstOrDefault();
                        break;
                    case "Volunteer":
                        permission = "志工";
                        VolunteersName = db.Volunteers.Where(v => v.Volunteer_no == userID.User_ID).Select(v => v.Chinese_name + " " + v.English_name).FirstOrDefault();
                        break;
                    case "Application_unit":
                        permission = "運用單位";
                        ApplicationName = db.Application_unit.Where(d => d.Application_unit_no == userID.User_ID).Select(d => d.Application_unit1).FirstOrDefault();
                        break;
                    case "director":
                        permission = "社工";
                        break;
                }              
                UserName = ApplicationName != null ? userID.User_ID +" "+ permission + " " + ApplicationName : SupersionName != null ? userID.User_ID + " " + permission + " " + SupersionName : VolunteersName != null ? userID.User_ID + " " + permission + " " + VolunteersName : "";
            }

            return UserName;
        }
    }
}