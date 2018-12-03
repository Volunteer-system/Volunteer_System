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
        public static IQueryable<AbnormaleventStageVM> selectit = null;

        // GET: Abnormalevent
        public ActionResult Index()
        {            
            int unit = Convert.ToInt32(Session["UserID"]);                //搜尋近3個月通報
            DateTime date = DateTime.Now.Date.AddMonths(-3);

            var q = from a in db.Abnormal_event
                    join s in db.Stages on a.Stage equals s.Stage_ID
                    where a.Application_unit_no == unit && a.Notification_date > date
                    orderby a.Notification_date descending
                    select new AbnormaleventStageVM { abnormalevent = a, stage = s };


            var stage = from s in db.Stages                       //階段下拉選單
                        where s.Stage_type == "異常事件"
                        select new
                        {
                            s.Stage_ID,
                            s.Stage1
                        };

            ViewBag.stage = new SelectList(stage, "Stage_ID", "Stage1");


            if (selectit == null)
            {
                return View(q);
            }
            else
            {
                return View(selectit);
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

                _abnormal_Event.Supervisor_ID = 1;
                _abnormal_Event.Abnormal_event_ID = Request.Form["Abnormal_event_ID"];
                _abnormal_Event.Abnormal_event1 = Request.Form["Abnormal_event"];
                _abnormal_Event.Unit_descrition = Request.Form["Unit_description"];
                _abnormal_Event.Application_unit_no = Convert.ToInt32(Session["UserID"]);
                _abnormal_Event.Volunteer_no = Convert.ToInt32(Request.Form["Volunteer_no"]);
                _abnormal_Event.Notification_date = DateTime.Now;

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


            var q = from a in db.Abnormal_event
                    join s in db.Stages on a.Stage equals s.Stage_ID
                    where a.Abnormal_event_no == id
                    select new AbnormaleventStageVM { abnormalevent = a, stage = s };

            return View(q.First());
            // return View(abnormaleventVMRepository.GetByid(id));
        }

        [HttpPost]
        public ActionResult Edit()
        {

            Abnormal_event abnormal_Event = abnormaleventRepository.GetByid(Convert.ToInt32(Request.Form["Abnormal_event_no"]));
            abnormal_Event.Abnormal_event_ID = Request.Form["Abnormal_event_ID"];
            abnormal_Event.Abnormal_event1 = Request.Form["Abnormal_event1"];
            abnormal_Event.Unit_descrition = Request.Form["Unit_descrition"];
            abnormal_Event.Volunteer_no = Convert.ToInt32(Request.Form["Volunteer_no"]);


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


        
        public ActionResult SelectByDate(DateTime? date1, DateTime? date2)
        {
            int selectstage = 0;
            DateTime longago = DateTime.Now.AddYears(-200);
            DateTime future = DateTime.Now.AddDays(1);


            var stageselect = from s in db.Stages                       //階段下拉選單
                              where s.Stage_type == "異常事件" 
                              select new
                              {
                                  s.Stage_ID,
                                  s.Stage1
                              };

            ViewBag.stage = new SelectList(stageselect, "Stage_ID", "Stage1");

            if (Request.Form["Stage"] != null && Request.Form["Stage"] != "")
            {
                selectstage = Convert.ToInt32(Request.Form["Stage"]);
            }


            if ((date1 != null || date2 != null) && (selectstage != 0))      //有選階段
            {
                var searchDate = from a in db.Abnormal_event
                                 join s in db.Stages on a.Stage equals s.Stage_ID
                                 where a.Notification_date > ((date1 == null) ? longago : date1) && a.Notification_date < ((date2 == null) ? future : date2) && a.Stage == selectstage
                                 orderby a.Notification_date descending
                                 select new AbnormaleventStageVM { abnormalevent = a, stage = s };

                selectit = searchDate;
                return RedirectToAction("Index");
            }

            else if ((date1 != null || date2 != null) && (selectstage == 0))    //沒選階段
            {
                var searchDate = from a in db.Abnormal_event
                                 join s in db.Stages on a.Stage equals s.Stage_ID
                                 where a.Notification_date > ((date1 == null) ? longago : date1) && a.Notification_date < ((date2 == null) ? future : date2)
                                 orderby a.Notification_date descending
                                 select new AbnormaleventStageVM { abnormalevent = a, stage = s };

                selectit = searchDate;
                return RedirectToAction("Index");

            }

            else if ((date1 == null && date2 == null) && (selectstage != 0))  //沒選日期有選階段
            {
                var searchDate = from a in db.Abnormal_event
                                 join s in db.Stages on a.Stage equals s.Stage_ID
                                 where a.Stage == selectstage
                                 orderby a.Notification_date descending
                                 select new AbnormaleventStageVM { abnormalevent = a, stage = s };
                selectit = searchDate;                
                return RedirectToAction("Index");
            }

            else
            {
                var searchDate = from a in db.Abnormal_event
                                 join s in db.Stages on a.Stage equals s.Stage_ID
                                 where a.Stage ==-1
                                 orderby a.Notification_date descending
                                 select new AbnormaleventStageVM { abnormalevent = a, stage = s };
                selectit = searchDate;

                return RedirectToAction("Index");
                //return View("Index", new List<AbnormaleventStageVM>());
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
                    where s.Stage1 == "事件取消" && s.Stage_type=="異常事件"
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

    }
}