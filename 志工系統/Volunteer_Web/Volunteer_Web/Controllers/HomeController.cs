using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Volunteer_Web.Models;

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
        public ActionResult NewVolunteer(int id=1)
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
                    break;
                case 5:
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
        public ActionResult basic_info()
        {
            ViewBag.Partilview = "basic_info";
            ViewBag.Expertise = dbContext.Expertise1.ToList();
            return PartialView();
        }
        //問券調查
        public ActionResult Questionnaire()
        {
            ViewBag.Partilview = "Questionnaire";
            return PartialView();
        }
        //申請完成
        public ActionResult Requirement_ok()
        {
            ViewBag.Partilview = "Requirement_ok";
            return PartialView();
        }
    }
}