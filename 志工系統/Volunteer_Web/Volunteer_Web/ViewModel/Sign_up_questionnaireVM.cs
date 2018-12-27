using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Volunteer_Web.Models;

namespace Volunteer_Web.ViewModel
{
    public class Sign_up_questionnaireVM
    {
       public Sign_up_questionnaireVM() {
            Q1 = new List<string> ();
            Q1else = "";
            Q2= new List<string>();
            Q2else = "";
            Q3= new List<string>();
            Q3doc = "";
            Q4= new List<string>();
            Q4else = "";
            Q5= new List<string>();
            Q5unit = "";
            Q5years = "";
            Q5content = "";
            Q6jobs = "";
            Q7= "";
            Q8=new List<string> ();
        }

        public IEnumerable<string> Q1 { get; set; }
        public string Q1else { get; set; }
        public IEnumerable<string> Q2 { get; set; }
        public string Q2else { get; set; }
        public IEnumerable<string> Q3 { get; set; }
        public string Q3doc { get; set; }
        public IEnumerable<string> Q4 { get; set; }
        public string Q4else { get; set; }
        public IEnumerable<string> Q5 { get; set; }
        public string Q5unit{ get; set; }
        public string Q5years { get; set; }
        public string Q5content { get; set; }
        //public IEnumerable<int> Q6 { get; set; }
        public string Q6jobs { get; set; }
        public string Q7 { get; set; }
        public IEnumerable<string> Q8 { get; set; }

        








    }

}