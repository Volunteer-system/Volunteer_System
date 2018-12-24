using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Volunteer_Web.Models.VolunteerUse
{
    public class ScheduleModel
    {
        public int Volunteer_no { get; set; }

        public int Wish_order { get; set; }

        public int Service_period_no { get; set; }
        public void Insert_Volunteer_Service_period(int Volunteer_no, int[] Wish_order, List<int[]> Service_period_no)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var user_detail = dbContext.Service_period2.Where(v => v.Volunteer_no == Volunteer_no).ToList();
            foreach (var del in user_detail)
            {
                dbContext.Service_period2.Remove(del);
            }
            for (int i = 0; i < Wish_order.Length; i += 1)
            {
                for (int j = 0; j < Service_period_no[i].Length; j += 1)
                {
                    Service_period2 sp = new Service_period2();
                    sp.Volunteer_no = Volunteer_no;
                    //sp.Wish_order = Wish_order[i];
                    sp.Service_period_no = Service_period_no[i][j];
                    dbContext.Service_period2.Add(sp);
                }
            }
            dbContext.SaveChanges();
        }
    }
}