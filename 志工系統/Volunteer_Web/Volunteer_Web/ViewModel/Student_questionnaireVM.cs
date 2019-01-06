using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Volunteer_Web.ViewModel
{
    public class Student_questionnaireVM
    {

        public Student_questionnaireVM()
        {
            training = true;
            knowing = true;
            Emergency_Name = "";
            Emergency_Phone = "";
            School_Name = "";
            School_Regulation = "";
        }
        public bool training { get; set; }
        public bool knowing { get; set; }
        public string Emergency_Name { get; set; }
        public string Emergency_Phone { get; set; }
        public string School_Name { get; set; }
        public string School_Regulation { get; set; }
    }
}