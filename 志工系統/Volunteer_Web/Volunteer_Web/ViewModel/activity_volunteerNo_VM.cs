using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Volunteer_Web.Models;

namespace Volunteer_Web.ViewModel
{
    public class activity_volunteerNo_VM
    {
        public bool part { get; set; }
        public int count { get; set; }
        public int Activity_no { get; set; }
        public string Activity_name { get; set; }
        public int Activity_type_ID { get; set; }
        public Nullable<int> Group_no { get; set; }
        public Nullable<System.DateTime> Activity_startdate { get; set; }
        public Nullable<System.DateTime> Activity_enddate { get; set; }
        public string Undertake_unit { get; set; }
        public Nullable<int> Undertaker { get; set; }
        public string Undertake_phone { get; set; }
        public string Undertake_email { get; set; }
        public string lecturer { get; set; }
        public Nullable<int> Member { get; set; }
        public Nullable<int> Spare { get; set; }
        public string Place { get; set; }
        public string Summary { get; set; }
        public Nullable<int> Activity_Photo_id { get; set; }
        public Nullable<bool> Meal { get; set; }

        //Activity_type1 是 Basic.Activity_type資料表 裡面的 Activity_type
        //Group_name 是 Basic.Serviece_group資料表 裡面的 Group_name
        //supervision_Name 是 supervision.Volunteer_supervision資料表 supervision_Name
        public string Activity_type1 { get; set; }

        public string Group_name { get; set; }

        public string supervision_Name { get; set; }

        // public int activityNumberOfPeople(int act_no)
        //{

        //    VolunteerEntities db = new VolunteerEntities();
        //    var q = db.Activity1.Where(m=>m.Activity_no== act_no).Select(n => n.Volunteer_no).Distinct().Count();
        //    return q;


        //}

        //依Activity_type_ID (活動類別ID) 分別 列出活動
        public List<activity_volunteerNo_VM> activity_By_Activity_type_ID(int id, int userid)
        {
            List<activity_volunteerNo_VM> acv = new List<activity_volunteerNo_VM>();

            VolunteerEntities db = new VolunteerEntities();

            var q = from a in db.Activities
                    join at in db.Activity_type
                    on a.Activity_type_ID equals at.Activity_type_ID
                    join gr in db.Service_group
                    on a.Group_no equals gr.Group_no
                    join vs in db.Volunteer_supervision
                    on a.Undertaker equals vs.supervision_ID

                    where a.Activity_type_ID == id
                    select new
                    {
                        //count 是 某個活動類別(以id判斷) 的 每個活動 的 活動報名人數
                        count = db.Activity1.Where(m => m.Activity_no == a.Activity_no).Select(n => n.Volunteer_no).Distinct().Count(),
                        //db.Activity1 是 Volunteer.Activity 資料表

                        part = db.Activity1.Where(m => m.Activity_no == a.Activity_no).Any(p => p.Volunteer_no == userid),
                        Activity_no = a.Activity_no,
                        Activity_name = a.Activity_name,
                        Activity_type_ID = a.Activity_type_ID,

                        Activity_type1 = at.Activity_type1,
                        Group_name = gr.Group_name,
                        supervision_Name = vs.supervision_Name,

                        Group_no = a.Group_no,
                        Activity_startdate = a.Activity_startdate,
                        Activity_enddate = a.Activity_enddate,
                        Undertake_unit = a.Undertake_unit,
                        Undertaker = a.Undertaker,
                        Undertake_phone = a.Undertake_phone,
                        Undertake_email = a.Undertake_email,
                        lecturer = a.lecturer,
                        Member = a.Member,
                        Spare = a.Spare,
                        Place = a.Place,
                        Summary = a.Summary,
                        Activity_Photo_id = a.Activity_Photo_id,
                        Meal = a.Meal
                    };

            foreach (var item in q)
            {
                acv.Add(new activity_volunteerNo_VM()        /*acv.Add( new 1個 activity_volunteerNo_VM(){ 裡面 放東西} )*/
                {
                    part = item.part,
                    count = item.count,
                    Activity_no = item.Activity_no,
                    Activity_name = item.Activity_name,
                    Activity_type_ID = item.Activity_type_ID,
                    Group_no = item.Group_no,
                    Activity_startdate = item.Activity_startdate,
                    Activity_enddate = item.Activity_enddate,
                    Undertake_unit = item.Undertake_unit,
                    Undertaker = item.Undertaker,
                    Undertake_phone = item.Undertake_phone,
                    Undertake_email = item.Undertake_email,
                    lecturer = item.lecturer,
                    Member = item.Member,
                    Spare = item.Spare,
                    Place = item.Place,
                    Summary = item.Summary,
                    Activity_Photo_id = item.Activity_Photo_id,
                    Meal = item.Meal,

                    Activity_type1 = item.Activity_type1,
                    Group_name = item.Group_name,
                    supervision_Name = item.Group_name


                });
            }

            return acv;
        }

        //依 Basic.Activity資料表 的 Activity_type_ID (活動類別ID) 和 Activity_startdate (活動開始日期) 和 Activity_enddate (活動結束日期)
        public List<activity_volunteerNo_VM> activity_By_Activity_type_ID(int id, DateTime startDate, DateTime endDate,int userid)
        {
            List<activity_volunteerNo_VM> acv = new List<activity_volunteerNo_VM>();

            VolunteerEntities db = new VolunteerEntities();
            var q = from a in db.Activities
                    join at in db.Activity_type
                    on a.Activity_type_ID equals at.Activity_type_ID
                    join gr in db.Service_group
                    on a.Group_no equals gr.Group_no
                    join vs in db.Volunteer_supervision
                    on a.Undertaker equals vs.supervision_ID

                    where a.Activity_type_ID == id && a.Activity_startdate>=startDate && a.Activity_enddate<=endDate
                    select new
                    {
                        //count 是 某個活動類別(以id判斷) 的 每個活動 的 活動報名人數
                        count = db.Activity1.Where(m => m.Activity_no == a.Activity_no).Select(n => n.Volunteer_no).Distinct().Count(),
                        //db.Activity1 是 Volunteer.Activity 資料表

                        part = db.Activity1.Where(m => m.Activity_no == a.Activity_no).Any(p => p.Volunteer_no == userid),
                        Activity_no = a.Activity_no,
                        Activity_name = a.Activity_name,
                        Activity_type_ID = a.Activity_type_ID,

                        Activity_type1 = at.Activity_type1,
                        Group_name = gr.Group_name,
                        supervision_Name = vs.supervision_Name,

                        Group_no = a.Group_no,
                        Activity_startdate = a.Activity_startdate,
                        Activity_enddate = a.Activity_enddate,
                        Undertake_unit = a.Undertake_unit,
                        Undertaker = a.Undertaker,
                        Undertake_phone = a.Undertake_phone,
                        Undertake_email = a.Undertake_email,
                        lecturer = a.lecturer,
                        Member = a.Member,
                        Spare = a.Spare,
                        Place = a.Place,
                        Summary = a.Summary,
                        Activity_Photo_id = a.Activity_Photo_id,
                        Meal = a.Meal
                    };

            foreach (var item in q)
            {
                acv.Add(new activity_volunteerNo_VM()        /*acv.Add( new 1個 activity_volunteerNo_VM(){ 裡面 放東西} )*/
                {
                    part = item.part,
                    count = item.count,
                    Activity_no = item.Activity_no,
                    Activity_name = item.Activity_name,
                    Activity_type_ID = item.Activity_type_ID,
                    Group_no = item.Group_no,
                    Activity_startdate = item.Activity_startdate,
                    Activity_enddate = item.Activity_enddate,
                    Undertake_unit = item.Undertake_unit,
                    Undertaker = item.Undertaker,
                    Undertake_phone = item.Undertake_phone,
                    Undertake_email = item.Undertake_email,
                    lecturer = item.lecturer,
                    Member = item.Member,
                    Spare = item.Spare,
                    Place = item.Place,
                    Summary = item.Summary,
                    Activity_Photo_id = item.Activity_Photo_id,
                    Meal = item.Meal,

                    Activity_type1 = item.Activity_type1,
                    Group_name = item.Group_name,
                    supervision_Name = item.Group_name


                });
            }
            return acv;

        }

        //依 登入的志工號碼(Volunteer_no) 已參加的活動 分別 列出活動
        public List<activity_volunteerNo_VM> activityByAlreadyParticipated(int userid)
        {
            List<activity_volunteerNo_VM> acv = new List<activity_volunteerNo_VM>();
            VolunteerEntities db = new VolunteerEntities();

            var q = from a in db.Activities.Distinct()
                    join at in db.Activity_type
                    on a.Activity_type_ID equals at.Activity_type_ID
                    join gr in db.Service_group
                    on a.Group_no equals gr.Group_no
                    join vs in db.Volunteer_supervision
                    on a.Undertaker equals vs.supervision_ID
                    join aa in db.Activity1
                    on a.Activity_no equals aa.Activity_no
                    where aa.Volunteer_no == userid
                    select new
                    {
                        //count 是 某個活動類別(以id判斷) 的 每個活動 的 活動報名人數
                        count = db.Activity1.Where(m => m.Activity_no == a.Activity_no).Select(n => n.Volunteer_no).Distinct().Count(),
                        //db.Activity1 是 Volunteer.Activity 資料表

                        part = db.Activity1.Where(m => m.Activity_no == a.Activity_no).Any(p => p.Volunteer_no == userid),
                        Activity_no = a.Activity_no,
                        Activity_name = a.Activity_name,
                        Activity_type_ID = a.Activity_type_ID,

                        Activity_type1 = at.Activity_type1,
                        Group_name = gr.Group_name,
                        supervision_Name = vs.supervision_Name,

                        Group_no = a.Group_no,
                        Activity_startdate = a.Activity_startdate,
                        Activity_enddate = a.Activity_enddate,
                        Undertake_unit = a.Undertake_unit,
                        Undertaker = a.Undertaker,
                        Undertake_phone = a.Undertake_phone,
                        Undertake_email = a.Undertake_email,
                        lecturer = a.lecturer,
                        Member = a.Member,
                        Spare = a.Spare,
                        Place = a.Place,
                        Summary = a.Summary,
                        Activity_Photo_id = a.Activity_Photo_id,
                        Meal = a.Meal
                    };

            foreach (var item in q)
            {
                acv.Add(new activity_volunteerNo_VM()        /*acv.Add( new 1個 activity_volunteerNo_VM(){ 裡面 放東西} )*/
                {
                    part = item.part,
                    count = item.count,
                    Activity_no = item.Activity_no,
                    Activity_name = item.Activity_name,
                    Activity_type_ID = item.Activity_type_ID,
                    Group_no = item.Group_no,
                    Activity_startdate = item.Activity_startdate,
                    Activity_enddate = item.Activity_enddate,
                    Undertake_unit = item.Undertake_unit,
                    Undertaker = item.Undertaker,
                    Undertake_phone = item.Undertake_phone,
                    Undertake_email = item.Undertake_email,
                    lecturer = item.lecturer,
                    Member = item.Member,
                    Spare = item.Spare,
                    Place = item.Place,
                    Summary = item.Summary,
                    Activity_Photo_id = item.Activity_Photo_id,
                    Meal = item.Meal,

                    Activity_type1 = item.Activity_type1,
                    Group_name = item.Group_name,
                    supervision_Name = item.Group_name


                });
            }

            return acv;

        }

        //依 登入的志工號碼(Volunteer_no) 未參加的活動 分別 列出活動
        public List<activity_volunteerNo_VM> activityByNotParticipated(int userid)
        {
            List<activity_volunteerNo_VM> acv = new List<activity_volunteerNo_VM>();
            VolunteerEntities db = new VolunteerEntities();
            //var a1 = db.Activity1.Select(a => a.Activity_no).ToList();
            //var a2 = db.Activities.Select(a => a.Activity_no).ToList();
            //var a3 = a2.Except(a1);




            //basic.Activity資料表 一筆一筆 導覽到 Volunteer.Activity資料表 ， Volunteer.Activity資料表 裡面 一筆一筆 比對 Volunteer_no == userid 
            var q1 = db.Activities.Where(a => !a.Activity1.Where(p => p.Volunteer_no == userid).Any()).ToList();





            //q1在前面先篩選
            var q = from a in q1
                    join at in db.Activity_type
                    on a.Activity_type_ID equals at.Activity_type_ID
                    join gr in db.Service_group
                    on a.Group_no equals gr.Group_no
                    join vs in db.Volunteer_supervision
                    on a.Undertaker equals vs.supervision_ID
                    select new
                    {
                        //count 是 某個活動類別(以id判斷) 的 每個活動 的 活動報名人數
                        count = db.Activity1.Where(m => m.Activity_no == a.Activity_no).Select(n => n.Volunteer_no).Distinct().Count(),
                        //db.Activity1 是 Volunteer.Activity 資料表

                        part = db.Activity1.Where(m => m.Activity_no == a.Activity_no).Any(p => p.Volunteer_no == userid),
                        Activity_no = a.Activity_no,
                        Activity_name = a.Activity_name,
                        Activity_type_ID = a.Activity_type_ID,

                        Activity_type1 = at.Activity_type1,
                        Group_name = gr.Group_name,
                        supervision_Name = vs.supervision_Name,

                        Group_no = a.Group_no,
                        Activity_startdate = a.Activity_startdate,
                        Activity_enddate = a.Activity_enddate,
                        Undertake_unit = a.Undertake_unit,
                        Undertaker = a.Undertaker,
                        Undertake_phone = a.Undertake_phone,
                        Undertake_email = a.Undertake_email,
                        lecturer = a.lecturer,
                        Member = a.Member,
                        Spare = a.Spare,
                        Place = a.Place,
                        Summary = a.Summary,
                        Activity_Photo_id = a.Activity_Photo_id,
                        Meal = a.Meal
                    };



            foreach (var item in q)
            {
                acv.Add(new activity_volunteerNo_VM()        /*acv.Add( new 1個 activity_volunteerNo_VM(){ 裡面 放東西} )*/
                {
                    part = item.part,
                    count = item.count,
                    Activity_no = item.Activity_no,
                    Activity_name = item.Activity_name,
                    Activity_type_ID = item.Activity_type_ID,
                    Group_no = item.Group_no,
                    Activity_startdate = item.Activity_startdate,
                    Activity_enddate = item.Activity_enddate,
                    Undertake_unit = item.Undertake_unit,
                    Undertaker = item.Undertaker,
                    Undertake_phone = item.Undertake_phone,
                    Undertake_email = item.Undertake_email,
                    lecturer = item.lecturer,
                    Member = item.Member,
                    Spare = item.Spare,
                    Place = item.Place,
                    Summary = item.Summary,
                    Activity_Photo_id = item.Activity_Photo_id,
                    Meal = item.Meal,

                    Activity_type1 = item.Activity_type1,
                    Group_name = item.Group_name,
                    supervision_Name = item.Group_name


                });
            }

            return acv;
        }




    }
}
