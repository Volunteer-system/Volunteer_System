﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer_WPF.Model
{
    class Activity_Model
    {
        //活動編號
        public string Activity_no { get; set; }
        //活動名稱
        public string Activity_name { get; set; }
        //活動類別
        public string Activity_type { get; set; }
        //組別
        public string Group { get; set; }
        //活動起始時間
        public string Activity_startdate { get; set; }
        //活動結束時間
        public string Activity_enddate { get; set; }
        //承辦單位
        public string Undertake_unit { get; set; }
        //承辦人
        public string Undertaker { get; set; }
        //承辦單位電話
        public string Undertake_phone { get; set; }
        //承辦單位e-mail
        public string Undertake_email { get; set; }
        //課程人數
        public string Member { get; set; }
        //備取人數
        public string Spare { get; set; }
        //活動地點
        public string Place { get; set; }
        //活動簡介
        public string Summary { get; set; }

        public List<Activity_Model> SelectActivity_byActivity_no(DateTime Startdate, DateTime Enddate, string Type, string Group)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n1 in dbContext.Activities
                    join n2 in dbContext.Volunteer_supervision
                    on n1.Undertaker equals n2.supervision_ID
                    join n3 in dbContext.Activity_type
                    on n1.Activity_type_ID equals n3.Activity_type_ID
                    join n4 in dbContext.Service_group
                    on n1.Group_no equals n4.Group_no
                    where
                    ((Startdate == DateTime.MinValue) ? true : n1.Activity_startdate >= Startdate) &&
                    ((Enddate == DateTime.MinValue) ? true : n1.Activity_enddate <= Enddate) &&
                    ((Type == "") ? true : n3.Activity_type1 == Type) &&
                    ((Group == "") ? true : n4.Group_name == Group)
                    select new
                    {
                        Activity_no = n1.Activity_no,
                        Activity_name = n1.Activity_name,
                        Activity_type = n3.Activity_type1,
                        Group = n4.Group_name,
                        Activity_startdate = n1.Activity_startdate,
                        Activity_enddate = n1.Activity_enddate,
                        Undertake_unit = n1.Undertake_unit,
                        Undertaker = n2.supervision_Name,
                        Undertake_phone = n1.Undertake_phone,
                        Undertake_email = n1.Undertake_email,
                        Member = n1.Member,
                        Spare = n1.Spare,
                        Place = n1.Place,
                        Summary = n1.Summary
                    };

            List < Activity_Model > activity_models = new List<Activity_Model>();
            foreach (var row in q)
            {
                Activity_Model activity_Model = new Activity_Model();
                activity_Model.Activity_no = row.Activity_no.ToString();
                activity_Model.Activity_name = row.Activity_name.ToString();
                activity_Model.Activity_type = row.Activity_type.ToString();
                activity_Model.Group = row.Group.ToString();
                activity_Model.Activity_startdate = row.Activity_startdate.ToString();
                activity_Model.Activity_enddate = row.Activity_enddate.ToString();
                activity_Model.Undertake_unit = row.Undertake_unit.ToString();
                activity_Model.Undertaker = row.Undertaker.ToString();
                activity_Model.Undertake_phone = row.Undertake_phone.ToString();
                activity_Model.Undertake_email = row.Undertake_email.ToString();
                activity_Model.Member = row.Member.ToString();
                activity_Model.Spare = row.Spare.ToString();
                activity_Model.Place = row.Place.ToString();
                activity_Model.Summary = row.Summary.ToString();

                activity_models.Add(activity_Model);
            }
        
            return activity_models;
        }

        public List<Activity_Model> SelectVolunteer_ActivitybyVolunteerno(int Volunteer_no)
        {
            VolunteerEntities dbContext = new VolunteerEntities();
            var q = from n1 in dbContext.Volunteer
                    join n2 in dbContext.Activity1
                    on n1.Volunteer_no equals n2.Volunteer_no
                    join n3 in dbContext.Activities
                    on n2.Activity_no equals n3.Activity_no
                    join n4 in dbContext.Volunteer_supervision
                    on n3.Undertaker equals n4.supervision_ID
                    where n1.Volunteer_no == Volunteer_no
                    select new
                    {
                        Activity_no = n3.Activity_no,
                        Activity_name = n3.Activity_name,
                        Undertaker = n4.supervision_Name
                    };

            List<Activity_Model> activity_list = new List<Activity_Model>();
            foreach (var row in q)
            {
                Activity_Model activity_Model = new Activity_Model();
                activity_Model.Activity_no = row.Activity_no.ToString();
                activity_Model.Activity_name = row.Activity_name.ToString();
                activity_Model.Undertaker = row.Undertaker.ToString();

                activity_list.Add(activity_Model);
            }

            return activity_list;
        }


    }
}
