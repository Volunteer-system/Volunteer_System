using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Volunteer_Web.Models;

namespace Volunteer_Web.Controllers
{
    public class UnitController : Controller
    {
        private Repository<Application_unit> applicationRepository = new Repository<Application_unit>();
        private VolunteerEntities db = new VolunteerEntities();

        // GET: Unit
        //運用單位基本資料
        
        public ActionResult Index()
        {
            int unit = Convert.ToInt32(Session["UserID"]);
            string unittype = Session["UserType"].ToString();

            var img = from a in db.Application_unit
                      join ac in db.accounts on a.Application_unit_no equals ac.User_ID
                      where ac.Permission == "Application_unit" && a.Application_unit_no == unit
                      select a.ImagePath;

            TempData["Image"] = img.ToList().First();


            var q = from a in db.Application_unit
                    join ac in db.accounts on a.Application_unit_no equals ac.User_ID
                    where ac.Permission == "Application_unit" && a.Application_unit_no==unit
                    select a;


            return View(q.First());

            
        }


        [HttpGet]
        public ActionResult Upload()
        {

            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Upload(Application_unit Application_unit_no, HttpPostedFileBase ImagePath)
        {
            Application_unit Application_unit = applicationRepository.GetByid(Convert.ToInt32(Request.Form["Application_unit_no"]));

            string strPath = Request.PhysicalApplicationPath + @"Images\" + ImagePath.FileName;
            ImagePath.SaveAs(strPath);

            

            //db.Application_unit.Add(application_Unit);
           // db.SaveChanges();
          TempData["Image"]= ImagePath.FileName;
            ViewBag.Path = strPath;

            Application_unit.ImagePath = ImagePath.FileName;
            applicationRepository.Update(Application_unit);


            return RedirectToAction("Index");
        }




        public ActionResult GetImage(string fileName)
        {
            return File("~/Images/" + fileName, "image/jpeg");
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