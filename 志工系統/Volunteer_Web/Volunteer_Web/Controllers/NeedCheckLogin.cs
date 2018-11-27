using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Volunteer_Web.Controllers
{
    public class NeedCheckLogin:Controller       
    {
        public bool IsCheck = false;  //判斷是否執行檢查
        //當前的控制器內所有的方法執行前，都先執行此
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (IsCheck)
            {             //判斷Session["UserName"]是否有值
                if (filterContext.HttpContext.Session["UserName"] == null)
                {        //沒有值，轉到首頁
                    filterContext.HttpContext.Response.Redirect("~/Home/Index");
                }
            }
        }
        /*使用方法
         * 1.Controller控制器不繼承Controller
             ex: public class LoginController : Controller ->改成 public class LoginController : NeedCheckLogin
           2.建立Controller的建構子
              public LoginController()
              {   
                  IsCheck = true; //設定是否執行檢查
              }
         */
    }
}