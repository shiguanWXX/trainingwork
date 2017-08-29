using System.Web.Mvc;
using System.Web.Security;

using BackgroundEFManage.DataBLL;
using BackgroundEFManage.Model;
using BackgroundEFManage.ViewModels;

namespace BackgroundManage.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
         BackgroundBLL bll=new BackgroundBLL();
         bll.InitializaUser();
            return View();
        }
        /// <summary>
        /// 登录检查
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <returns></returns>
        public ActionResult DoLogin(UserViewModel user)
        {
           BackgroundBLL bll=new BackgroundBLL();
            var userE = bll.CheckUsers(user);
            if (userE == null)
            {
             ModelState.AddModelError("CredentialError","用户名或密码错误");
                return View("Index");
            }
            Session["UserId"] = userE.Id;
            Session["Account"] = userE.Account;
            //return View("Test");
            return RedirectToAction("Index", "User");
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