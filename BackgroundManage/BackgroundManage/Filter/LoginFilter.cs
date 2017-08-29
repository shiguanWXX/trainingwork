using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackgroundManage.Filter
{
    public class LoginFilter:ActionFilterAttribute
    {
        /// <summary>
        /// 防止用户跳过登录直接进入页面
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!Convert.ToBoolean(filterContext.HttpContext.Session["UserId"])) {
                //filterContext.Result = new ContentResult()
                //{
                //    Content = "您还没登录，请登录！"
                //};
                filterContext.HttpContext.Response.Redirect("/Login/Index");
            }
        }
    }
}