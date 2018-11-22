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
        public ActionResult NewVolunteer()
        {
            return View();
        }
    }
}