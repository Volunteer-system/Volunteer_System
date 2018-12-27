using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Volunteer_Web.Models;
using Volunteer_Web.ViewModel;
using System.Text.RegularExpressions;


namespace Volunteer_Web.Controllers
{
    public class HomeController : Controller
    {
        VolunteerEntities dbContext = new VolunteerEntities();

        // GET: Home
        //首頁
        public ActionResult Index()
        {
            Home_maintenanceVM home_MaintenanceVM = new Home_maintenanceVM();
            home_MaintenanceVM.SelectHome_maintenance_inHome();
            return View(home_MaintenanceVM);
        }
        //志工隊介紹
        public ActionResult Introduction()
        {
            return View();
        }
        public ActionResult Experience()
        {            
            Supervision_Experience_VM supervision_Experience_VM = new Supervision_Experience_VM();
            supervision_Experience_VM.Service_groups = dbContext.Service_group.ToList();
            supervision_Experience_VM.Experiences = supervision_Experience_VM.SelectExperience_byGroup(0);
            return View(supervision_Experience_VM);
        }
        [HttpPost]
        public ActionResult Experience(int id=0)
        {
            Supervision_Experience_VM supervision_Experience_VM = new Supervision_Experience_VM();
            supervision_Experience_VM.Service_groups = dbContext.Service_group.ToList();
            supervision_Experience_VM.Experiences = supervision_Experience_VM.SelectExperience_byGroup(id);
            return View(supervision_Experience_VM);
        }

        public ActionResult Activity(int id=0)
        {
            Visitor_Activity_VM visitor_Activity_VM = new Visitor_Activity_VM();
            visitor_Activity_VM.Activity_types = dbContext.Activity_type.ToList();
            visitor_Activity_VM.Visitor_Activity_VMs = visitor_Activity_VM.SelectActivity_byActivity_type(id);
            return View(visitor_Activity_VM);
        }
        public ActionResult GetImageBytes(int id = 1)
        {
            Activity_photo ap = dbContext.Activity_photo.Find(id);
            byte[] img = ap.Activity_photo1;
            return File(img, "image/jpeg");
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
                    Session["Agree"] = true;
                    break;
                case 3:
                    ViewBag.Partilview = "Questionnaire";
                    break;
                case 4:
                    ViewBag.Partilview = "Service_period";
                    break;
                case 5:
                    ViewBag.Partilview = "Interview_period";
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
             return Redirect("~/Home/NewVolunteer/3");
        }
        //Demo
        public ActionResult Demo()
        {

            Session["Sign_up_session"] = new Sign_up_session {
                Chinese_name = "李小明",
                Sex = "Male",
                Birthday = DateTime.Now.AddYears(-25),
                Sign_up_type = "社會志工",
                Identity_card="A123456789",
                Phone = "02-12345678",
                Mobile = "0912345678",
                Email = "Ming120@gmail.com",
                Address = "台北市大安區復興南路一段390號",
                Education = "大學",
                Job = "服務業",
                Expertises = new string[] { "2飛天","3衝浪" }
            };
            Session["Question"] = new Sign_up_questionnaireVM {
                Q1 = new string[] { "02打發時間", "03體驗志願服務工作" },
                Q2 = new string[] { "05有認識的人在醫院", "01離家近" },
                Q3 = new string[] { "05親友介紹", "09醫生介紹" },
                Q3doc = "李大明醫師",
                Q4 = new string[] { "03員工眷屬", "06曾參與過本院相關活動" },
                Q5unit = "春暉工作社",
                Q5years = "2",
                Q5content = "陪伴唐氏症兒童",
                Q6jobs = "超商店員",
                Q7="01是",
                Q8=new string[] { "03捷運", "02機車" }

            };
            Session["Service_period"] = new Service_period_VM {
                wish1 = new int[] {1,5,8,9 },
                wish2 = new int[] {2 },
                wish3 = new int[] {7 }
        };
            Session["Interview_period"] = new Interview_period_VM {
                wish1= new int[] { 1, 5, 8, 9 }
            };





            return Redirect("/Home/NewVolunteer/2");
        }


        //問券調查
        [HttpGet]
        public ActionResult Questionnaire()
        {
            ViewBag.Partilview = "Questionnaire";

            Sign_up_questionnaireVM session;
            if (Session["Question"] == null)
            {
                session = new Sign_up_questionnaireVM();
            }
            else
            {
                session = Session["Question"] as Sign_up_questionnaireVM;
            }
            ViewData.Model = session;


            return PartialView();
        }
        [HttpPost]
        public ActionResult Questionnaire(Sign_up_questionnaireVM SQ)
        {
           
            ViewBag.Partilview = "Questionnaire";
            Session["Question"] = SQ;
            return Redirect("~/Home/NewVolunteer/4");
        }

        //服務時段調查
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
        public ActionResult Service_period(int[] wish_1st, int[] wish_2nd, int[] wish_3rd)
        {

            Service_period_VM service=new Service_period_VM();

            service.wish1 = null;
            service.wish1 = wish_1st;
            service.wish2 = wish_2nd;
            service.wish3 = wish_3rd;

            Session["Service_period"]= service ;


            return Content("新增服務時段成功", "text/plain");
        }

        //面試時段調查
        [HttpGet]
        public ActionResult Interview_period()
        {
            Interview_period_VM temp;
            if (Session["Interview_period"] == null)
            {
                temp = new Interview_period_VM();
            }
            else
            {
                temp = Session["Interview_period"] as Interview_period_VM;
            }
            TempData["data"] = temp;
            return PartialView();
        }
        [HttpPost]
        public ActionResult Interview_period(int[] wish_1st)
        {
            Interview_period_VM service = new Interview_period_VM();
            service.wish1 = wish_1st;
            Session["Interview_period"] = service;
            return Content("新增/修改成功", "text/plain");

        }

        //資料總表顯示
        public ActionResult Alldata_check()
        {
            
            Sign_up_session sign_Up_Session =Session["Sign_up_session"] as Sign_up_session ?? new Sign_up_session();
  
            Sign_up_questionnaireVM Sign_up_question = Session["Question"] as Sign_up_questionnaireVM ?? new Sign_up_questionnaireVM();



            Sign_up_Alldata_VM sign_Up_Alldata_VM = new Sign_up_Alldata_VM();
            sign_Up_Alldata_VM.SetSign_up_Alldata(sign_Up_Session, Sign_up_question);

            return PartialView(sign_Up_Alldata_VM);
        }

        //檢查時段
        public ActionResult Check_period(int [] wish) {

            string re = "";
            var service=Session["Service_perviod"] as Service_period_VM;
            var interview = Session["Interview_perviod"] as Interview_period_VM;
            if (service.wish1.Length == 0)
            {
                re += "服務時間";
                
            }
            if(interview.wish1.Length==0)
            {
                re += "," + "可面試時間";
            }
            return Content(re, "/UTF-8");
        }

        //存入個人資料
        public ActionResult InsertSign_up()
        {


          //session轉VM
          Sign_up_session sign_Up_Session = Session["Sign_up_session"] as Sign_up_session;
          Sign_up_questionnaireVM  Sign_up_question = Session["Question"] as Sign_up_questionnaireVM;

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
                sign.Sign_up_type = "社會青年";
            //日期預設
            sign.Sign_up_date = Convert.ToDateTime("1800-01-01 00:00:00");
            sign.Approval_date= Convert.ToDateTime("1800-01-01 00:00:00");
            dbContext.Entry(sign).State = System.Data.Entity.EntityState.Added;
            dbContext.SaveChanges();
            
            
            //找出此新志工的暫時ID
                int Number= dbContext.Sign_up.Where(p=>p.Chinese_name==sign.Chinese_name).First().Sign_up_no;
            
            
            //存專長資料
            foreach (var exp in sign_Up_Session.Expertises)
            {
                Sign_up_expertise se = new Sign_up_expertise();
                se.Sign_up_no = Number;
                se.Expertise = Convert.ToInt32(Regex.Replace(exp, "[^0-9]", ""));
                dbContext.Entry(se).State = System.Data.Entity.EntityState.Added;
            }


            //存表單資料 

            //存Q1
            foreach (var q1 in Sign_up_question.Q1)
            {
               // var q1num = ;

                Sign_up_questionnaire sq = new Sign_up_questionnaire();
                sq.Sign_up_no = Number;
                sq.Question_no = 1;
                sq.Answer_num = Convert.ToInt32(Regex.Replace(q1, "[^0-9]", ""));

                //選到其他，則加入其他的值
                if (Regex.Replace(q1, "[^0-9]", "") == "06")
                {
                    sq.Other_result1 = Sign_up_question.Q1else;
                }
                dbContext.Entry(sq).State = System.Data.Entity.EntityState.Added;

            }


            //存Q2
            foreach (var q2 in Sign_up_question.Q2)
            {
                var q2num = Regex.Replace(q2, "[^0-9]", "");

                Sign_up_questionnaire sq = new Sign_up_questionnaire();
                sq.Sign_up_no = Number;
                sq.Question_no = 2;
                sq.Answer_num = Convert.ToInt32(q2num);

                //選到其他，則加入其他的值
                if (q2num == "08")
                {
                    sq.Other_result1 = Sign_up_question.Q2else;
                }
                dbContext.Entry(sq).State = System.Data.Entity.EntityState.Added;


            }

            //存Q3
            foreach (var q3 in Sign_up_question.Q3)
            {
                var q3num = Regex.Replace(q3, "[^0-9]", "");

                Sign_up_questionnaire sq = new Sign_up_questionnaire();
                sq.Sign_up_no = Number;
                sq.Question_no = 3;
                sq.Answer_num = Convert.ToInt32(q3num);

                //選到其他，則加入其他的值
                if (q3num == "09")
                {
                    sq.Other_result1 = Sign_up_question.Q3doc;
                }
                dbContext.Entry(sq).State = System.Data.Entity.EntityState.Added;


            }

            //存Q4
            foreach (var q4 in Sign_up_question.Q4)
            {
                var q4num = Regex.Replace(q4, "[^0-9]", "");

                Sign_up_questionnaire sq = new Sign_up_questionnaire();
                sq.Sign_up_no = Number;
                sq.Question_no = 4;
                sq.Answer_num = Convert.ToInt32(q4num);

                //選到其他，則加入其他的值
                if (q4num == "08")
                {
                    sq.Other_result1 = Sign_up_question.Q4else;
                }
                dbContext.Entry(sq).State = System.Data.Entity.EntityState.Added;
            }

            //Q5
            Sign_up_questionnaire q5 = new Sign_up_questionnaire();
            q5.Sign_up_no = Number;
            q5.Question_no = 5;
            q5.Answer_num = 1;
            q5.Other_result1 = Sign_up_question.Q5unit;
            q5.Other_result2 = Sign_up_question.Q5years;
            q5.Other_result3 = Sign_up_question.Q5content;
            dbContext.Entry(q5).State = System.Data.Entity.EntityState.Added;

            //Q6
            Sign_up_questionnaire q6jobs = new Sign_up_questionnaire();
            q6jobs.Sign_up_no = Number;
            q6jobs.Question_no = 6;
            q6jobs.Other_result1 = Sign_up_question.Q6jobs;
            dbContext.Entry(q6jobs).State = System.Data.Entity.EntityState.Added;

            //Q7
            Sign_up_questionnaire q7 = new Sign_up_questionnaire();
            q7.Sign_up_no = Number;
            q7.Question_no = 7;
            q7.Other_result1 = Sign_up_question.Q7;
            dbContext.Entry(q7).State = System.Data.Entity.EntityState.Added;


            //Q8
            foreach (var q8 in Sign_up_question.Q8)
            {
                var q8num = Regex.Replace(q8, "[^0-9]", "");

                Sign_up_questionnaire sq = new Sign_up_questionnaire();
                sq.Sign_up_no = Number;
                sq.Question_no = 8;
                sq.Answer_num = Convert.ToInt32(q8num);

                dbContext.Entry(sq).State = System.Data.Entity.EntityState.Added;
            }

            //存服務時間
            var service_period_vm = Session["Service_period"] as Service_period_VM;
            int[] wish1 = service_period_vm.wish1;
            int[] wish2 = service_period_vm.wish2;
            int[] wish3 = service_period_vm.wish3;


            foreach (var i in wish1)
            {
                Sign_up_Service_period service_period = new Sign_up_Service_period();
                service_period.Sign_up_no = Number;
                service_period.Wish_order = 1;
                service_period.Service_period_no = i;
                dbContext.Entry(service_period).State = System.Data.Entity.EntityState.Added;
            }
            if (wish2 != null)
            {
                foreach (var i in wish2)
                {
                    Sign_up_Service_period service_period = new Sign_up_Service_period();
                    service_period.Sign_up_no = Number;
                    service_period.Wish_order = 2;
                    service_period.Service_period_no = i;
                    dbContext.Entry(service_period).State = System.Data.Entity.EntityState.Added;
                }
            }
            if (wish3 != null)
            {
                foreach (var i in wish3)
                {
                    Sign_up_Service_period service_period = new Sign_up_Service_period();
                    service_period.Sign_up_no = Number;
                    service_period.Wish_order = 3;
                    service_period.Service_period_no = i;
                    dbContext.Entry(service_period).State = System.Data.Entity.EntityState.Added;
                }
            }
            //存可面試時間
            var interview_period_vm = Session["Interview_period"] as Interview_period_VM;
            int[] interview_wish = interview_period_vm.wish1;

            foreach (var i in interview_wish)
            {
                Sign_up_interview_period interview_period = new Sign_up_interview_period();
                interview_period.Sign_up_no = Number;
                interview_period.interview_period_no = i;
                dbContext.Entry(interview_period).State = System.Data.Entity.EntityState.Added;
            }

            dbContext.SaveChanges();

            return Redirect("~/Home/NewVolunteer/7");
        }


        
        //申請完成
        public ActionResult Requirement_ok()
        {
            ViewBag.Partilview = "Requirement_ok";
            return PartialView();
        }



    }
}