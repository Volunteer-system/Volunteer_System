using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Volunteer_Web.Controllers
{
    public class UnitController : Controller
    {
        // GET: Unit
        //運用單位基本資料
        public ActionResult Index()
        {
            return View();
        }
        //人力需求申請
        public ActionResult Manpower_apply()
        {
            return View();
        }
        //異常事件通報
        public ActionResult Abnormal_event()
        {
            return View();
        }
    }
}