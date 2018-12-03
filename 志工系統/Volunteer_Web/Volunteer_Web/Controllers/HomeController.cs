using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Volunteer_Web.Models;
using Volunteer_Web.ViewModel;



namespace Volunteer_Web.Controllers
{
    public class HomeController : Controller
    {
        VolunteerEntities dbContext = new VolunteerEntities();

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
        public ActionResult NewVolunteer(int id = 1)
        {
            switch (id)
            {
                case 1:
                    ViewBag.Partilview = "Requirements";
                    break;
                case 2:
                    ViewBag.Partilview = "basic_info";
                    break;
                case 3:
                    ViewBag.Partilview = "Questionnaire";
                    break;
                case 4:
                    ViewBag.Partilview = "Service_period";
                    break;
                case 5:
                    break;
                case 6:
                    ViewBag.Partilview = "Alldata_check";
                    break;
                case 7:
                    ViewBag.Partilview = "Requirement_ok";
                    break;
            }

            return View();
        }
        //申請須知
        public ActionResult Requirements()
        {
            ViewBag.Partilview = "Requirements";
            return PartialView();
        }
        //基本資料
        [HttpGet]
        public ActionResult basic_info()
        {
            ViewBag.Partilview = "basic_info";
            ViewBag.Expertise = dbContext.Expertise1.ToList();
            return PartialView();
        }

        [HttpPost]
        public ActionResult basic_info(Sign_up_session sign_Up_Session)
        {
            ViewBag.Partilview = "basic_info";
            ViewBag.Expertise = dbContext.Expertise1.ToList();
            Session["Sign_up_session"] = sign_Up_Session;
            return PartialView();
        }
        //問券調查
        [HttpGet]
        public ActionResult Questionnaire()
        {
            ViewBag.Partilview = "Questionnaire";

            return PartialView();
        }

        [HttpPost]
        public ActionResult Questionnaire(Sign_up_questionnaireVM SQ)
        {
           
            ViewBag.Partilview = "Questionnaire";
            Session["Question"] = SQ;
            return PartialView();
        }
        //服務時段調查
        public ActionResult Service_period()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Schedule(int[] service_no)
        {
            Session["service_no"] = service_no;
            return PartialView();
        }
        //檢查資料
        public ActionResult Alldata_check()
        {
            
            Sign_up_session sign_Up_Session = new Sign_up_session();
            sign_Up_Session = Session["Sign_up_session"] as Sign_up_session;
            Sign_up_questionnaireVM Sign_up_question = new Sign_up_questionnaireVM();
            Sign_up_question = Session["Question"] as Sign_up_questionnaireVM;

            Sign_up_Alldata_VM sign_Up_Alldata_VM = new Sign_up_Alldata_VM();
            sign_Up_Alldata_VM.SetSign_up_Alldata(sign_Up_Session, Sign_up_question);

            return PartialView(sign_Up_Alldata_VM);
        }

        public ActionResult InsertSign_up()
        {
          //session轉VM
          Sign_up_session sign_Up_Session = Session["Sign_up_session"] as Sign_up_session;
          Sign_up_questionnaireVM  Sign_up_question = Session["Question"] as Sign_up_questionnaireVM;

          //轉dbset並存檔
              //存個人資料   日期通通是問號
                Sign_up s = new Sign_up();
                s.Chinese_name = sign_Up_Session.Chinese_name;
                s.Sex = sign_Up_Session.Sex;
                s.Birthday = DateTime.Now;
            s.Phone = sign_Up_Session.Phone;
                s.Mobile = sign_Up_Session.Mobile;
                s.Email = sign_Up_Session.Email;
                s.Address = sign_Up_Session.Address;
                s.Education = sign_Up_Session.Education;
                s.Job = sign_Up_Session.Job;
                s.Sign_up_date = DateTime.Now;
            s.Approval_date= DateTime.Now;
            dbContext.Entry(s).State = System.Data.Entity.EntityState.Added;
            dbContext.SaveChanges();
            //存專長資料
            //foreach (var exp in sign_Up_Session.Expertises)
            //{
            //   exp.First();
            //}
            //存表單資料
            int Number= dbContext.Sign_up.Where(p=>p.Chinese_name==s.Chinese_name).First().Sign_up_no;
            //存Q1
            foreach (var q1 in Sign_up_question.Q1)
            {
                Sign_up_questionnaire sq = new Sign_up_questionnaire();
                sq.Sign_up_no = Number;
                sq.Question_no = 1;
               
                sq.Question_num =Convert.ToInt32(q1.Substring(0,1));
                dbContext.Entry(sq).State = System.Data.Entity.EntityState.Added;
            }
            Sign_up_questionnaire q1else = new Sign_up_questionnaire();
            q1else.Sign_up_no = Number;
            q1else.Question_no = 1;
            q1else.Other_result1 = Sign_up_question.Q1else;
            dbContext.Entry(q1else).State = System.Data.Entity.EntityState.Added;

            //存Q2
            foreach (var q2 in Sign_up_question.Q2)
            {
                Sign_up_questionnaire sq = new Sign_up_questionnaire();
                sq.Sign_up_no = Number;
                sq.Question_no = 2;
                sq.Question_num = Convert.ToInt32(q2.Substring(0, 1));
                dbContext.Entry(sq).State = System.Data.Entity.EntityState.Added;
            }
            Sign_up_questionnaire q2else = new Sign_up_questionnaire();
            q2else.Sign_up_no = Number;
            q2else.Question_no = 2;
            q2else.Other_result1 = Sign_up_question.Q2else;
            dbContext.Entry(q1else).State = System.Data.Entity.EntityState.Added;


            //存Q3
            foreach (var q3 in Sign_up_question.Q3)
            {
                Sign_up_questionnaire sq = new Sign_up_questionnaire();
                sq.Sign_up_no = Number;
                sq.Question_no = 3;
                sq.Question_num = Convert.ToInt32(q3.Substring(0, 1));
                dbContext.Entry(sq).State = System.Data.Entity.EntityState.Added;
            }
            Sign_up_questionnaire q3doc = new Sign_up_questionnaire();
            q3doc.Sign_up_no = Number;
            q3doc.Question_no = 3;
            q3doc.Other_result1 = Sign_up_question.Q3doc;
            dbContext.Entry(q3doc).State = System.Data.Entity.EntityState.Added;

            //存Q4
            foreach (var q4 in Sign_up_question.Q4)
            {
                Sign_up_questionnaire sq = new Sign_up_questionnaire();
                sq.Sign_up_no = Number;
                sq.Question_no = 4;
                sq.Question_num = Convert.ToInt32(q4.Substring(0, 1));
                dbContext.Entry(sq).State = System.Data.Entity.EntityState.Added;
            }
            Sign_up_questionnaire q4else = new Sign_up_questionnaire();
            q4else.Sign_up_no = Number;
            q4else.Question_no = 4;
            q4else.Other_result1 = Sign_up_question.Q4else;
            dbContext.Entry(q4else).State = System.Data.Entity.EntityState.Added;

            //Q5?????????????????????????????????????????????????

            //Q6
            Sign_up_questionnaire q6jobs = new Sign_up_questionnaire();
            q6jobs.Sign_up_no = Number;
            q6jobs.Question_no = 6;
            q6jobs.Other_result1 = Sign_up_question.Q6jobs;
            dbContext.Entry(q6jobs).State = System.Data.Entity.EntityState.Added;


            //Q7
            foreach (var q7 in Sign_up_question.Q7)
            {
                Sign_up_questionnaire sq = new Sign_up_questionnaire();
                sq.Sign_up_no = Number;
                sq.Question_no = 7;
                sq.Question_num = Convert.ToInt32(q7.Substring(0, 1));
                dbContext.Entry(sq).State = System.Data.Entity.EntityState.Added;
            }

            //Q8
            foreach (var q8 in Sign_up_question.Q4)
            {
                Sign_up_questionnaire sq = new Sign_up_questionnaire();
                sq.Sign_up_no = Number;
                sq.Question_no = 8;
                sq.Question_num = Convert.ToInt32(q8.Substring(0, 1));
                dbContext.Entry(sq).State = System.Data.Entity.EntityState.Added;
            }

            dbContext.SaveChanges();

            return RedirectToAction("NewVolunteer");
        }
        //申請完成
        public ActionResult Requirement_ok()
        {
            ViewBag.Partilview = "Requirement_ok";
            return PartialView();
        }



    }
}