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
            return Redirect("~/Home/NewVolunteer/4");
        }
        //服務時段調查
        [HttpGet]
        public ActionResult Service_period()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Service_period(int[] wish_1st, int[] wish_2nd, int[] wish_3rd)
        {
            Session["Service_period_1st"] = wish_1st;
            Session["Service_period_2nd"] = wish_2nd;
            Session["Service_period_3rd"] = wish_3rd;

            return Content("新增服務時段成功", "text/plain");
        }
        //面試時間
        [HttpGet]
        public ActionResult Interview_period()
        {

            return PartialView();
        }
        [HttpPost]
        public ActionResult Interview_period(int[] wish_1st)
        {
            Session["Interview_period_1st"] = wish_1st;
            return Content("新增/修改成功", "text/plain");

        }
        //檢查資料
        public ActionResult Alldata_check()
        {
            
            Sign_up_session sign_Up_Session =Session["Sign_up_session"] as Sign_up_session ?? new Sign_up_session();
  
            Sign_up_questionnaireVM Sign_up_question = Session["Question"] as Sign_up_questionnaireVM ?? new Sign_up_questionnaireVM();



            Sign_up_Alldata_VM sign_Up_Alldata_VM = new Sign_up_Alldata_VM();
            sign_Up_Alldata_VM.SetSign_up_Alldata(sign_Up_Session, Sign_up_question);

            return PartialView(sign_Up_Alldata_VM);
        }


        //寫入個人資料
        public ActionResult InsertSign_up()
        {


          //session轉VM
          Sign_up_session sign_Up_Session = Session["Sign_up_session"] as Sign_up_session;
          Sign_up_questionnaireVM  Sign_up_question = Session["Question"] as Sign_up_questionnaireVM;

          //轉dbset並存檔
              //存個人資料   日期沒輸入的預設為1800/1/1
                Sign_up s = new Sign_up();
                s.Chinese_name = sign_Up_Session.Chinese_name;
                s.Sex = sign_Up_Session.Sex;
                s.Birthday = sign_Up_Session.Birthday;
                s.Phone = sign_Up_Session.Phone;
                s.Mobile = sign_Up_Session.Mobile;
                s.Email = sign_Up_Session.Email;
                s.Address = sign_Up_Session.Address;
                s.Education = sign_Up_Session.Education;
                s.Job = sign_Up_Session.Job;
                s.Stage = 1;
                s.Sign_up_type = "社會青年";
            //日期預設
            s.Sign_up_date = Convert.ToDateTime("1800-01-01 00:00:00");
            s.Approval_date= Convert.ToDateTime("1800-01-01 00:00:00");
            dbContext.Entry(s).State = System.Data.Entity.EntityState.Added;
            dbContext.SaveChanges();
            
            
            //找出此新志工的暫時ID
                int Number= dbContext.Sign_up.Where(p=>p.Chinese_name==s.Chinese_name).First().Sign_up_no;
            
            
            //存專長資料
            foreach (var exp in sign_Up_Session.Expertises)
            {
                Sign_up_expertise se = new Sign_up_expertise();
                se.Sign_up_no = Number;
                se.Expertise = Convert.ToInt32(exp.Substring(0, 1));
                dbContext.Entry(se).State = System.Data.Entity.EntityState.Added;
            }


            //存表單資料 
           
            //存Q1
            foreach (var q1 in Sign_up_question.Q1)
            {
                Sign_up_questionnaire sq = new Sign_up_questionnaire();
                sq.Sign_up_no = Number;
                sq.Question_no = 1;
               
                sq.Answer_num =Convert.ToInt32(q1.Substring(0,1));
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
                sq.Answer_num = Convert.ToInt32(q2.Substring(0, 1));
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
                sq.Answer_num = Convert.ToInt32(q3.Substring(0, 1));
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
                sq.Answer_num = Convert.ToInt32(q4.Substring(0, 1));
                dbContext.Entry(sq).State = System.Data.Entity.EntityState.Added;
            }
            Sign_up_questionnaire q4else = new Sign_up_questionnaire();
            q4else.Sign_up_no = Number;
            q4else.Question_no = 4;
            q4else.Other_result1 = Sign_up_question.Q4else;
            dbContext.Entry(q4else).State = System.Data.Entity.EntityState.Added;

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
            foreach (var q7 in Sign_up_question.Q7)
            {
                Sign_up_questionnaire sq = new Sign_up_questionnaire();
                sq.Sign_up_no = Number;
                sq.Question_no = 7;
                sq.Answer_num = Convert.ToInt32(q7.Substring(0, 1));
                dbContext.Entry(sq).State = System.Data.Entity.EntityState.Added;
            }

            //Q8
            foreach (var q8 in Sign_up_question.Q4)
            {
                Sign_up_questionnaire sq = new Sign_up_questionnaire();
                sq.Sign_up_no = Number;
                sq.Question_no = 8;
                sq.Answer_num = Convert.ToInt32(q8.Substring(0, 1));
                dbContext.Entry(sq).State = System.Data.Entity.EntityState.Added;
            }

            //存服務時間

            int[] wish1 = Session["Service_period_1st"] as int[];
            int[] wish2 = Session["Service_period_2nd"] as int[];
            int[] wish3 = Session["Service_period_3rd"] as int[];

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

            int[] interview_wish = Session["Interview_period_1st"] as int[];

            foreach (var i in interview_wish)
            {
                Sign_up_interview_period interview_period = new Sign_up_interview_period();
                interview_period.Sign_up_no = Number;
                interview_period.interview_period_no = i;
                dbContext.Entry(interview_period).State = System.Data.Entity.EntityState.Added;
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