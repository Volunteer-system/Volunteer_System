using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Volunteer_Web.Models;
using Volunteer_Web.ViewModel;

namespace Volunteer_Web.Controllers
{
    public class StudentController : Controller
    {
        VolunteerEntities dbContext = new VolunteerEntities();

        // GET: Student
        public ActionResult Index()
        {
            return View();
        }



        //新志工申請
        public ActionResult NewVolunteer(int id = 1)
        {
            switch (id)
            {
                case 1:
                    ViewBag.partilview = "Kind_of";
                    break;
                case 2:
                    ViewBag.Partilview = "basic_info";
                    Session["Agree"] = true;
                    break;
                case 3:
                    ViewBag.Partilview = "Questionnaire";
                    break;
                case 4:
                    ViewBag.Partilview = "Service_period";
                    break;
                case 5:
                    ViewBag.Partilview = "Alldata_check";
                    break;
                case 6:
                    ViewBag.Partilview = "Requirement_ok";
                    break;
            }

            return View();
        }

        //學生志工申請須知
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

            //有填寫過則使用已填寫資料
            Sign_up_session session;
            if (Session["Sign_up_session"] == null)
            {
                session = new Sign_up_session();
            }
            else
            {
                session = Session["Sign_up_session"] as Sign_up_session;
            }
            ViewData.Model = session;
            return PartialView();
        }
        [HttpPost]
        public ActionResult basic_info(Sign_up_session sign_Up_Session)
        {
            ViewBag.Partilview = "basic_info";
            ViewBag.Expertise = dbContext.Expertise1.ToList();
            Session["Sign_up_session"] = sign_Up_Session;
            return Redirect("~/Student/NewVolunteer/3");
        }

        //問券調查
        [HttpGet]
        public ActionResult Questionnaire()
        {
            ViewBag.Partilview = "Questionnaire";

            Student_questionnaireVM session;
            if (Session["Question"] == null)
            {
                session = new Student_questionnaireVM();
            }
            else
            {
                session = Session["Question"] as Student_questionnaireVM;
            }
            ViewData.Model = session;


            return PartialView();
        }
        [HttpPost]
        public ActionResult Questionnaire(Student_questionnaireVM SQ)
        {

            ViewBag.Partilview = "Questionnaire";
            Session["Question"] = SQ;
            return Redirect("~/Student/NewVolunteer/4");
        }


        //學生服務時段調查
        [HttpGet]
        public ActionResult Service_period()
        {

            Service_period_VM temp;
            if (Session["Service_period"] == null)
            {
                temp = new Service_period_VM();
            }
            else
            {
                temp = Session["Service_period"] as Service_period_VM;
            }
            TempData["data"] = temp;
            return PartialView();
        }
        [HttpPost]
        public ActionResult Service_period(int[] wish_1st)
        {
            Service_period_VM service = new Service_period_VM();
            service.wish1 = wish_1st;
            Session["Service_period"] = service;
            return Content("新增/修改成功", "text/plain");

        }

        //資料總表顯示
        public ActionResult Alldata_check()
        {

            Sign_up_session sign_Up_Session = Session["Sign_up_session"] as Sign_up_session ?? new Sign_up_session();

            Student_questionnaireVM student_question = Session["Question"] as Student_questionnaireVM ?? new Student_questionnaireVM();



            Sign_up_Alldata_VM sign_Up_Alldata_VM = new Sign_up_Alldata_VM();
            sign_Up_Alldata_VM.SetSign_up_Alldata(sign_Up_Session, student_question);

            return PartialView(sign_Up_Alldata_VM);
        }


        //檢查身分證
        public ActionResult identity_check(string identity)
        {
            var q = from peoele in dbContext.Sign_up
                    where (peoele.Identity_card == identity)
                    select peoele;
            var q2 = from people in dbContext.Volunteers
                     where (people.IDcrad_no == identity)
                     select people;

            if (q.Count() > 0)
            {
                return Content("這個身分證已經辦過申請囉", "text/plain");
            }
            if (q2.Count() > 0)
            {
                return Content("您已經是志工了", "text/plain");
            }
            else {
                return Content("", "text/plain");
            }
                   
        }


        //檢查時段
            public ActionResult Check_period(int[] wish)
        {

            string re = "";
            var service = new Service_period_VM();
            if (Session["Service_period"] != null)
            {
                service = Session["Service_period"] as Service_period_VM;
            }

            if (service.wish1.Length == 0)
            {
                re += "服務時間";

            }
            return Content(re, "/UTF-8");
        }

        //存入個人資料
        public ActionResult InsertSign_up()
        {


            //session轉VM
            Sign_up_session sign_Up_Session = Session["Sign_up_session"] as Sign_up_session;
            Student_questionnaireVM Student_question = Session["Question"] as Student_questionnaireVM;

            //轉dbset並存檔
            //存個人資料   日期沒輸入的預設為1800/1/1
            Sign_up sign = new Sign_up();
            sign.Chinese_name = sign_Up_Session.Chinese_name;
            sign.Identity_card = sign_Up_Session.Identity_card;
            sign.Sex = sign_Up_Session.Sex;
            sign.Birthday = sign_Up_Session.Birthday;
            sign.Phone = sign_Up_Session.Phone;
            sign.Mobile = sign_Up_Session.Mobile;
            sign.Email = sign_Up_Session.Email;
            sign.Address = sign_Up_Session.Address;
            sign.Education = sign_Up_Session.Education;
            sign.Job = sign_Up_Session.Job;
            sign.Stage = 1;
            sign.Sign_up_type = sign_Up_Session.Sign_up_type;
            //日期預設
            sign.Sign_up_date = Convert.ToDateTime("1800-01-01 00:00:00");
            sign.Approval_date = Convert.ToDateTime("1800-01-01 00:00:00");
            dbContext.Entry(sign).State = System.Data.Entity.EntityState.Added;
            dbContext.SaveChanges();


            //找出此新志工的暫時ID
            int Number = dbContext.Sign_up.Where(p => p.Identity_card == sign.Identity_card).First().Sign_up_no;


            //存專長資料
            foreach (var exp in sign_Up_Session.Expertises)
            {
                Sign_up_expertise se = new Sign_up_expertise();
                se.Sign_up_no = Number;
                se.Expertise = Convert.ToInt32(Regex.Replace(exp, "[^0-9]", ""));
                dbContext.Entry(se).State = System.Data.Entity.EntityState.Added;
            }


            //存表單資料 
            //存Q9  是否參加訓練
            {
                Sign_up_questionnaire q9 = new Sign_up_questionnaire();
                q9.Sign_up_no = Number;
                q9.Question_no = 9;
                q9.Other_result1 = Student_question.training.ToString();
                dbContext.Entry(q9).State = System.Data.Entity.EntityState.Added;
            }


            //存Q10 家長是否知道
            {
                Sign_up_questionnaire q10 = new Sign_up_questionnaire();
                q10.Sign_up_no = Number;
                q10.Question_no = 10;
                q10.Other_result1 = Student_question.knowing.ToString();
                dbContext.Entry(q10).State = System.Data.Entity.EntityState.Added;
            }

            //存Q11 緊急連絡人名
            {
                Sign_up_questionnaire q11 = new Sign_up_questionnaire();
                q11.Sign_up_no = Number;
                q11.Question_no = 11;
                q11.Other_result1 = Student_question.Emergency_Name.ToString();
                dbContext.Entry(q11).State = System.Data.Entity.EntityState.Added;
            }

            //存Q12 緊急連絡人電話
            {
                Sign_up_questionnaire q12 = new Sign_up_questionnaire();
                q12.Sign_up_no = Number;
                q12.Question_no = 12;
                q12.Other_result1 = Student_question.Emergency_Phone.ToString();
                dbContext.Entry(q12).State = System.Data.Entity.EntityState.Added;
            }

            //存Q13 學校名稱
            {
                Sign_up_questionnaire q13 = new Sign_up_questionnaire();
                q13.Sign_up_no = Number;
                q13.Question_no = 13;
                q13.Other_result1 = Student_question.School_Name.ToString();
                dbContext.Entry(q13).State = System.Data.Entity.EntityState.Added;
            }
            //存Q14 學校規定
            {
                Sign_up_questionnaire q14 = new Sign_up_questionnaire();
                q14.Sign_up_no = Number;
                q14.Question_no = 14;
                q14.Other_result1 = Student_question.School_Regulation.ToString();
                dbContext.Entry(q14).State = System.Data.Entity.EntityState.Added;
            }



            //存學生服務時間
            var service_period_vm = Session["Service_period"] as Service_period_VM;
            int[] wish1 = service_period_vm.wish1;
            foreach (var i in wish1)
            {
                Sign_up_Service_period service_period = new Sign_up_Service_period();
                service_period.Sign_up_no = Number;
                service_period.Wish_order = 1;
                service_period.Service_period_no = i;
                dbContext.Entry(service_period).State = System.Data.Entity.EntityState.Added;
            }


            dbContext.SaveChanges();

            return Redirect("~/Student/NewVolunteer/6");
        }



        //申請完成
        public ActionResult Requirement_ok()
        {
            ViewBag.Partilview = "Requirement_ok";
            return PartialView();
        }


    }
}