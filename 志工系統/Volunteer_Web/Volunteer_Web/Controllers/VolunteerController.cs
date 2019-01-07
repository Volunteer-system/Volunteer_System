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
        public ActionResult Activity_History()
        {
            ActivityHistoryModel AHM = new ActivityHistoryModel();            
            var SM = AHM.GetHistoryActivity(Convert.ToInt32(Session["UserID"]));

            return View(SM);
        }
        //歷史活動Partial
        public ActionResult Activity_DetailPartial(int id = 0)
        {
            ActivityDetail AD = new ActivityDetail();
            var act = AD.GetActivity(id);
            ViewBag.photoids = AD.GetActivityPhoto(id);
            return PartialView(act);
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

            vm.acvno_VM = actvm.activity_By_Activity_type_ID(id, Convert.ToInt32(Session["UserID"]));
            vm.activity_types = db.Activity_type.ToList();
            ViewBag.userid = Session["UserID"];
            ViewBag.Activity_type_ID = id;

            //activity_volunteerNo_VM avvm = new activity_volunteerNo_VM();
            //ViewBag.actNumOfPeople = avvm.activityNumberOfPeople(1107);
            return View(vm);
        }
        [HttpPost]
        public ActionResult Activity_Browse_DateFilter(activity_volunteerNo_VM avvm)
        {

            DateTime startDate = (DateTime)avvm.Activity_startdate;
            DateTime endDate = (DateTime)avvm.Activity_enddate;
            int activity_typeID = (int)avvm.Activity_type_ID;


            Activity_Photo_Schedule_typeVM vm = new Activity_Photo_Schedule_typeVM();
            activity_volunteerNo_VM actvm = new activity_volunteerNo_VM();

            vm.acvno_VM = actvm.activity_By_Activity_type_ID(activity_typeID, startDate, endDate, Convert.ToInt32(Session["UserID"]));
            vm.activity_types = db.Activity_type.ToList();
            ViewBag.userid = Session["UserID"];
            ViewBag.Activity_type_ID = activity_typeID;

            //找出活動類別 放到 ViewBag.acty 
            var t = vm.acvno_VM.Where(v => v.Activity_type_ID == avvm.Activity_type_ID).Select(a => a.Activity_type1).Distinct().ToList();
            ViewBag.acty = t[0];

            ViewBag.startDate = startDate;
            ViewBag.endDate = endDate;
            ViewBag.ss = false;
            if (ViewBag.startDate != null && ViewBag.endDate != null)
            {
                ViewBag.ss = true;
            }
            return View("Activity_Browse", vm);

        }
        [HttpPost]
        public ActionResult Activity_Browse(int id, int activityNo, bool VegeYesOrNo = false)
        {

            //if (查詢到的 每個活動的 活動報名人數 < 查詢到的 每個活動的 活動人數上限)
            //{
            //    才要新增一筆Volunteer.Activity表的資料
            //}

            Repository<Activity1> ry = new Repository<Activity1>();
            Activity1 activity1 = new Activity1()
            {

                Volunteer_no = id,
                Activity_no = activityNo,
                Registration_date = DateTime.Now,
                Stage = "未簽收",
                Vegetarian = VegeYesOrNo

            };
            ry.Create(activity1);


            //return Content("感謝您參加活動");
            return Redirect("~/Volunteer/Activity_Browse");

            //public ActionResult Activity_Browse(int id, int activityID, int? activity_no)
            //傳布林值過來的時候會預設是null,可是布林值不能為null,
            //所以我按submit 按鈕的時候 勾選素食便當 value的值 會是 true
            //所以我按submit 按鈕的時候 沒勾選素食便當 value的值 會是 null
            //所以我在[HttpPost]
            //public ActionResult Activity_Browse(int id, int activityID, bool VegeYesOrNo = false)
            //接收 bool VegeYesOrNo 的參數先給它預設 false
        }
        public ActionResult Activity_Participate()
        {
            Activity_Photo_Schedule_typeVM vm = new Activity_Photo_Schedule_typeVM();
            activity_volunteerNo_VM actvm = new activity_volunteerNo_VM();
            vm.acvno_VM = actvm.activityByAlreadyParticipated(Convert.ToInt32(Session["UserID"]));
            vm.activity_types = db.Activity_type.ToList();
            ViewBag.userid = Session["UserID"];

            return View(vm);
            //return View("Activity_Browse", vm);
        }

        public ActionResult Activity_NotParticipate()
        {
            Activity_Photo_Schedule_typeVM vm = new Activity_Photo_Schedule_typeVM();
            activity_volunteerNo_VM actvm = new activity_volunteerNo_VM();
            vm.acvno_VM = actvm.activityByNotParticipated(Convert.ToInt32(Session["UserID"]));
            vm.activity_types = db.Activity_type.ToList();
            ViewBag.userid = Session["UserID"];
            //activity_volunteerNo_VM avvm = new activity_volunteerNo_VM();
            //ViewBag.actNumOfPeople = avvm.activityNumberOfPeople(1107);
            //return View(vm);
            return View("Activity_Browse", vm);

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
        public ActionResult Schedule(Volunteer_Schedule_saveModel[] SM)
        {
            if (SM != null)
            {
                Volunteer_Schedule_saveModel sm = new Volunteer_Schedule_saveModel();
                try
                {
                    sm.Insert_Volunteer_Service_period(SM);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return Json("成功", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失敗", JsonRequestBehavior.AllowGet);
            }
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
        public ActionResult GetCalendar(int id = 0, string month = "")
        {
            if (id != 0)
            {
                ActivityHistoryModel AHM = new ActivityHistoryModel();
                return Json(AHM.GetHistoryActivity(id), JsonRequestBehavior.AllowGet);
            }
            else if (month != "")
            {
                Volunteer_Index_Schedule VIS = new Volunteer_Index_Schedule();
                var workschedule = VIS.GetWorkTime(Convert.ToInt32(Session["UserID"]), month);
                return Json(workschedule, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return new EmptyResult();
            }
        }
        //顯示班表細項
        public ActionResult GetSchedule_detail_Partial(int Volunteer_no, string Service_period_name, string Worktime)
        {
            Volunteer_Index_Schedule VIS = new Volunteer_Index_Schedule();
            var detail = VIS.GetSchedule(Volunteer_no, Service_period_name, Worktime);
            return PartialView(detail);
        }
    }
}