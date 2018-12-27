using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Volunteer_Web.Models;

namespace Volunteer_Web.Controllers
{
    public class UnitController : NeedCheckLogin
    {
        private Repository<Application_unit> applicationRepository = new Repository<Application_unit>();
        private VolunteerEntities db = new VolunteerEntities();

        public UnitController()
        {
            IsCheck = true;
        }

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

            if (img.ToList().First() == null)
            {
                TempData["Image"] = "hospital.png";
            }
            else
            {
                TempData["Image"] = img.ToList().First();
            }


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
           string now = DateTime.Now.ToString("yyyyMMddhhmmss");
            int unit = Convert.ToInt32(Session["UserID"]);                         //目前登入的單位ID
            string strPath = Request.PhysicalApplicationPath + @"Images\" + now + ImagePath.FileName; //存進資料夾的圖片路徑

            Application_unit Application_unit = applicationRepository.GetByid(Convert.ToInt32(Request.Form["Application_unit_no"]));
        
            //string[] pathword = ImagePath.FileName.Split(".".ToCharArray());
            //string s = pathword[0] + now +"."+ pathword[1];

            
            ImagePath.SaveAs(strPath);
            
                TempData["Image"] = now + ImagePath.FileName;



            var img = from a in db.Application_unit
                      join ac in db.accounts on a.Application_unit_no equals ac.User_ID
                      where ac.Permission == "Application_unit" && a.Application_unit_no == unit
                      select a.ImagePath;

           string deletepath = Request.PhysicalApplicationPath + @"Images\" + img.ToList().First();

          if(System.IO.File.Exists(deletepath))         //換照片成功後刪除舊的圖檔
            {
                try
                {
                    System.IO.File.Delete(deletepath);
                }
                catch (System.IO.IOException e)
                {
                    Console.WriteLine(e.Message);                 
                }
            }

            Application_unit.ImagePath = now + ImagePath.FileName;                    //更新進資料庫
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