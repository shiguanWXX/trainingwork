using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using BackgroundEFManage.DataBLL;
using BackgroundEFManage.DataDAL;
using BackgroundEFManage.ViewModels;
using BackgroundManage.Filter;

namespace BackgroundManage.Controllers
{
    public class ModuleController : Controller
    {
        // GET: Module
        [LoginFilter]
        public ActionResult Index()
        {
            return View();
        }

        #region 模块基本信息
        /// <summary>
        /// 获取模块树
        /// </summary>
        /// <param name="id">上级模块Id</param>
        /// <returns></returns>
        public ActionResult GetModuleTree(int id = 0)
        {
            BackgroundDAL dal=new BackgroundDAL();
            var data = dal.Module.Where(module => module.FId == id).OrderBy(module=>module.MSort).Select(module => new
                {
                    id = module.Id,
                    parent = module.FId.ToString() == "0" ? "#" : module.FId.ToString(),
                    text = module.MChName,
                    //true|false 是否有子项
                    children = dal.Module.Any(modules => modules.FId == module.Id)
                });
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取模块信息
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="mId"></param>
        /// <returns></returns>
        public ActionResult ModuleList(PageInfo pageInfo,string  mId)
        {
           BackgroundBLL bll = new BackgroundBLL();
            BaseJsonData<ModuleViewModel> jsonData = new BaseJsonData<ModuleViewModel>();
            int total=0;
            List<ModuleViewModel> moduleList = new List<ModuleViewModel>();
            if (!string.IsNullOrEmpty(mId))
            {
                moduleList = bll.GetModuleList(pageInfo, out total, int.Parse(mId));
            }
            jsonData.draw = pageInfo.Draw++;
            jsonData.iTotalDisplayRecords = total;
            jsonData.iTotalRecords = total;
            jsonData.aaData = moduleList;
            return Json(jsonData);
        }
        /// <summary>
        /// 返回模态框
        /// </summary>
        /// <param name="id">模块Id</param>
        /// <returns></returns>
        public ActionResult GetModulePartial(int id=0)
        {
            BackgroundBLL bll=new BackgroundBLL();
            ModuleViewModel module=new ModuleViewModel();
            if (id != 0)
            {
                module = bll.GetSingleModule(id);
                return PartialView("_ModulePartial", module);
            }
            else
            {
                return PartialView("_ModulePartial");
            }
        }
        /// <summary>
        /// 保存模块信息
        /// </summary>
        /// <param name="module">模块实体</param>
        /// <param name="checkId">角色选择Id</param>
        /// <returns></returns>
        public ActionResult SaveModule(ModuleViewModel module, string checkId)
        {
            BackgroundBLL bll=new BackgroundBLL();
            string[] str = checkId.Split(',');
            int[] listId = Array.ConvertAll<string, int>(str, s => int.Parse(s));
            string message;
            if (module.Id == 0)
            {
                module.Founder = (int) Session["UserId"];
                module.ModifyPerson = (int) Session["UserId"];
                module.Code = "a";
                var count = 0;
                if (ModelState.IsValid)
                {
                    try
                    {
                        count = bll.AddModule(module, listId);
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
                module.ModifyPerson = (int) Session["UserId"];
                module.Code = "a";
                var count = 0;
                if (ModelState.IsValid)
                {
                    try
                    {
                        count = bll.UpdateModule(module, listId);
                        message = count > 0 ? "保存成功" : "保存失败";
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
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
        /// 检查模块的中文名称是否重复
        /// </summary>
        /// <param name="mChName">模块中文名称</param>
        /// <param name="id">模块的Id</param>
        /// <returns></returns>
        public ActionResult CheckMChName(string mChName,int id)
        {
            BackgroundBLL bll=new BackgroundBLL();
            var check = bll.CheckMChName(mChName,id);
            return Json(check);
        }
        /// <summary>
        /// 检查模块的英文名称是否重复
        /// </summary>
        /// <param name="mEnName">模块的英文名称</param>
        /// <param name="id">模块的Id</param>
        /// <returns></returns>
        public ActionResult CheckMEnName(string mEnName,int id)
        {
            BackgroundBLL bll=new BackgroundBLL();
            var check = bll.CheckMEnName(mEnName,id);
            return Json(check);
        }
        #endregion
        #region 模块角色信息操作
        /// <summary>
        /// 获取模块角色树
        /// </summary>
        /// <param name="id">模块Id</param>
        /// <returns></returns>
        public JsonResult GetRModuleTree(int id = 0)
        {
            BackgroundDAL dal = new BackgroundDAL();
            if (id != 0)
            {
                var module = dal.Module.First(modules => modules.Id == id);
                RoleTree roleTreeN = new RoleTree() { id = 0, text = "全选" };
                var query = dal.Role.Select(role => new
                {
                    id = role.Id,
                    parent = "0",
                    text = role.RName,
                }).ToList();
                State state = new State();
                roleTreeN.state = state;
                if (module.Roles.Count == query.Count)
                {
                    state.selected = true;
                }
                List<Children> childrens = new List<Children>();
                foreach (var item in query)
                {
                    Children children = new Children();
                    State stateC = new State();
                    children.state = stateC;
                    foreach (var items in module.Roles)
                    {
                        if (module.Roles.Count > 0)
                        {
                            if (item.id == items.Id)
                            {
                                //children.state.@checked = true;
                                stateC.selected = true;
                            }
                        }
                    }
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
                    parent = "0",
                    text = role.RName,
                }).ToList();
                State state = new State();
                roleTreeN.state = state;
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
                roleTreeN.children = childrens;
                return Json(roleTreeN, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

    }
}