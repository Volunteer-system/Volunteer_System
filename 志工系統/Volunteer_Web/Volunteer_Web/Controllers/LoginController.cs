using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Volunteer_Web.Models;

namespace Volunteer_Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        [ChildActionOnly]
        public ActionResult LoginIndex()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult LoginIndex(account accounts)
        {
            #region 異常事件餅乾
            Response.Cookies["selectid"]["id"] = "0";
            Response.Cookies["selectstage"]["date1"] = new DateTime().ToString("yyyyMMdd");
            Response.Cookies["selectstage"]["date2"] = new DateTime().ToString("yyyyMMdd");
            Request.Cookies["selectid"].Expires = DateTime.Now.AddHours(1);
            Response.Cookies["selectid"].Expires = DateTime.Now.AddHours(1);
            Request.Cookies["selectstage"].Expires = DateTime.Now.AddHours(1);
            Response.Cookies["selectstage"].Expires = DateTime.Now.AddHours(1);
            #endregion

            #region 人力申請
            Response.Cookies["Manpowerstage"]["id"] = "0";
            Response.Cookies["Manpowerdate"]["year"] = "0";
            Request.Cookies["Manpowerstage"].Expires = DateTime.Now.AddHours(1);
            Response.Cookies["Manpowerstage"].Expires = DateTime.Now.AddHours(1);
            Request.Cookies["Manpowerdate"].Expires = DateTime.Now.AddHours(1);
            Response.Cookies["Manpowerdate"].Expires = DateTime.Now.AddHours(1);
            #endregion

            accountMethod account_db = new accountMethod();
            //接收回傳使用者: (ID△運用單位△名稱)
            string TypeAndName = account_db.GetUserName(accounts.Account_number, accounts.Password);
            if (TypeAndName != "")
            {   //以空白來分割字串
                var s = TypeAndName.Split(' ');
                Session["UserID"] = s[0];
                string UserType = s[1];
                string UserName = s[2];
                Session["UserType"] = UserType;
                Session["UserName"] = UserName;
                //return Content(UserType, "Text/plain");
                return Json(UserType, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("帳號或密碼錯誤，登入失敗!!",JsonRequestBehavior.AllowGet);
                //return Content("帳號或密碼錯誤，登入失敗!!", "Text/plain");
            }
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        [ChildActionOnly]
        public ActionResult ScriptView()
        {
            return PartialView();
        }
    }
}