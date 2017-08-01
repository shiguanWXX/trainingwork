using NewsManage.Data_Access_Layer;
using NewsManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NewsManage.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            NewsBLLUser newsBllUser = new NewsBLLUser();
            newsBllUser.NewUser();
            return View();
        }
       
        /// <summary>
        /// 登录检查
        /// </summary>
        /// <param name="user">填入的用户信息</param>
        /// <returns></returns>
        public ActionResult DoLogin(User user)
        {
            if (ModelState.IsValid)
            {

                NewsBLLUser newsBU = new NewsBLLUser();
                var  userE = newsBU.CheckUser(user);
                if (userE == null)
                {
                    ModelState.AddModelError("CredentialError", "用户名或密码错误");
                    return View("Index");
                }

                bool IsAdmin = false;
                if (userE.Status == UserStatus.AuthenticatedAdmin)
                {
                    IsAdmin = true;

                }
                else if (userE.Status == UserStatus.AuthentucatedUser)
                {
                    IsAdmin = false;
                }
                
                FormsAuthentication.SetAuthCookie(user.Account, false);
                Session["IsAdmin"] = IsAdmin;
                Session["UserId"] = userE.UserId;
                return RedirectToAction("Index", "News");
            }
            else
            {
                return View("Index");
            }
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");

        }
    }
}