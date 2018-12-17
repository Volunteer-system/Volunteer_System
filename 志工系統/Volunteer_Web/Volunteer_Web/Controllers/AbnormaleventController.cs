using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Volunteer_Web.Models;
using Volunteer_Web.ViewModel;


namespace Volunteer_Web.Controllers
{
    public class AbnormaleventController : Controller
    {
        private Repository<Abnormal_event> abnormaleventRepository = new Repository<Abnormal_event>();
        private VolunteerEntities db = new VolunteerEntities();
        


        // GET: Abnormalevent
        public ActionResult Index(int id = 0)
        {

            DateTime date1 = new DateTime();
            DateTime date2 = new DateTime();

            int unit = Convert.ToInt32(Session["UserID"]);
            DateTime date = DateTime.Now.Date.AddMonths(-3);

            if (id != 0)
            {
                Response.Cookies["selectid"]["id"] = id.ToString();
                if (Request.Cookies["selectid"] != null)
                {
                    ViewBag.id = id;
                }
            }

            if (Request.Cookies["selectstage"]["date1"] != null)
            {

                date1 = DateTime.ParseExact(Request.Cookies["selectstage"]["date1"],"yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            }           

            if (Request.Cookies["selectstage"]["date2"] != null)
            {
                date2 = DateTime.ParseExact(Request.Cookies["selectstage"]["date2"], "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            }
            


            if (id != 0 && (date1.ToString("yyyyMMdd") != "00010101" || date2.ToString("yyyyMMdd") != "00010101")) //有時間有階段
            {
                ViewBag.date1 = date1.ToString("yyyy-MM-dd");
                ViewBag.date2 = date2.AddDays(-1).ToString("yyyy-MM-dd");

                var q = from a in db.Abnormal_event
                        join s in db.Stages on a.Stage equals s.Stage_ID
                        where a.Application_unit_no == unit && s.Stage_ID == id && a.Notification_date >= date1 && a.Notification_date <= date2
                        orderby a.Notification_date descending
                        select new AbnormaleventStageVM { abnormalevent = a, stage = s };

                return View(q);
            }


            else if (id == 0 && Request.Cookies["selectid"]["id"] != "0" ) 
            {
                if (date1.ToString("yyyyMMdd") != "00010101" || date2.ToString("yyyyMMdd") != "00010101")  //編輯後搜尋之前條件用(有時間)
                {
                    ViewBag.date1 = date1.ToString("yyyy-MM-dd");
                    ViewBag.date2 = date2.AddDays(-1).ToString("yyyy-MM-dd");
                    int goodID = Convert.ToInt32(Request.Cookies["selectid"]["id"]);
                    ViewBag.id = Convert.ToInt32(Request.Cookies["selectid"]["id"]);

                    var q = from a in db.Abnormal_event
                            join s in db.Stages on a.Stage equals s.Stage_ID
                            where a.Application_unit_no == unit && s.Stage_ID == goodID && a.Notification_date >= date1 && a.Notification_date <= date2
                            orderby a.Notification_date descending
                            select new AbnormaleventStageVM { abnormalevent = a, stage = s };

                    return View(q);
                }

                else                                                                                     //編輯後搜尋之前條件用(沒時間)           
                {               
                    int goodID = Convert.ToInt32(Request.Cookies["selectid"]["id"]);
                    ViewBag.id = Convert.ToInt32(Request.Cookies["selectid"]["id"]);

                    var q = from a in db.Abnormal_event
                            join s in db.Stages on a.Stage equals s.Stage_ID
                            where a.Application_unit_no == unit && s.Stage_ID == goodID 
                            orderby a.Notification_date descending
                            select new AbnormaleventStageVM { abnormalevent = a, stage = s };

                    return View(q);
                }
            }

            else if (id == 0 && Request.Cookies["selectid"]["id"] == "0" && date1.ToString("yyyyMMdd") != "00010101" || date2.ToString("yyyyMMdd") != "00010101")//一開始只選時間
            {
                ViewBag.date1 = date1.ToString("yyyy-MM-dd");
                ViewBag.date2 = date2.AddDays(-1).ToString("yyyy-MM-dd");

                var q = from a in db.Abnormal_event
                        join s in db.Stages on a.Stage equals s.Stage_ID
                        where a.Application_unit_no == unit && a.Notification_date >= date1 && a.Notification_date <= date2
                        orderby a.Notification_date descending
                        select new AbnormaleventStageVM { abnormalevent = a, stage = s };

                return View(q);

            }



            else if (id != 0 && date1.ToString("yyyyMMdd") == "00010101" && date2.ToString("yyyyMMdd") == "00010101") //有階段沒選時間=>近3個月
            {
                var q = from a in db.Abnormal_event
                        join s in db.Stages on a.Stage equals s.Stage_ID
                        where a.Application_unit_no == unit && a.Notification_date >= date && s.Stage_ID == id
                        orderby a.Notification_date descending
                        select new AbnormaleventStageVM { abnormalevent = a, stage = s };

                return View(q);
            }


            else                                                                              //一開始近來搜 近3個月
            {
                var q = from a in db.Abnormal_event
                        join s in db.Stages on a.Stage equals s.Stage_ID
                        where a.Application_unit_no == unit && a.Notification_date >= date
                        orderby a.Notification_date descending
                        select new AbnormaleventStageVM { abnormalevent = a, stage = s };

                return View(q);

            }

        }


        public ActionResult Insert()
        {

            int vol = Convert.ToInt32(Session["UserID"]);
            var volunteer_no = from v in db.Volunteer_list                                         //志工姓名下拉選單
                                                                                                   //join vs in db.Volunteers on v.Volunteer_no equals vs.Volunteer_no
                               where v.Application_unit_no == vol
                               select new
                               {
                                   v.Volunteer_no,
                                   v.Volunteer.Chinese_name
                               };

            ViewBag.voluteer = new SelectList(volunteer_no, "Volunteer_no", "Chinese_name"); //後面兩個>值,顯示



            if (Request.Form.Count > 0)
            {
                Abnormal_event _abnormal_Event = new Abnormal_event();

                _abnormal_Event.Abnormal_event_ID = Request.Form["Abnormal_event_ID"];
                _abnormal_Event.Abnormal_event1 = Request.Form["Abnormal_event"];
                _abnormal_Event.Unit_descrition = Request.Form["Unit_description"];
                _abnormal_Event.Application_unit_no = Convert.ToInt32(Session["UserID"]);
                _abnormal_Event.Volunteer_no = Convert.ToInt32(Request.Form["Volunteer_no"]);
                _abnormal_Event.Notification_date = DateTime.Now;
                _abnormal_Event.event_category_ID = 1;

                var q = from s in db.Stages
                        where s.Stage1 == "新事件" && s.Stage_type == "異常事件"
                        select s.Stage_ID;

                _abnormal_Event.Stage = q.ToList().First();
                abnormaleventRepository.Create(_abnormal_Event);

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id = 1)
        {
            int vol = Convert.ToInt32(Session["UserID"]);

            var volunteer_no = from v in db.Volunteer_list                                         //志工姓名下拉選單
                               where v.Application_unit_no == vol 
                               select new
                               {
                                   v.Volunteer_no,
                                   v.Volunteer.Chinese_name
                               };

            ViewBag.editvoluteer = new SelectList(volunteer_no, "Volunteer_no", "Chinese_name"); //後面兩個>值,顯示

            try
            {
                var q = from a in db.Abnormal_event
                        join s in db.Stages on a.Stage equals s.Stage_ID
                        where a.Abnormal_event_no == id && s.Stage1 == "新事件"|| s.Stage1 == "駁回" && s.Stage_type == "異常事件"
                        select new AbnormaleventStageVM { abnormalevent = a, stage = s };

                return View(q.First());
            }
            catch
            {
               return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit()
        {

            Abnormal_event abnormal_Event = abnormaleventRepository.GetByid(Convert.ToInt32(Request.Form["Abnormal_event_no"]));
            abnormal_Event.Abnormal_event_ID = Request.Form["Abnormal_event_ID"];
            abnormal_Event.Abnormal_event1 = Request.Form["Abnormal_event1"];
            abnormal_Event.Unit_descrition = Request.Form["Unit_descrition"];
            abnormal_Event.Volunteer_no = Convert.ToInt32(Request.Form["Volunteer_no"]);

            var q = from s in db.Stages
                    where s.Stage1 == "駁回" && s.Stage_type == "異常事件"
                    select s.Stage_ID;

            var q1 = from s in db.Stages
                    where s.Stage1 == "新事件" && s.Stage_type == "異常事件"
                    select s.Stage_ID;


            if (abnormal_Event.Stage == q.ToList().First())
            {
                abnormal_Event.Stage = q1.ToList().First();
                abnormal_Event.Notification_date = DateTime.Now;
            }




            int vol = Convert.ToInt32(Session["UserID"]);

            var volunteer_no = from v in db.Volunteer_list                                         //志工姓名下拉選單
                               where v.Application_unit_no == vol
                               select new
                               {
                                   v.Volunteer_no,
                                   v.Volunteer.Chinese_name
                               };

            ViewBag.editvoluteer = new SelectList(volunteer_no, "Volunteer_no", "Chinese_name"); //後面兩個>值,顯示

            abnormaleventRepository.Update(abnormal_Event);

            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id = 0)
        {
            Abnormal_event abnormal_Event = abnormaleventRepository.GetByid(id);
            abnormaleventRepository.Delete(abnormal_Event);

            return RedirectToAction("Index");
        }



        public ActionResult SelectByDate(DateTime date1, DateTime date2)
        {
            ViewBag.date1 = date1.ToString("yyyy-MM-dd");
            ViewBag.date2 = date2.ToString("yyyy-MM-dd");
            Response.Cookies["selectstage"]["date1"] = date1.ToString("yyyyMMdd");
            Response.Cookies["selectstage"]["date2"] = date2.AddDays(1).ToString("yyyyMMdd");

            DateTime day2 = date2.AddDays(1);


            if (Request.Cookies["selectid"]["id"] != "0")    //有選階段
            {
                ViewBag.id = Request.Cookies["selectid"]["id"];
                int thisstage = Convert.ToInt32(Request.Cookies["selectid"]["id"]);

                var searchDate = from a in db.Abnormal_event
                                 join s in db.Stages on a.Stage equals s.Stage_ID
                                 where a.Notification_date >= date1 && a.Notification_date <= day2 && a.Stage == thisstage
                                 orderby a.Notification_date descending
                                 select new AbnormaleventStageVM { abnormalevent = a, stage = s };
                return View("Index", searchDate);
            }

            else if (Request.Cookies["selectid"]["id"] == "0")   //沒選階段
            {


                var searchDate = from a in db.Abnormal_event
                                 join s in db.Stages on a.Stage equals s.Stage_ID
                                 where a.Notification_date >= date1 && a.Notification_date <= day2
                                 orderby a.Notification_date descending
                                 select new AbnormaleventStageVM { abnormalevent = a, stage = s };

                return View("Index", searchDate);

            }

            else
            {
                return View("Index", new List<AbnormaleventStageVM>());
            }

        }

        public ActionResult Detail(int id)
        {
            var q = from a in db.Abnormal_event
                    join s in db.Stages on a.Stage equals s.Stage_ID
                    where a.Abnormal_event_no == id
                    select new AbnormaleventStageVM { abnormalevent = a, stage = s };

            return View(q.First());
        }



        public ActionResult Cancel(int id)
        {
            var q = from s in db.Stages
                    where s.Stage1 == "事件取消" && s.Stage_type == "異常事件"
                    select s.Stage_ID;

            Abnormal_event abnormal_Event = abnormaleventRepository.GetByid(id);

            abnormal_Event.Stage = q.ToList().First();
            abnormaleventRepository.Update(abnormal_Event);

            return RedirectToAction("Index");
        }

        public ActionResult test()
        {
            return View();
        }

        //Partial View
        [ChildActionOnly]
        public ActionResult Menu()
        {
            //return View();  //結合主版
            return PartialView(db.Stages.Where(s => s.Stage_type == "異常事件")); //不會結合主版
        }

    }
}