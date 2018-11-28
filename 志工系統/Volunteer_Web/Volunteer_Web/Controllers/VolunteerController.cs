using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Volunteer_Web.Models;

namespace Volunteer_Web.Controllers
{
    public class VolunteerController : NeedCheckLogin////Controller
    {   //繼承登入帳戶驗證
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