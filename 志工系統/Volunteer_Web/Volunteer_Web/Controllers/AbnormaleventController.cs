using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Volunteer_Web.Models;
using Volunteer_Web.ViewModel;


namespace Volunteer_Web.Controllers
{
    public class AbnormaleventController : NeedCheckLogin
    {
        private Repository<Abnormal_event> abnormaleventRepository = new Repository<Abnormal_event>();
        private VolunteerEntities db = new VolunteerEntities();

        public AbnormaleventController()
        {
            IsCheck = true;
        }

        // GET: Abnormalevent
        public ActionResult Index(int id = 0)
        {
            #region 下拉選單

            var category = from c in db.event_category      //事件類別下拉選單
                           select new
                           {
                               c.event_category_ID,
                               c.event_category1
                           };

            ViewBag.indexcategory = new SelectList(category, "event_category_ID", "event_category1", Convert.ToInt32(Request.Cookies["category"]["id"]));
            #endregion

            DateTime date1 = new DateTime();
            DateTime date2 = new DateTime();
            DateTime date = DateTime.Now.Date.AddMonths(-3);
            int unit = Convert.ToInt32(Session["UserID"]);
            int categoryid = 0;



            if (id == 0)
            {
                ViewBag.id = -1;
            }

            else
            {
                Response.Cookies["selectid"]["id"] = id.ToString();
                if (Request.Cookies["selectid"] != null)
                {
                    ViewBag.id = id;
                }
                
            }
           


            if (Request.Cookies["selectstage"]["date1"] != null && Request.Cookies["selectstage"]["date1"] !="")
            {

                date1 = DateTime.ParseExact(Request.Cookies["selectstage"]["date1"],"yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            }           

            if (Request.Cookies["selectstage"]["date2"] != null && Request.Cookies["selectstage"]["date2"] != "")
            {
                date2 = DateTime.ParseExact(Request.Cookies["selectstage"]["date2"], "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            }

            if (Request.Cookies["category"]["id"]!=null)
            {
                categoryid= Convert.ToInt32(Request.Cookies["category"]["id"]);
            }




            if (id != 0 && (date1.ToString("yyyyMMdd") != "00010101" || date2.ToString("yyyyMMdd") != "00010101")) //有時間有階段
            {
                ViewBag.date1 = date1.ToString("yyyy-MM-dd");
                ViewBag.date2 = date2.AddDays(-1).ToString("yyyy-MM-dd");

                var q = from a in db.Abnormal_event
                        join s in db.Stages on a.Stage equals s.Stage_ID
                        where a.Application_unit_no == unit && ((id==-1)? true:s.Stage_ID== id) && a.Notification_date >= date1 && a.Notification_date <= date2 && ((categoryid != 0) ? a.event_category_ID == categoryid : true)
                        orderby a.Notification_date descending
                        select new AbnormaleventStageVM { abnormalevent = a, stage = s };

                return View(q.ToList());
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
                            where a.Application_unit_no == unit && ((goodID == -1) ? true : s.Stage_ID == goodID) && a.Notification_date >= date1 && a.Notification_date <= date2 && ((categoryid != 0) ? a.event_category_ID == categoryid : true)
                            orderby a.Notification_date descending
                            select new AbnormaleventStageVM { abnormalevent = a, stage = s };

                    return View(q.ToList());
                }

                else                                                                                     //編輯後搜尋之前條件用(沒時間)           
                {               
                    int goodID = Convert.ToInt32(Request.Cookies["selectid"]["id"]);
                    ViewBag.id = Convert.ToInt32(Request.Cookies["selectid"]["id"]);

                    var q = from a in db.Abnormal_event
                            join s in db.Stages on a.Stage equals s.Stage_ID
                            where a.Application_unit_no == unit && ((goodID == -1) ? true : s.Stage_ID == goodID) && ((categoryid != 0) ? a.event_category_ID == categoryid : true)
                            orderby a.Notification_date descending
                            select new AbnormaleventStageVM { abnormalevent = a, stage = s };

                    return View(q.ToList());
                }
            }

            else if (id == 0 && Request.Cookies["selectid"]["id"] == "0" && date1.ToString("yyyyMMdd") != "00010101" || date2.ToString("yyyyMMdd") != "00010101")//一開始選時間OR加選種類
            {
                ViewBag.date1 = date1.ToString("yyyy-MM-dd");
                ViewBag.date2 = date2.AddDays(-1).ToString("yyyy-MM-dd");

                var q = from a in db.Abnormal_event
                        join s in db.Stages on a.Stage equals s.Stage_ID
                        where a.Application_unit_no == unit && a.Notification_date >= date1 && a.Notification_date <= date2 && ((categoryid != 0) ? a.event_category_ID == categoryid : true)
                        orderby a.Notification_date descending
                        select new AbnormaleventStageVM { abnormalevent = a, stage = s };

                return View(q.ToList());

            }
            else if(id == 0 && Request.Cookies["selectid"]["id"] == "0" && date1.ToString("yyyyMMdd") == "00010101"&&categoryid!=0) //只選種類
            {
                ViewBag.date1 = "";
                ViewBag.date2 = "";

                var q = from a in db.Abnormal_event
                        join s in db.Stages on a.Stage equals s.Stage_ID
                        where a.Application_unit_no == unit && a.event_category_ID == categoryid 
                        orderby a.Notification_date descending
                        select new AbnormaleventStageVM { abnormalevent = a, stage = s };

                return View(q.ToList());
            }


            else if (id != 0 && date1.ToString("yyyyMMdd") == "00010101" && date2.ToString("yyyyMMdd") == "00010101") //有階段沒選時間=>近3個月
            {
                ViewBag.date1 = "";
                ViewBag.date2 = "";

                var q = from a in db.Abnormal_event
                        join s in db.Stages on a.Stage equals s.Stage_ID
                        where a.Application_unit_no == unit && a.Notification_date >= date && ((id == -1) ? true : s.Stage_ID == id) && ((categoryid != 0) ? a.event_category_ID == categoryid : true)
                        orderby a.Notification_date descending
                        select new AbnormaleventStageVM { abnormalevent = a, stage = s };

                return View(q.ToList());
            }


            else                                                                              //一開始近來搜 近3個月
            {
                ViewBag.date1 = "";
                ViewBag.date2 = "";

                var q = from a in db.Abnormal_event
                        join s in db.Stages on a.Stage equals s.Stage_ID
                        where a.Application_unit_no == unit && a.Notification_date >= date
                        orderby a.Notification_date descending
                        select new AbnormaleventStageVM { abnormalevent = a, stage = s };

                return View(q.ToList());

            }

        }


        public ActionResult Insert()
        {
            #region 下拉選單
            int vol = Convert.ToInt32(Session["UserID"]);
            var volunteer_no = from v in db.Volunteer_list   //志工姓名下拉選單
                                                                                                  
                               where v.Application_unit_no == vol
                               select new
                               {
                                   v.Volunteer_no,
                                   v.Volunteer.Chinese_name
                               };

            ViewBag.voluteer = new SelectList(volunteer_no, "Volunteer_no", "Chinese_name"); //後面兩個>值,顯示


            var category = from c in db.event_category      //事件類別下拉選單
                           select new
                           {
                               c.event_category_ID,
                               c.event_category1
                           };

            ViewBag.insertcategory = new SelectList(category, "event_category_ID", "event_category1");
            #endregion

            if (Request.Form.Count > 0)
            {
                Abnormal_event _abnormal_Event = new Abnormal_event();

                _abnormal_Event.Abnormal_event_ID = Request.Form["Abnormal_event_ID"];
                _abnormal_Event.Abnormal_event1 = Request.Form["Abnormal_event"];
                _abnormal_Event.Unit_descrition = Request.Form["Unit_description"];
                _abnormal_Event.Application_unit_no = Convert.ToInt32(Session["UserID"]);
                _abnormal_Event.Volunteer_no = Convert.ToInt32(Request.Form["Volunteer_no"]);
                _abnormal_Event.Notification_date = DateTime.Now;
                _abnormal_Event.event_category_ID= Convert.ToInt32(Request.Form["category"]);
                _abnormal_Event.Supervisor_ID = 1;

                var q = from s in db.Stages
                        where s.Stage1 == "新事件" && s.Stage_type == "異常事件"
                        select s.Stage_ID;

                _abnormal_Event.Stage = q.ToList().First();
                abnormaleventRepository.Create(_abnormal_Event);

                return Content("<script>alert('通報成功!');window.open('" + Url.Content("~/Abnormalevent/Home") + "', '_self')</script>");

            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id = 1)
        {
            #region 下拉選單

            int vol = Convert.ToInt32(Session["UserID"]);

            var volunteer_no = from v in db.Volunteer_list  //志工姓名下拉選單
                               where v.Application_unit_no == vol
                               select new
                               {
                                   v.Volunteer_no,
                                   v.Volunteer.Chinese_name
                               };
            var volid = from a in db.Abnormal_event       //選定
                        where a.Abnormal_event_no == id
                        select a.Volunteer_no;       

            ViewBag.editvoluteer = new SelectList(volunteer_no, "Volunteer_no", "Chinese_name", volid.ToList().First()); //後面兩個>值,顯示


            var category = from c in db.event_category  //事件類別下拉選單
                           select new
                           {
                               c.event_category_ID,
                               c.event_category1
                           };

            var cateid = from a in db.Abnormal_event       //選定
                        where a.Abnormal_event_no == id
                        select a.event_category_ID;


            ViewBag.editcategory = new SelectList(category, "event_category_ID", "event_category1", cateid.ToList().First());

            #endregion
            try
            {
                var q = from a in db.Abnormal_event
                        join s in db.Stages on a.Stage equals s.Stage_ID
                        where a.Abnormal_event_no == id && s.Stage1 == "新事件"|| s.Stage1 == "駁回" && s.Stage_type == "異常事件"
                        select new AbnormaleventStageVM { abnormalevent = a, stage = s };

                return View(q.ToList().First());
            }
            catch
            {
               return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit()
        {
            #region 下拉選單

            int vol = Convert.ToInt32(Session["UserID"]);

            var volunteer_no = from v in db.Volunteer_list  //志工姓名下拉選單
                               where v.Application_unit_no == vol
                               select new
                               {
                                   v.Volunteer_no,
                                   v.Volunteer.Chinese_name
                               };

            ViewBag.editvoluteer = new SelectList(volunteer_no, "Volunteer_no", "Chinese_name"); //後面兩個>值,顯示


            var category = from c in db.event_category  //事件類別下拉選單
                           select new
                           {
                               c.event_category_ID,
                               c.event_category1
                           };

            ViewBag.category = new SelectList(category, "event_category_ID", "event_category1");

            #endregion

            Abnormal_event abnormal_Event = abnormaleventRepository.GetByid(Convert.ToInt32(Request.Form["Abnormal_event_no"]));
            abnormal_Event.Abnormal_event_ID = Request.Form["Abnormal_event_ID"];
            abnormal_Event.Abnormal_event1 = Request.Form["Abnormal_event1"];
            abnormal_Event.Unit_descrition = Request.Form["Unit_descrition"];
            abnormal_Event.Volunteer_no = Convert.ToInt32(Request.Form["Volunteer_no"]);
            abnormal_Event.event_category_ID = Convert.ToInt32(Request.Form["category"]);

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
            
            abnormaleventRepository.Update(abnormal_Event);
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id = 0)
        {
            Abnormal_event abnormal_Event = abnormaleventRepository.GetByid(id);
            abnormaleventRepository.Delete(abnormal_Event);

            return RedirectToAction("Index");
        }


        public ActionResult SelectByDate(DateTime? date1, DateTime? date2 , int? eventcategory=0)
        {

            if (Request.Cookies["category"]["id"] != null)
            {
                Response.Cookies["category"]["id"] = eventcategory.ToString();
            }
            #region 下拉選單

            var category = from c in db.event_category      //事件類別下拉選單
                           select new
                           {
                               c.event_category_ID,
                               c.event_category1
                           };

            ViewBag.indexcategory = new SelectList(category, "event_category_ID", "event_category1", Convert.ToInt32(Request.Cookies["category"]["id"]));
            #endregion
            


            if (date1 != null)
            {
                ViewBag.date1 = date1.Value.ToString("yyyy-MM-dd");
                Response.Cookies["selectstage"]["date1"] = date1.Value.ToString("yyyyMMdd");
                ViewBag.date2 = date2.Value.ToString("yyyy-MM-dd");
                Response.Cookies["selectstage"]["date2"] = date2.Value.AddDays(1).ToString("yyyyMMdd");
            }
            else
            {
                date1 = new DateTime();
                date2 = new DateTime();
                Response.Cookies["selectstage"]["date1"] = "";
                Response.Cookies["selectstage"]["date2"] = "";
            }


            if (Request.Cookies["selectid"]["id"] != "0")    //有選階段
            {
                ViewBag.id = Request.Cookies["selectid"]["id"];
                int thisstage = Convert.ToInt32(Request.Cookies["selectid"]["id"]);

                if (date1.Value.ToString("yyyyMMdd")!= "00010101")//有時間
                {
                    DateTime day2 = date2.Value.AddDays(1);
                    var searchDate = from a in db.Abnormal_event
                                     join s in db.Stages on a.Stage equals s.Stage_ID
                                     where a.Notification_date >= date1 && a.Notification_date <= day2 && ((thisstage!=-1)? a.Stage == thisstage:true) && ((eventcategory != 0) ? a.event_category_ID == eventcategory : true)
                                     orderby a.Notification_date descending
                                     select new AbnormaleventStageVM { abnormalevent = a, stage = s };
                    return View("Index", searchDate.ToList());
                }
                else //沒時間
                {
                    var searchDate = from a in db.Abnormal_event
                                     join s in db.Stages on a.Stage equals s.Stage_ID
                                     where ((thisstage != -1) ? a.Stage == thisstage : true) && ((eventcategory != 0) ? a.event_category_ID == eventcategory : true)
                                     orderby a.Notification_date descending
                                     select new AbnormaleventStageVM { abnormalevent = a, stage = s };
                    return View("Index", searchDate.ToList());
                }
                
                
            }

            else if (Request.Cookies["selectid"]["id"] == "0")   //沒選階段
            {
                if (date1.Value.ToString("yyyyMMdd") != "00010101")//有時間
                {
                    DateTime day2 = date2.Value.AddDays(1);
                    var searchDate = from a in db.Abnormal_event
                                     join s in db.Stages on a.Stage equals s.Stage_ID
                                     where a.Notification_date >= date1 && a.Notification_date <= day2 && ((eventcategory != 0) ? a.event_category_ID == eventcategory : true)
                                     orderby a.Notification_date descending
                                     select new AbnormaleventStageVM { abnormalevent = a, stage = s };

                    return View("Index", searchDate.ToList());
                }
                else  //沒時間
                {
                    var searchDate = from a in db.Abnormal_event
                                     join s in db.Stages on a.Stage equals s.Stage_ID
                                     where ((eventcategory != 0) ? a.event_category_ID == eventcategory : true)
                                     orderby a.Notification_date descending
                                     select new AbnormaleventStageVM { abnormalevent = a, stage = s };

                    return View("Index", searchDate.ToList());
                }

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

            return View(q.ToList().First());
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


        public ActionResult Home()
        {
            return View();
        }


        //Partial View
        [ChildActionOnly]
        public ActionResult Menu()
        {
            return PartialView(db.Stages.Where(s => s.Stage_type == "異常事件")); //不會結合主版
        }

        

    }
}