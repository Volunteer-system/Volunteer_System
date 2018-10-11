using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer_WPF.Model;

namespace Volunteer_WPF.View_Model
{
    public static class Account_ViewModel
    {  
        public static int User_ID;

        public static string User;
        
        public static string Permission;        

        public static bool Selectaccount(string Account_number, string Password)
        {
            Account_Model account_Model = new Account_Model();
            bool login = account_Model.SelectAccount(Account_number, Password);

            User_ID = account_Model.User_ID;
            User = account_Model.User;
            Permission = account_Model.Permission;

            return login;
        }
    }
}
