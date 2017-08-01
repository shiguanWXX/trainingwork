using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsManage.Data_Access_Layer;
using NewsManage.ViewModels;
using NewsManage.Models;

namespace NewsManage.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取部门树的信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetDeptTree(int id=0)
        {
            //NewsDepartBLL newsDepartBll = new NewsDepartBLL();
            //var data = newsDepartBll.GetDeptTree();
            NewsDAL newsDal = new NewsDAL();
            var data = newsDal.Depart.Where(dept => dept.FdepartId == id.ToString()).Select(dept => new
            //var data = newsDal.Depart.Select(dept => new
            {
                id=dept.Id,
                parent=dept.FdepartId=="0"?"#":dept.FdepartId,
                text=dept.DeptName,
                //true|false 是否有子项
                children = newsDal.Depart.Any(depts=>depts.FdepartId==dept.Id)
            });
            return Json(data,JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="pageInfo">页面信息</param>
        /// <param name="deptid">部门编号</param>
        /// <returns></returns>
        public ActionResult GetUerList(PageInfo pageInfo,string deptid)
        {
            NewsBLLUser newsBllUser = new NewsBLLUser();
            BaseJsonData<UserViewModel> jsonData=new BaseJsonData<UserViewModel>();
            int total;
            List<UserViewModel> userList = new List<UserViewModel>();
            userList = newsBllUser.GetUserList(pageInfo, out total, deptid);
            jsonData.draw = pageInfo.Draw++;
            jsonData.iTotalDisplayRecords = total;
            jsonData.iTotalRecords = total;
            jsonData.aaData = userList;
            return Json(jsonData);
        }
        /// <summary>
        /// 获取部门下拉框的信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDeptInfo()
        {
            return Json("");
        }

        /// <summary>
        /// 保存用户信息 
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <returns></returns>
        public ActionResult AddUser(User user)
        {
            NewsBLLUser newsBllUser = new NewsBLLUser();
            int count=0;
            
                try
                {
                    count = newsBllUser.AddUser(user);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return Json(count > 0 ? "保存成功" : "保存失败");
        }
        /// <summary>
        /// 获取用户的单条数据
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public JsonResult GetSingleUser(string userId)
        {
            NewsBLLUser newsBllUser=new NewsBLLUser();
            var data=newsBllUser.GetSingleUser(int.Parse(userId));
            return Json(data);
        }
        /// <summary>
        /// 更新单条数据
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <returns></returns>
        public ActionResult UpdateUser(User user)
        {

            NewsBLLUser newsBllUser=new NewsBLLUser();
            int count = 0;
            if (ModelState.IsValid)
            {
                try
                {
                    count = newsBllUser.UpdateUser(user);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return Json(count > 0 ? "保存成功" : "保存失败");
            }
            return Json("请检查信息是否有误");
        }
        /// <summary>
        /// 检查用户名是否重复
        /// </summary>
        /// <param name="account">用户名</param>
        /// <returns></returns>
        public ActionResult CheckAccount(string account)
        {
            NewsBLLUser newsBllUser = new NewsBLLUser();
            var check = newsBllUser.CheckAccount(account);
            return Json(check);
        }
    }
}