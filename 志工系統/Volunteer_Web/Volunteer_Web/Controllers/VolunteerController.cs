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
            return View();
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

        //歷史活動活動
        public ActionResult Activity_History(int id = 0)
        {
            ActivityHistoryModel AHM = new ActivityHistoryModel();
            ActivityDetail ad = new ActivityDetail();
            ShowModel SM = new ShowModel();
            if (id != 0)
            {
                SM.ActivityDetail = ad.GetActivity(id);
                ViewBag.photo = ad.GetActivityPhoto(id);
            }
            SM.ActivityHistoryModel = AHM.GetHistoryActivity(Convert.ToInt32(Session["UserID"]));

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
        //[HttpPost]
        //public ActionResult Activity_Create(Activity activity)
        //{
        //    db.Activities.Add(activity);
        //    db.SaveChanges();
        //    //return View();
        //    return RedirectToAction("Activity_Browse");
        //}
        [HttpPost]
        public ActionResult Activity_Create(Act_pho ap1, HttpPostedFileBase photo)
        {
            //檔案上傳
            //<input type="file" name="ProductImage"
            //<form enctype="multipart/form-data"
            //用HttpPostedFileBase ProductImage來接收傳到Server上的檔案

            //將檔案儲存到一個資料夾中
            //1.足夠的權限 users everyone write
            //2.資料夾的實際路徑 
            //string strPath = Request.PhysicalApplicationPath + @"ProductImages\" + ProductImage.FileName;
            //ProductImage.SaveAs(strPath);

            //product.ProductImage = ProductImage.FileName;

            //將檔案轉成二進位存進資料庫中

            var imgSize = photo.ContentLength;
            byte[] imgByte = new byte[imgSize];
            photo.InputStream.Read(imgByte, 0, imgSize);


            ap1.activity_photo.Activity_photo1 = imgByte;
            //product.BytesImage = imgByte;



            //新增到資料庫中

            db.Activities.Add(ap1.activity);
            db.Activity_photo.Add(ap1.activity_photo);
            //db.Activities.Add(activity);
            db.SaveChanges();
            ap1.activity.Activity_Photo_id = ap1.activity_photo.Activity_photo_id;
            var q = db.Activity_photo.Select(n => n.Activity_photo_id).ToList().LastOrDefault();
            //db.Activities.LastOrDefault().Activity_no = ap1.activity_photo.Activity_photo_id;
            //var q = db.Activities.LastOrDefault().Activity_no;
            var x = db.Activities.Select(c => c.Activity_Photo_id).ToList().LastOrDefault();
            x = q;
            var a = db.Activities.Select(k => k.Activity_no).ToList().LastOrDefault();
            var p = db.Activities.Find(a);
            db.Entry(p).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            //return View();
            return RedirectToAction("Activity_Browse");
        }
        //排班意願

        [HttpGet]
        public ActionResult Activity_Browse(int id = 1)
        {
            Activity_Photo_Schedule_typeVM vm = new Activity_Photo_Schedule_typeVM();
            vm.activity = db.Activities.Where(p => p.Activity_type_ID == id);
            vm.activity_types = db.Activity_type.ToList();

            //var q = from c in db.Activities
            //        let b = c.Activity_photo
            //        select new { b.Activity_photo1 };
            ViewBag.userid = Session["UserID"];
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
        {
            ViewBag.UserID = Session["UserID"];
            return View();
        }
        [HttpPost]
        public ActionResult Schedule(int id,int[] wishid,List<int[]> service_no)
        {
            if (id != 0)
            {
                Repository<Service_period2> db = new Repository<Service_period2>();
                db.Insert_Volunteer_Service_period(id, wishid, service_no);
                return Content("新增/修改成功","text/plain");
            }
            return View();
        }
        //讀取已排過班的資料
        public JsonResult GetJson()
        {  
            VolunteerEntities db = new VolunteerEntities();
            int userID = Convert.ToInt32(Session["UserID"]);

            var volunteer_user = db.Service_period2.Where(v => v.Volunteer_no == userID).Select(v => new
            {
                v.Wish_order, v.Service_period_no
            });
            return Json(volunteer_user, JsonRequestBehavior.AllowGet);
            
        }
    }
}