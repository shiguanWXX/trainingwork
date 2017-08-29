using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;

using BackgroundEFManage.DataBLL;
using BackgroundEFManage.ViewModels;
using BackgroundEFManage.Model;
using BackgroundManage.Filter;
using BackgroundEFManage.DataDAL;

namespace BackgroundManage.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
        [LoginFilter]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 加载角色列表
        /// </summary>
        /// <param name="pageInfo">页面信息</param>
        /// <param name="roleName">角色名</param>
        /// <returns></returns>
        public ActionResult RoleList(PageInfo pageInfo, string roleName)
        {
            BackgroundBLL bll = new BackgroundBLL();
            BaseJsonData<RoleViewModel> jsonData=new BaseJsonData<RoleViewModel>();
            List<RoleViewModel> roleList= new List<RoleViewModel>();
            int total = 0;
            roleList = bll.GetRoleList(pageInfo, out total, roleName);
            jsonData.draw = pageInfo.Draw++;
            jsonData.iTotalDisplayRecords = total;
            jsonData.iTotalRecords = total;
            jsonData.aaData = roleList;
            return Json(jsonData);
        }
        /// <summary>
        /// 保存角色信息
        /// </summary>
        /// <param name="role">角色实体</param>
        /// <param name="checkAId">管理员角色Id集合</param>
        /// <param name="checkMId">模块角色Id集合</param>
        /// <returns></returns>
        public ActionResult SaveRole(RoleViewModel role,string checkAId,string checkMId)
        {
            BackgroundBLL bll=new BackgroundBLL();
            string[] strA;
            string[] strM;
            strA = checkAId.Split(',');
            int[] listIdA = Array.ConvertAll<string, int>(strA, s => int.Parse(s));
            strM = checkMId.Split(',');
            int[] listIdM = Array.ConvertAll<string, int>(strM, s => int.Parse(s));
            string message;
            if (role.Id == 0)
            {
                role.Founder = (int) Session["UserId"];
                role.ModifyPerson = (int) Session["UserId"];
                var count = 0;
                if (ModelState.IsValid)
                {
                    try
                    {
                        count = bll.AddRole(role, listIdA, listIdM);
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
            else
            {
                role.ModifyPerson = (int) Session["UserId"];
                var count = 0;
                if (ModelState.IsValid)
                {
                    try
                    {
                        count = bll.UpdateRole(role, listIdA, listIdM);
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
        /// 删除角色信息
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <returns></returns>
        public ActionResult DeleteRole(int id)
        {
            BackgroundBLL bll=new BackgroundBLL();
            var count = bll.DeleteRole(id);
            return Json(count > 0 ? "删除成功" : "删除失败");
        }
        /// <summary>
        /// 返回模态框
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <returns></returns>
        public ActionResult GetRolePartial(int id=0)
        {
            BackgroundBLL  bll=new BackgroundBLL();
            RoleViewModel role=new RoleViewModel();
            if (id == 0)
            {
                return PartialView("_RolePartial");
            }
            else
            {
                role = bll.GetSigleRole(id);
                return PartialView("_RolePartial", role);
            }
        }
        /// <summary>
        /// 检查角色名是否重复
        /// </summary>
        /// <param name="roleName">角色名</param>
        /// <param name="id">角色名Id</param>
        /// <returns></returns>
        public ActionResult CheckRoleName(string roleName,int id)
        {
            BackgroundBLL bll = new BackgroundBLL();
            var check = bll.CheckDChName(roleName,id);
            return Json(check);
        }
#region 管理员与模块权限树
        /// <summary>
        /// 管理员权限树
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <returns></returns>
        public JsonResult GetAdminTree(int id=0)
        {
            BackgroundDAL dal = new BackgroundDAL();
            if (id != 0)
            {
                var role = dal.Role.First(users => users.Id == id);
                RoleTree adminTreeN = new RoleTree() { id = 0, text = "全选", };
                var query = dal.Users.Select(user=> new
                {
                    id = user.Id,
                    text = user.RealName,
                }).ToList();
                List<Children> childrens = new List<Children>();
                foreach (var item in query)
                {
                    Children children = new Children();
                    State stateC = new State();
                    foreach (var items in role.Userses)
                    {
                        if (role.Userses.Count > 0)
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
                adminTreeN.children = childrens;
                return Json(adminTreeN, JsonRequestBehavior.AllowGet);
            }
            else
            {
                RoleTree adminTreeN = new RoleTree { id = 0, text = "全选" };
                var query = dal.Users.Select(user => new
                {
                    id = user.Id,
                    text = user.RealName,
                }).ToList();
                State state = new State();
                adminTreeN.state = state;
                List<Children> childrens = new List<Children>();
                foreach (var item in query)
                {
                    State stateC = new State();
                    Children children = new Children();
                    children.id = item.id;
                    children.text = item.text;
                    children.state = stateC;
                    childrens.Add(children);
                }
                adminTreeN.children = childrens;
                return Json(adminTreeN, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 返回权限树
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <returns></returns>
        public JsonResult GetModuleTrees(int id)
        {
            BackgroundDAL dal = new BackgroundDAL();
            if (id != 0)
            {
                var role = dal.Role.First(roles => roles.Id == id);
                List<int> idList =new List<int>();
                JsTree moduleTreeN = new JsTree() { id = 0, text = "全选", };
                var modules = dal.Module.Where(module => module.FId == 0).ToList();
                foreach (var item in role.Modules)
                {
                    if (role.Modules.Count > 0)
                    {
                        idList.Add(item.Id);
                    }
                }
                moduleTreeN.children = GetModuleTreelist(modules, idList);
                return Json(moduleTreeN, JsonRequestBehavior.AllowGet);
            }
            else
            {
                JsTree moduleTreeN = new JsTree() { id = 0, text = "全选", };
                var modules = dal.Module.Where(module => module.FId == 0).ToList();
                moduleTreeN.children = GetModuleTreelist(modules, null);
                return Json(moduleTreeN, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 获取权限树
        /// </summary>
        /// <param name="modules">模块集合</param>
        /// <param name="idList">选中模块Id集合</param>
        /// <returns></returns>
        public List<JsTree> GetModuleTreelist(List<Module> modules,List<int>idList)
        {
            BackgroundDAL dal = new BackgroundDAL();
           List<JsTree>  moduleTrees = new List<JsTree>();
            foreach (var item in modules)
            {
               State state=new State();
                if (idList != null)
                {
                    if (idList.Contains(item.Id))
                    {
                        state.selected = true;
                    }
                }
                JsTree roleTree = new JsTree();
                roleTree.id = item.Id;
                roleTree.text = item.MChName;
                roleTree.state = state;
                var childModule = dal.Module.Where(moduleC => moduleC.FId == item.Id).ToList();
                if (childModule.Count != 0)
                {
                    roleTree.children = GetModuleTreelist(childModule, idList);
                }
                moduleTrees.Add(roleTree);
            }
            return moduleTrees;
        }
        #endregion
    }
}