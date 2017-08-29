using System;
using System.Web.Mvc;
using System.Collections.Generic;

using BackgroundEFManage.ViewModels;
using BackgroundEFManage.DataBLL;
using System.Linq;
using BackgroundEFManage.DataDAL;
using BackgroundManage.Filter;

namespace BackgroundManage.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [LoginFilter]
        public ActionResult Index()
        {
          
            UserViewModel model = new UserViewModel();
                return View(model);
        }

        #region 管理员基本信息
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="pageInfo">页面信息</param>
        /// <returns></returns>
        public ActionResult GetUserList(PageInfo pageInfo)
        {
            BackgroundBLL bll = new BackgroundBLL();
           
            BaseJsonData<UserViewModel> jsonData = new BaseJsonData<UserViewModel>();
            int total = 0;
            List<UserViewModel> userList = new List<UserViewModel>();
            userList = bll.GetUserList(pageInfo, out  total);
            jsonData.draw = pageInfo.Draw++;
            jsonData.iTotalDisplayRecords = total;
            jsonData.iTotalRecords = total;
            jsonData.aaData = userList;
            return Json(jsonData);
        }
        /// <summary>
        /// 获取单个用户信息
        /// </summary>
        /// <param name="id">用户Id</param>
        /// <returns></returns>
        public ActionResult GetSingleUser(string id)
        {
            BackgroundBLL bll = new BackgroundBLL();
            var user = bll.GetSingleUser(int.Parse(id));
            return Json(user);
        }
        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <param name="checkId">选中的角色Id</param>
        /// <returns></returns>
        public ActionResult SaveUser( UserViewModel user,string checkId)
        {
            BackgroundBLL bll = new BackgroundBLL();
            string[] str =checkId.Split(',');
            int[] listId = Array.ConvertAll<string, int>(str, s => int.Parse(s));
            string message;
            if (user.Id == 0)
            {
                user.Founder = (int) Session["UserId"];
                user.ModifyPerson = (int) Session["UserId"];
                user.Id = 0;
                int count = 0;
                if (ModelState.IsValid)
                {
                    try
                    {
                        count = bll.AddUser(user, listId);
                        message = count > 0 ? "保存成功" : "保存失败";

                    }
                    catch (Exception ex)
                    {
                        message = "保存出错，请重试！";
                    }
                    return  Json(message);
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    return Json(errors);
                }
            }
            else
            {
                user.ModifyPerson = (int)Session["UserId"];
                int count = 0;
                if (ModelState.IsValid)
                {
                    try
                    {
                        count = bll.UpdateUser(user, listId);
                        message = count > 0 ? "保存成功" : "保存失败";
                    }
                    catch (Exception ex)
                    {
                        message = "保存出错，请重试！";
                    }
                    return Json(message);
                }
                else
                {
                    var modelError = ModelState.AllModelStateErrors();
                    return Json(modelError);
                }
            }
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        public ActionResult DeteleUser(string id)
        {
            BackgroundBLL bll = new BackgroundBLL();
            var result = 0;
            string meaasge;
            try
            {
                result = bll.DeleteUser(int.Parse(id));
               meaasge= result > 0 ? "删除成功" : "删除失败";
            }
            catch (Exception ex)
            {
                meaasge = "保存出错，请重试！";
            }

            return Json(meaasge);
        }
        /// <summary>
        /// 验证用户账号是否存在
        /// </summary>
        /// <param name="account">用户账号</param>
        /// <param name="id">用户Id</param>
        /// <returns></returns>
        public ActionResult CheckAccount(string account,int id)
        {
            BackgroundBLL bll = new BackgroundBLL();
            var check = bll.CheckAccount(account,id);
            return Json(check);
        }
        #endregion
        /// <summary>
        /// 请求角色树
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <returns></returns>
        public JsonResult GetAdminTree(int id)
        {
            BackgroundDAL dal = new BackgroundDAL();
            if (id != 0)
            {
                var user = dal.Users.First(users => users.Id == id);
                RoleTree roleTreeN = new RoleTree() { id = 0, text = "全选",};
                var query = dal.Role.Select(role => new
                {
                    id = role.Id,
                    text = role.RName,
                }).ToList();
                List<Children> childrens = new List<Children>();
                foreach (var item in query)
                {
                    Children children = new Children();
                    State stateC = new State();
                    foreach (var items in user.Roles)
                    {
                        if (user.Roles.Count > 0)
                        {
                            if (item.id == items.Id)
                            {
                                stateC.selected = true;
                            }
                        }
                    }
                    children.state = stateC;
                    children.id = item.id;
                    children.text = item.text;
                    childrens.Add(children);
                }
                roleTreeN.children = childrens;
                return Json(roleTreeN, JsonRequestBehavior.AllowGet);
            }
            else
            {
                RoleTree roleTreeN = new RoleTree { id = 0, text = "全选" };
                var query = dal.Role.Select(role => new
                {
                    id = role.Id,
                    text = role.RName,
                }).ToList();
                State state=new State();
                roleTreeN.state= state;
                List<Children> childrens = new List<Children>();
                foreach (var item in query)
                {
                    State stateC = new State();
                    Children children = new Children();
                    children.id = item.id;
                    children.text = item.text;
                    children.state=stateC;
                    childrens.Add(children);
                }
                roleTreeN.children = childrens;
                return Json(roleTreeN, JsonRequestBehavior.AllowGet);
            }
        }
    }
}