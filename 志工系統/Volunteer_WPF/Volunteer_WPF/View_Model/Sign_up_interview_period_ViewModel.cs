using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer_WPF.Model;

namespace Volunteer_WPF.View_Model
{
    class Sign_up_interview_period_ViewModel
    {
        public string interview_period { get; set; }

        public List<string> SelectSign_up_interview_periodbySignup_no(int Signup_no)
        {
            List<string> List_interview_period = new List<string>();
            Sign_up_interview_period_Model interview_Period_Model = new Sign_up_interview_period_Model();
            List_interview_period = interview_Period_Model.SelectSign_up_interview_periodbySignup_no(Signup_no);

            return List_interview_period;
        }
    }
}
