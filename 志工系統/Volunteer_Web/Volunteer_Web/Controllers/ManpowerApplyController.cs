using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Volunteer_Web.Models;
using Volunteer_Web.ViewModel;

namespace Volunteer_Web.Controllers
{
    public class ManpowerApplyController : Controller
    {
        private IRepository<Manpower_apply> manpowerapplyRepository = new Repository<Manpower_apply>();
        Manpower_apply _manpower_apply = new Manpower_apply();
        VolunteerEntities dbContext = new VolunteerEntities();


        // GET: ManpowerApply
        public ActionResult Index()
        {
            var q = from m in dbContext.Manpower_apply
                    join s in dbContext.Stages on m.Apply_state equals s.Stage_ID
                    orderby m.Apply_date descending
                    select new Manpower_applyStageVM { manpower_apply = m, stage = s, };


            return View(q);
        }

        public ActionResult Insert()
        {
            if (Request.Form.Count > 0)
            {
                _manpower_apply.Application_unit_no = 1;  //運用單位ID
                _manpower_apply.Applicant = Request.Form["Applicant"];  //申請人
                _manpower_apply.Apply_date = DateTime.Now;  //申請日期
                _manpower_apply.Applicant_phone = Request.Form["Applicant_phone"];  //申請人電話
                _manpower_apply.Work_place = Request.Form["Work_place"];  //值班地點]
                _manpower_apply.Apply_description = Request.Form["Apply_description"];  //工作項目與流程
                _manpower_apply.Application_unit_Supervisor = Request.Form["Application_unit_Supervisor"];  //單位主管
                _manpower_apply.Application_unit_heads = Request.Form["Application_unit_heads"];  //部門主管
                _manpower_apply.Application_number = Convert.ToInt32(Request.Form["Application_number"]);  //申請人數

                var q = from s in dbContext.Stages
                        where s.Stage1 == "新申請"
                        select s.Stage_ID;
                _manpower_apply.Apply_state = q.ToList().First();//申請階段

                manpowerapplyRepository.Create(_manpower_apply);

                return RedirectToAction("Index");
            }
            return View();
        }


        [HttpGet]
        public ActionResult Edit(int id = 1)
        {
            var q = from m in dbContext.Manpower_apply
                    join s in dbContext.Stages on m.Apply_state equals s.Stage_ID
                    where m.Apply_ID == id
                    select new Manpower_applyStageVM { manpower_apply = m, stage = s, };

            return View(q.First());
        }
        [HttpPost]
        public ActionResult Edit()
        {
            Manpower_apply _manpower_apply = manpowerapplyRepository.GetByid(Convert.ToInt32(Request.Form["Apply_ID"]));
            //_manpower_apply.Application_unit_no = 1;  //運用單位ID
            _manpower_apply.Applicant = Request.Form["Applicant"];  //申請人
            _manpower_apply.Applicant_phone = Request.Form["Applicant_phone"];  //申請人電話
            _manpower_apply.Work_place = Request.Form["Work_place"];  //值班地點]
            _manpower_apply.Apply_description = Request.Form["Apply_description"];  //工作項目與流程
            _manpower_apply.Application_unit_Supervisor = Request.Form["Application_unit_Supervisor"];  //單位主管
            _manpower_apply.Application_unit_heads = Request.Form["Application_unit_heads"];  //部門主管
            _manpower_apply.Application_number = Convert.ToInt32(Request.Form["Application_number"]);  //申請人數

            manpowerapplyRepository.Update(_manpower_apply);

            return RedirectToAction("Index");
        }

        public ActionResult Cancel(int id)
        {
            var q = from s in dbContext.Stages
                    where s.Stage1 == "取消申請" && s.Stage_type == "人力申請"
                    select s.Stage_ID;

            Manpower_apply manpower_apply = manpowerapplyRepository.GetByid(id);

            manpower_apply.Apply_state = q.ToList().First();
            manpowerapplyRepository.Update(manpower_apply);

            return RedirectToAction("Index");

        }

        public ActionResult Detail(int id = 1)
        {
            var q = from m in dbContext.Manpower_apply
                    join s in dbContext.Stages on m.Apply_state equals s.Stage_ID
                    where m.Apply_ID == id
                    select new Manpower_applyStageVM { manpower_apply = m, stage = s, };

            var test = q.First();

            return View(q.First());
        }

        public ActionResult test()
        {
            return View();
        }

        public ActionResult All()
        {
            var search = from m in dbContext.Manpower_apply
                         join s in dbContext.Stages on m.Apply_state equals s.Stage_ID
                         orderby m.Apply_date descending
                         select new Manpower_applyStageVM { manpower_apply = m, stage = s, };

            return View("Index", search);
        }

        public ActionResult New()
        {
            var search = from m in dbContext.Manpower_apply
                         join s in dbContext.Stages on m.Apply_state equals s.Stage_ID
                         where s.Stage1 == "新申請" && s.Stage_type == "人力申請"
                         orderby m.Apply_date descending
                         select new Manpower_applyStageVM { manpower_apply = m, stage = s, };

            return View("Index", search);
        }

        public ActionResult Processing()
        {
            var search = from m in dbContext.Manpower_apply
                         join s in dbContext.Stages on m.Apply_state equals s.Stage_ID
                         where s.Stage1 == "處理中" && s.Stage_type == "人力申請"
                         orderby m.Apply_date descending
                         select new Manpower_applyStageVM { manpower_apply = m, stage = s, };

            return View("Index", search);
        }

        public ActionResult Complete()
        {
            var search = from m in dbContext.Manpower_apply
                         join s in dbContext.Stages on m.Apply_state equals s.Stage_ID
                         where s.Stage1 == "申請完成" && s.Stage_type == "人力申請"
                         orderby m.Apply_date descending
                         select new Manpower_applyStageVM { manpower_apply = m, stage = s, };

            return View("Index", search);
        }

        public ActionResult Overrule()
        {
            var search = from m in dbContext.Manpower_apply
                         join s in dbContext.Stages on m.Apply_state equals s.Stage_ID
                         where s.Stage1 == "申請駁回" && s.Stage_type == "人力申請"
                         orderby m.Apply_date descending
                         select new Manpower_applyStageVM { manpower_apply = m, stage = s, };

            return View("Index", search);
        }

        public ActionResult Revoke()
        {
            var search = from m in dbContext.Manpower_apply
                         join s in dbContext.Stages on m.Apply_state equals s.Stage_ID
                         where s.Stage1 == "取消申請" && s.Stage_type == "人力申請"
                         orderby m.Apply_date descending
                         select new Manpower_applyStageVM { manpower_apply = m, stage = s, };

            return View("Index", search);
        }

        public ActionResult SelectDate()
        {

            DateTime date1 = DateTime.Now.AddDays(-5);
            DateTime date2 = DateTime.Now;

           var search = from m in dbContext.Manpower_apply
                         join s in dbContext.Stages on m.Apply_state equals s.Stage_ID
                         where s.Stage1 == "新申請" && s.Stage_type == "人力申請" && m.Apply_date>date1 &&m.Apply_date<date2
                         orderby m.Apply_date descending
                         select new Manpower_applyStageVM { manpower_apply = m, stage = s, };

            return View("Index", search);
        }
    }
}