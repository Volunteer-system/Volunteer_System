using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Volunteer_Web.Controllers
{
    public class HomeController : Controller
    {
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

        public ActionResult Requirements()
        {
            ViewBag.Partilview = "Requirements";
            return PartialView();
        }

        public ActionResult basic_info()
        {
            ViewBag.Partilview = "basic_info";
            return PartialView();
        }

        public ActionResult Questionnaire()
        {
            ViewBag.Partilview = "Questionnaire";
            return PartialView();
        }

        public ActionResult Requirement_ok()
        {
            ViewBag.Partilview = "Requirement_ok";
            return PartialView();
        }
    }
}