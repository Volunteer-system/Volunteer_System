using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Volunteer_Web.Models;

namespace Volunteer_Web.ViewModel
{
    public class Volunteer_Schedule_GroupModel
    {
        public int Group_no { get; set; }
        public string Group_name { get; set; }
        public List<Volunteer_Schedule_GroupModel> Getgroup()
        {
            List<Volunteer_Schedule_GroupModel> GM = new List<Volunteer_Schedule_GroupModel>();
            using (VolunteerEntities db = new VolunteerEntities())
            {
                var FG = from g in db.Service_group
                         select new { Group_no = g.Group_no, Group_name = g.Group_name };
                foreach (var G in FG)
                {
                    GM.Add(new Volunteer_Schedule_GroupModel()
                    {
                        Group_no = G.Group_no,
                        Group_name = G.Group_name
                    });
                }
            }
            return GM;
        }
    }
}