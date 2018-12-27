using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Volunteer_Web.Models;
using Volunteer_Web.ViewModel;

namespace Volunteer_Web.Controllers
{
    public class ManpowerApplyController : NeedCheckLogin
    {
        private IRepository<Manpower_apply> manpowerapplyRepository = new Repository<Manpower_apply>();
        Manpower_apply _manpower_apply = new Manpower_apply();
        VolunteerEntities dbContext = new VolunteerEntities();

        public ManpowerApplyController()
        {
            IsCheck = true;
        }


        // GET: ManpowerApply
        public ActionResult Index(int id =0)
        {
            int unit = Convert.ToInt32(Session["UserID"]);
            int date = DateTime.Now.Year;
            int year = new int();

            var apply_date = (from m in dbContext.Manpower_apply.ToList()  //年份下拉選單         
                              orderby m.Apply_date descending
                              select new {  Apply_date = m.Apply_date.ToString("yyyy") }).Distinct();

            ViewBag.apply_date = new SelectList(apply_date, "Apply_date", "Apply_date");

 
            //用Cookies記錄搜尋狀態,編輯完回到搜尋的頁面
            if (id != 0)
            {
                Response.Cookies["Manpowerstage"]["id"] = id.ToString();
                ViewBag.Partilview = id;

                var q = from m in dbContext.Manpower_apply
                        join s in dbContext.Stages on m.Apply_state equals s.Stage_ID
                        where m.Application_unit_no==unit&& m.Apply_state == id
                        orderby m.Apply_date descending
                        select new Manpower_applyStageVM { manpower_apply = m, stage = s, };
                return View(q);
            }

            if (Request.Cookies["Manpowerdate"]["year"] != "0")
            {
                year = Convert.ToInt32(Request.Cookies["Manpowerdate"]["year"]);
            }

            if (id != 0 && year != 0)
            {
                var q = from m in dbContext.Manpower_apply
                        join s in dbContext.Stages on m.Apply_state equals s.Stage_ID
                        where m.Application_unit_no == unit && m.Apply_state == id && m.Apply_date.Year == year
                        orderby m.Apply_date descending
                        select new Manpower_applyStageVM { manpower_apply = m, stage = s, };
                return View(q);
            }

            if (id == 0 && Request.Cookies["Manpowerstage"]["id"] != "0")
            {
                if (year != 0)  //編輯完回到之前的搜尋條件(有年份)
                {
                    ViewBag.year = year;
                    int stageid = Convert.ToInt32(Request.Cookies["Manpowerstage"]["id"]);
                    ViewBag.Partilview= Convert.ToInt32(Request.Cookies["Manpowerstage"]["id"]);

                    var q = from m in dbContext.Manpower_apply
                            join s in dbContext.Stages on m.Apply_state equals s.Stage_ID
                            where m.Application_unit_no==unit && s.Stage_ID==stageid&&m.Apply_date.Year==year
                            orderby m.Apply_date descending
                            select new Manpower_applyStageVM { manpower_apply = m, stage = s, };
                    return View(q);
                }
                else  //編輯完回到之前的搜尋條件(沒有年份)
                {
                    int stageid = Convert.ToInt32(Request.Cookies["Manpowerstage"]["id"]);
                    ViewBag.Partilview = Convert.ToInt32(Request.Cookies["Manpowerstage"]["id"]);

                    var q = from m in dbContext.Manpower_apply
                            join s in dbContext.Stages on m.Apply_state equals s.Stage_ID
                            where m.Application_unit_no == unit && s.Stage_ID == stageid
                            orderby m.Apply_date descending
                            select new Manpower_applyStageVM { manpower_apply = m, stage = s, };
                    return View(q);
                }
            }

            else if(id == 0 && Request.Cookies["Manpowerstage"]["id"] == "0" && Request.Cookies["Manpowerdate"]["year"] != "0")
            {
                    ViewBag.year = year;

                    var q = from m in dbContext.Manpower_apply
                            join s in dbContext.Stages on m.Apply_state equals s.Stage_ID
                            where m.Application_unit_no == unit && m.Apply_date.Year == year
                            orderby m.Apply_date descending
                            select new Manpower_applyStageVM { manpower_apply = m, stage = s, };
                    return View(q);
            }

            else
            {
                var q = from m in dbContext.Manpower_apply
                        join s in dbContext.Stages on m.Apply_state equals s.Stage_ID
                        where m.Application_unit_no==unit&& m.Apply_date.Year == date
                        orderby m.Apply_date descending
                        select new Manpower_applyStageVM { manpower_apply = m, stage = s, };
                return View(q);
            }
        }
        
        public ActionResult Insert()
        {
            if (Request.Form.Count > 0)
            {
                int countPeple = Convert.ToInt32(Request.Cookies["countPeple"].Value);

                _manpower_apply.Application_unit_no = Convert.ToInt32(Session["UserID"]);  //運用單位編號
                _manpower_apply.Applicant = Request.Form["Applicant"];  //申請人
                _manpower_apply.Apply_date = DateTime.Now;  //申請日期
                _manpower_apply.Applicant_phone = Request.Form["Applicant_phone"];  //申請人電話
                //_manpower_apply.Work_place = Request.Form["Work_place"];  //值班地點]
                _manpower_apply.Apply_description = Request.Form["Apply_description"];  //工作項目與流程
                _manpower_apply.Application_unit_Supervisor = Request.Form["Application_unit_Supervisor"];  //單位主管
                _manpower_apply.Application_unit_heads = Request.Form["Application_unit_heads"];  //部門主管
                _manpower_apply.Apply_type ="年度申請"; //申請類別
                //_manpower_apply.Application_number = Convert.ToInt32(Request.Form["Application_number"]);  //申請人數
                _manpower_apply.Application_number = countPeple;
                _manpower_apply.Remarks = Request.Form["Remarks"];  //備註

                var q = from s in dbContext.Stages
                        where s.Stage1 == "新申請"
                        select s.Stage_ID;
                _manpower_apply.Apply_state = q.ToList().First(); //申請階段

                manpowerapplyRepository.Create(_manpower_apply);

                int applyID = _manpower_apply.Apply_ID; //新增過後得到新增的applyID
                string morning = Request.Cookies["morning"].Value;
                string afternoon = Request.Cookies["afternoon"].Value;
                string night = Request.Cookies["night"].Value;
                string assistance = Request.Cookies["assistance"].Value; //寫一個靜態方法在model裡面 用來將cookie資料轉型                
                ManpowerDataContext.ConvertAndInsert(applyID, morning, afternoon, night, assistance);
                
                return RedirectToAction("Index");
            }
            return View();
        }
        
        [HttpGet]
        public ActionResult Edit(int id = 0)//這個ID是applyId
        {
            try
            {
                var q = from m in dbContext.Manpower_apply
                        join s in dbContext.Stages on m.Apply_state equals s.Stage_ID
                        where m.Apply_ID == id && (s.Stage1 == "新申請" || s.Stage1 == "申請駁回")
                        select new Manpower_applyStageVM { manpower_apply = m, stage = s, };

                //宣告一個ViewBag 把值用來存放DB裡面的 根據applyID 找到每個applyId的volunteerNumber
                ViewBag.PeriodDictionary = ManpowerDataContext.ReturnDictionary(id);

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
            Manpower_apply _manpower_apply = manpowerapplyRepository.GetByid(Convert.ToInt32(Request.Form["Apply_ID"]));

            _manpower_apply.Applicant = Request.Form["Applicant"];  //申請人
            _manpower_apply.Applicant_phone = Request.Form["Applicant_phone"];  //申請人電話
            //_manpower_apply.Work_place = Request.Form["Work_place"];  //值班地點]
            _manpower_apply.Apply_description = Request.Form["Apply_description"];  //工作項目與流程
            _manpower_apply.Application_unit_Supervisor = Request.Form["Application_unit_Supervisor"];  //單位主管
            _manpower_apply.Application_unit_heads = Request.Form["Application_unit_heads"];  //部門主管
            _manpower_apply.Application_number = Convert.ToInt32(Request.Form["Application_number"]);  //申請人數
            _manpower_apply.Remarks = Request.Form["Remarks"];  //備註

            var q = from s in dbContext.Stages  //申請階段
                    where s.Stage1 == "新申請"
                    select s.Stage_ID;
            _manpower_apply.Apply_state = q.ToList().First();
            
            manpowerapplyRepository.Update(_manpower_apply);
            //呼叫靜態方法
            string morning = Request.Cookies["morning"].Value;
            string afternoon = Request.Cookies["afternoon"].Value;
            string night = Request.Cookies["night"].Value;
            string assistance = Request.Cookies["assistance"].Value;
            int totalPeople = Convert.ToInt32(Request.Cookies["countPeple"].Value);
            ManpowerDataContext.ModifiedPeriod(Convert.ToInt32(Request.Form["Apply_ID"]), morning, afternoon, night, assistance,totalPeople);
            
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
            
            return View(q.First());
        }

        //Partial View
        [ChildActionOnly]
        public ActionResult Menu()
        {
            return PartialView(dbContext.Stages.Where(s=>s.Stage_type=="人力申請").ToList()); //不會結合主版
        }

        public ActionResult SelectYear(int Apply_date)
        {
            var apply_date = (from m in dbContext.Manpower_apply.ToList()  //年份下拉選單
                              orderby m.Apply_date descending
                              select new { Apply_date = m.Apply_date.ToString("yyyy") }).Distinct();

            ViewBag.apply_date = new SelectList(apply_date, "Apply_date", "Apply_date");
            
            if (Request.Cookies["Manpowerstage"]["id"] != "0")
            {
                ViewBag.Partilview = Request.Cookies["Manpowerstage"]["id"];
                int stage = Convert.ToInt32(Request.Cookies["Manpowerstage"]["id"]);

                var searchYear = from m in dbContext.Manpower_apply
                                 join s in dbContext.Stages on m.Apply_state equals s.Stage_ID
                                 where m.Apply_date.Year == Apply_date && m.Apply_state==stage
                                 orderby m.Apply_date descending
                                 select new Manpower_applyStageVM { manpower_apply = m, stage = s, };
                return View("Index", searchYear);
            }
            else
            {
                var searchYear = from m in dbContext.Manpower_apply  
                                 join s in dbContext.Stages on m.Apply_state equals s.Stage_ID
                                 where m.Apply_date.Year == Apply_date
                                 orderby m.Apply_date descending
                                 select new Manpower_applyStageVM { manpower_apply = m, stage = s, };
                return View("Index", searchYear);
            }
        }

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult InsertSuccess(int id=1)
        {
            var q = from m in dbContext.Manpower_apply
                    join s in dbContext.Stages on m.Apply_state equals s.Stage_ID
                    where m.Apply_ID == id
                    select new Manpower_applyStageVM { manpower_apply = m, stage = s, };

            return View(q.First());
        }








        public ActionResult text()
        {
            return View();
        }
    }
}
