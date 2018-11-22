using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Volunteer_Web.Controllers
{
    public class VolunteerController : Controller
    {
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
        public ActionResult Schedule()
        {
            return View();
        }
    }
}