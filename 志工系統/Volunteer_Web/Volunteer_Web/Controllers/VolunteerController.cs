using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Volunteer_Web.Models;
using Volunteer_Web.Models.VolunteerUse;
using Volunteer_Web.ViewModel;

namespace Volunteer_Web.Controllers
{
    public class VolunteerController : NeedCheckLogin////Controller
    {
        private VolunteerEntities db = new VolunteerEntities();
        //繼承登入帳戶驗證
        public VolunteerController()
        {   //設定要執行驗證
            IsCheck = true;
        }
        // GET: Volunteer
        //志工基本資料
        public ActionResult Index()
        {
            ViewBag.userID = Session["UserID"];
            ActivityHistoryModel AHM = new ActivityHistoryModel();
            var activities = AHM.GetHistoryActivity(Convert.ToInt32(Session["UserID"]));
            return View(activities);
        }
        //活動
        public ActionResult Activity()
        {
            return View();
        }
        public ActionResult MembersDetail()
        {
            VolunteerModel db = new VolunteerModel();
            VolunteerLeader db1 = new VolunteerLeader();
            var volunteer_detail = db.GetAllVolunteer(Convert.ToInt32(Session["UserID"]));
            ViewBag.expertises = db.GetVolunteerExpertise(Convert.ToInt32(Session["UserID"]));
            ViewBag.leaders = db1.GetVolunteerLeader(Convert.ToInt32(Session["UserID"]));
            ViewBag.group = db.GetVolunteerGroup(Convert.ToInt32(Session["UserID"]));
            return View(volunteer_detail);
        }

        //歷史活動
        public ActionResult Activity_History(int id = 0, int photo = 0)
        {
            ActivityHistoryModel AHM = new ActivityHistoryModel();
            //ActivityDetail ad = new ActivityDetail();
            //ShowModel SM = new ShowModel();
            VolunteerEntities db = new VolunteerEntities();
            if (id != 0)
            {
                //SM.ActivityDetail = ad.GetActivity(id);                
                var detail = db.Activities.Where(a => a.Activity_name == id.ToString());
                return Json(detail.AsEnumerable().Select(a => new { Activity_no = a.Activity_no, Activity_name = a.Activity_name, Activity_type = a.Activity_type.Activity_type1, Activity_startdate = a.Activity_startdate.Value.ToShortDateString(), Activity_enddate = a.Activity_enddate.Value.ToShortDateString(), supervision_Name = a.Volunteer_supervision.supervision_Name, supervision_phone = a.Volunteer_supervision.supervision_phone, supervision_email = a.Volunteer_supervision.supervision_email, Group_name = a.Service_group.Group_name, lecturer = a.lecturer, Place = a.Place, Summary = a.Summary, Activity_photo_id = a.Activity_Photo_id }), JsonRequestBehavior.AllowGet);
            }
            if (photo != 0)
            {
                var photoid = db.Activity_photo.Where(p => p.Activity_id == photo);
                return Json(photoid.AsEnumerable().Select(p => new { Activity_photo_id = p.Activity_photo_id }), JsonRequestBehavior.AllowGet);
            }
            var SM = AHM.GetHistoryActivity(Convert.ToInt32(Session["UserID"]));

            return View(SM);
        }
        public ActionResult WorkHours()
        {
            return View();
        }
        //取得班表
        public ActionResult WorkHoursJson(int year = 0)
        {
            WorkHoursModel wh = new WorkHoursModel();
            var w = wh.GetWorkHours(Convert.ToInt32(Session["UserID"]), year);
            return Json(w, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Activity_Create()
        {
            ViewBag.Activity_Type = db.Activity_type.ToList();
            ViewBag.Activity_Group = db.Service_group.ToList();
            ViewBag.Activity_VS = db.Volunteer_supervision.ToList();
            Activity ap = new Activity();
            Act_pho ap1 = new Act_pho();
            return View(ap1);
        }

        [HttpPost]
        public ActionResult Activity_Create(Act_pho ap1, HttpPostedFileBase photo)
        {


            //將檔案轉成二進位存進資料庫中

            var imgSize = photo.ContentLength;
            byte[] imgByte = new byte[imgSize];
            photo.InputStream.Read(imgByte, 0, imgSize);


            ap1.activity_photo.Activity_photo1 = imgByte;
            //product.BytesImage = imgByte;



            //新增到資料庫中

            db.Activities.Add(ap1.activity);
            db.Activity_photo.Add(ap1.activity_photo);
            db.SaveChanges();
            ap1.activity.Activity_Photo_id = ap1.activity_photo.Activity_photo_id;
            var q = db.Activity_photo.Select(n => n.Activity_photo_id).ToList().LastOrDefault();
            var x = db.Activities.Select(c => c.Activity_Photo_id).ToList().LastOrDefault();
            x = q;
            var a = db.Activities.Select(k => k.Activity_no).ToList().LastOrDefault();
            var p = db.Activities.Find(a);
            db.Entry(p).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Activity_Browse");
        }
        //排班意願

        [HttpGet]
        public ActionResult Activity_Browse(int id = 1)
        {
            //string a = null;
            //if(Request.Form.Count>0)
            //{
            //    a = Request.Form["activity_no"];
            //}
            Activity_Photo_Schedule_typeVM vm = new Activity_Photo_Schedule_typeVM();
            //vm.activity = db.Activities.Where(p => p.Activity_type_ID == id);
            activity_volunteerNo_VM actvm = new activity_volunteerNo_VM();
            vm.acvno_VM = actvm.activityNumberOfPeople(id);
            vm.activity_types = db.Activity_type.ToList();
            ViewBag.userid = Session["UserID"];
            //activity_volunteerNo_VM avvm = new activity_volunteerNo_VM();
            //ViewBag.actNumOfPeople = avvm.activityNumberOfPeople(1107);
            return View(vm);
        }
        [HttpPost]
        public ActionResult Activity_Browse(int id, int activityID)
        {
            Repository<Activity1> ry = new Repository<Activity1>();
            Activity1 activity1 = new Activity1()
            {
                Activity_no = activityID,
                Volunteer_no = id,
                Registration_date = DateTime.Now,
                Stage = "未簽收"

            };
            ry.Create(activity1);


            return Content("感謝您參加活動");
        }
        public ActionResult GetImageBytes(int id = 1)
        {
            Activity_photo ap = db.Activity_photo.Find(id);
            byte[] img = ap.Activity_photo1;
            return File(img, "image/jpeg");
        }
        //排班意願
        [HttpGet]
        public ActionResult Schedule()
        {   //取得運用單位
            Volunteer_Schedule_applicationModel vsa = new Volunteer_Schedule_applicationModel();
            var A = vsa.Getapplication();
            Session["Schedule_application"] = A;
            //取的服務組別
            Volunteer_Schedule_GroupModel GM = new Volunteer_Schedule_GroupModel();
            var G = GM.Getgroup();
            Session["Schedule_group"] = G;
            //取得時段
            Volunteer_Schedule_PeriodModel VSP = new Volunteer_Schedule_PeriodModel();
            var P = VSP.Getservice_period();
            Session["Schedule_period"] = P;
            ScheduleModel SM = new ScheduleModel();
            var schedule = SM.GetSchedule(Convert.ToInt32(Session["UserID"]));
            if (schedule != null)
            {
                return View(schedule);
            }
            else
            {
                return View();
            }

        }
        [HttpPost]
        public ActionResult Schedule(List<Volunteer_Schedule_saveModel> SM)
        {
            Volunteer_Schedule_saveModel sm = new Volunteer_Schedule_saveModel();
            sm.Insert_Volunteer_Service_period(SM);
            return Content("成功", "text/plain");
            //return View();
        }//12/22
        //Schedule下拉清單
        public ActionResult Schedule_select(string type)
        {
            switch (type)
            {
                case "A":
                    return Json(Session["Schedule_application"], JsonRequestBehavior.AllowGet);
                case "G":
                    return Json(Session["Schedule_group"], JsonRequestBehavior.AllowGet);
                case "P":
                    return Json(Session["Schedule_period"], JsonRequestBehavior.AllowGet);
            }
            return new EmptyResult(); //不回傳到view     
        }
        //讀取已排過班的資料
        public JsonResult GetJson()
        {  
            VolunteerEntities db = new VolunteerEntities();
            int userID = Convert.ToInt32(Session["UserID"]);

            var volunteer_user = db.Service_period2.Where(v => v.Volunteer_no == userID);
            //var volunteer_user = db.Service_period2.Where(v => v.Volunteer_no == userID).Select(v => new
            //{
            //    v.Wish_order,
            //    v.Service_period_no
            //});
            return Json(volunteer_user, JsonRequestBehavior.AllowGet);
            
        }
        //取得照片
        public ActionResult GetImageByID(int id)
        {
            VolunteerEntities db = new VolunteerEntities();
            var p = db.Activity_photo.Find(id);
            byte[] imgs = p.Activity_photo1;
            return File(imgs, "image/jpeg");
        }
        //取得月曆的顯示
        public ActionResult GetCalendar(int id)
        {
            ActivityHistoryModel AHM = new ActivityHistoryModel();
            return Json(AHM.GetHistoryActivity(id).Select(n => new { Activity_no = n.Activity_no, Activity_name = n.Activity_name, Activity_startdate = n.Activity_startdate.Value.ToString("yyyy-MM-dd"), Activity_enddate = n.Activity_enddate.Value.ToString("yyyy-MM-dd") }), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCalendarJson(int id)
        {
            VolunteerEntities db = new VolunteerEntities();

            var detail = db.Activities.Where(n => n.Activity_no == id);
            Session["photo"] = detail.FirstOrDefault().Activity_no;
            return Json(detail.AsEnumerable().Select(n => new { Activity_name = n.Activity_name, Activity_type1 = n.Activity_type.Activity_type1, Activity_startdate = n.Activity_startdate.Value.ToShortDateString(), Activity_enddate = n.Activity_enddate.Value.ToShortDateString(), Summary = n.Summary, Activity_Photo_id = n.Activity_Photo_id }), JsonRequestBehavior.AllowGet);
        }
    }
}