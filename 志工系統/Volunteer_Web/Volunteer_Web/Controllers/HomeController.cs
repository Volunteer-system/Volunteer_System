using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Volunteer_Web.Models;
using Volunteer_Web.ViewModel;


namespace Volunteer_Web.Controllers
{
    public class HomeController : Controller
    {
        VolunteerEntities dbContext = new VolunteerEntities();

        // GET: Home
        //首頁
        public ActionResult Index()
        {
            return View();
        }
        //志工隊介紹
        public ActionResult Introduction()
        {
            return View();
        }
        //新志工申請
        public ActionResult NewVolunteer(int id = 1)
        {
            switch (id)
            {
                case 1:
                    ViewBag.Partilview = "Requirements";
                    break;
                case 2:
                    ViewBag.Partilview = "basic_info";
                    break;
                case 3:
                    ViewBag.Partilview = "Questionnaire";
                    break;
                case 4:
                    ViewBag.Partilview = "Service_period";
                    break;
                case 5:
                    break;
                case 6:
                    ViewBag.Partilview = "Alldata_check";
                    break;
                case 7:
                    ViewBag.Partilview = "Requirement_ok";
                    break;
            }

            return View();
        }
        //申請須知
        public ActionResult Requirements()
        {
            ViewBag.Partilview = "Requirements";
            return PartialView();
        }
        //基本資料
        [HttpGet]
        public ActionResult basic_info()
        {
            ViewBag.Partilview = "basic_info";
            ViewBag.Expertise = dbContext.Expertise1.ToList();
            return PartialView();
        }

        [HttpPost]
        public ActionResult basic_info(Sign_up_session sign_Up_Session)
        {
            ViewBag.Partilview = "basic_info";
            ViewBag.Expertise = dbContext.Expertise1.ToList();
            Session["Sign_up_session"] = sign_Up_Session;
            return PartialView();
        }
        //問券調查
        [HttpGet]
        public ActionResult Questionnaire()
        {
            ViewBag.Partilview = "Questionnaire";

            return PartialView();
        }

        [HttpPost]
        public ActionResult Questionnaire(Sign_up_questionnaireVM SQ)
        {
            ViewBag.Partilview = "Questionnaire";
            Session["Question"] = SQ;
            return PartialView();
        }
        //服務時段調查
        public ActionResult Service_period()
        {
            return PartialView();
        }
        //檢查資料
        public ActionResult Alldata_check()
        {
            Sign_up_session sign_Up_Session = new Sign_up_session();
            sign_Up_Session = Session["Sign_up_session"] as Sign_up_session;
            return PartialView(sign_Up_Session);
        }

        public ActionResult InsertSign_up()
        {
            return RedirectToAction("NewVolunteer");
        }
        //申請完成
        public ActionResult Requirement_ok()
        {
            ViewBag.Partilview = "Requirement_ok";
            return PartialView();
        }
    }
}