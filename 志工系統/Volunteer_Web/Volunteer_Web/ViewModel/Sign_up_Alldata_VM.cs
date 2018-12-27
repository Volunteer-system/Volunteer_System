using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Volunteer_Web.ViewModel
{
    public class Sign_up_Alldata_VM
    {
        public string Chinese_name { get; set; }
        public string English_name { get; set; }
        public string Sex { get; set; }
        public DateTime Birthday { get; set; }
        public string Sign_up_type { get; set; }
        public DateTime Sign_up_date { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Education { get; set; }
        public string Job { get; set; }
        public IEnumerable<string> Expertises { get; set; }
        public IEnumerable<string> Q1 { get; set; }
        public string Q1else { get; set; }
        public IEnumerable<string> Q2 { get; set; }
        public string Q2else { get; set; }
        public IEnumerable<string> Q3 { get; set; }
        public string Q3doc { get; set; }
        public IEnumerable<string> Q4 { get; set; }
        public string Q4else { get; set; }
      //  public IEnumerable<string> Q5 { get; set; }
        public string Q5unit { get; set; }
        public string Q5years { get; set; }
        public string Q5content { get; set; }
       // public IEnumerable<int> Q6 { get; set; }
        public string Q6jobs { get; set; }
        public string Q7 { get; set; }
        public IEnumerable<string> Q8 { get; set; }

        public void SetSign_up_Alldata(Sign_up_session sign_Up_Session, Sign_up_questionnaireVM sign_Up_QuestionnaireVM)
        {
            Chinese_name = sign_Up_Session.Chinese_name;
            English_name = sign_Up_Session.English_name;
            Sex = sign_Up_Session.Sex;
            Birthday = sign_Up_Session.Birthday;
            Sign_up_type = sign_Up_Session.Sign_up_type;
            Sign_up_date = sign_Up_Session.Sign_up_date;
            Phone = sign_Up_Session.Phone;
            Mobile = sign_Up_Session.Mobile;
            Email = sign_Up_Session.Email;
            Address = sign_Up_Session.Address;
            Education = sign_Up_Session.Education;
            Job = sign_Up_Session.Job;
            Expertises = sign_Up_Session.Expertises;

            Q1 = sign_Up_QuestionnaireVM.Q1;
            Q1else = sign_Up_QuestionnaireVM.Q1else;
            Q2 = sign_Up_QuestionnaireVM.Q2;
            Q2else = sign_Up_QuestionnaireVM.Q2else;
            Q3 = sign_Up_QuestionnaireVM.Q3;
            Q3doc = sign_Up_QuestionnaireVM.Q3doc;
            Q4 = sign_Up_QuestionnaireVM.Q4;
            Q4else = sign_Up_QuestionnaireVM.Q4else;
          //  Q5 = sign_Up_QuestionnaireVM.Q5;
            Q5unit = sign_Up_QuestionnaireVM.Q5unit;
            Q5years = sign_Up_QuestionnaireVM.Q5years;
            Q5content = sign_Up_QuestionnaireVM.Q5content;
           // Q6 = sign_Up_QuestionnaireVM.Q6;
            Q6jobs = sign_Up_QuestionnaireVM.Q6jobs;
            Q7 = sign_Up_QuestionnaireVM.Q7;
            Q8 = sign_Up_QuestionnaireVM.Q8;
        }
    }
}