using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Volunteer_Web.ViewModel
{
    public class Service_period_VM
    {
        public Service_period_VM()
        {
            wish1 = new int[] { };
            wish2 = new int[] { };
            wish3 = new int[] { };
        }

        public int[] wish1 { get; set; }
        public int[] wish2 { get; set; }
        public int[] wish3 { get; set; }


    }
}
