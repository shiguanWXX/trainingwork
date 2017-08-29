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
    public class DataDicController : Controller
    {
        // GET: DataDic
        [LoginFilter]
        public ActionResult Index()
        {
                return View();
        }
        #region 字典信息操作
        /// <summary>
        /// 获取数据字典树信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDicTree(string id)
        {
            BackgroundDAL dal=new BackgroundDAL();
            var data = dal.DataDic.Select(dic => new
            {
                id = dic.Id,
                parent = "#",
                text = dic.DChName,
                children = false
            }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取字典信息列表
        /// </summary>
        /// <param name="pageInfo">页面信息</param>
        /// <param name="dicId">字典Id</param>
        /// <returns></returns>
        public ActionResult GetDicList(PageInfo pageInfo, string dicId)
        {
            BackgroundBLL bll=new BackgroundBLL();
            BaseJsonData<DataDicViewModel> jsonData=new BaseJsonData<DataDicViewModel>();
            int total=0;
            List<DataDicViewModel> dicList=new List<DataDicViewModel>();
            if (!string.IsNullOrEmpty(dicId))
            {
                dicList = bll.GetDicList(pageInfo, out total, int.Parse(dicId));
            }
            jsonData.draw = pageInfo.Draw;
            jsonData.iTotalDisplayRecords = total;
            jsonData.iTotalRecords = total;
            jsonData.aaData = dicList;
            return Json(jsonData);
        }
        /// <summary>
        /// 用于返回数据字典的部分视图
        /// </summary>
        /// <param name="id">字典id</param>
        /// <returns></returns>
        public ActionResult GetDicPartialModal(int id = 0)
        {
            DataDicViewModel dataDicViewModel = new DataDicViewModel();
            if (id != 0)
            {
                BackgroundBLL bll = new BackgroundBLL();
                dataDicViewModel = bll.GetSigleDic(id);
                return PartialView("_DicPartial", dataDicViewModel);
            }
            else
            {
                return PartialView("_DicPartial");
            }
        }
        /// <summary>
        /// 保存数据字典
        /// </summary>
        /// <param name="dataDic">字典实体</param>
        /// <returns></returns>
        public ActionResult SaveDataDic(DataDicViewModel dataDic)
        {
            BackgroundBLL bll=new BackgroundBLL();
            string message;
            if (dataDic.Id == 0)
            {
                dataDic.Founder = (int) Session["UserId"];
                int count = 0;
                if (ModelState.IsValid)
                {
                    try
                {
                    count = bll.AddDataDic(dataDic);
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
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    return Json(errors);
                }
            }
            else
            {
                dataDic.Founder = (int)Session["UserId"];
                dataDic.FoundTime = DateTime.Now;
                int count = 0;
                if (ModelState.IsValid)
                {
                    try
                    {
                        count = bll.UpdateDataDic(dataDic);
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
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    return Json(errors);
                }
            }
        }
        /// <summary>
        /// 检查字典中文名称是否重复
        /// </summary>
        /// <param name="dChName">字典中文名称</param>
        /// <param name="id">字典的Id</param>
        /// <returns></returns>
        public ActionResult CheckDChName(string dChName,int id=0)
        {
            BackgroundBLL bll = new BackgroundBLL();
            var check = bll.CheckDChName(dChName, id);
            return Json(check);
        }
        /// <summary>
        /// 检查字典中文名称是否重复
        /// </summary>
        /// <param name="dEnName">字典的英文名称</param>
        /// <param name="id">字典的Id</param>
        /// <returns></returns>
        public ActionResult CheckDEnName(string dEnName,int id=0)
        {
            BackgroundBLL bll = new BackgroundBLL();
            var check = bll.CheckDEnName(dEnName,id);
            return Json(check);
        }
        #endregion

        #region 字典详细信息操作
        /// <summary>
        ///获取数据字典详细信息列表
        /// </summary>
        /// <param name="pageInfo">页面信息</param>
        /// <param name="dicId">字典Id</param>
        /// <returns></returns>
        public ActionResult GetDicDetailList(PageInfo pageInfo, string dicId)
        {
            BackgroundBLL bll = new BackgroundBLL();
            BaseJsonData<DataDicDetailViewModel> jsonData = new BaseJsonData<DataDicDetailViewModel>();
            int total=0;
            List<DataDicDetailViewModel> dicDetailList = new List<DataDicDetailViewModel>();
            if (!string.IsNullOrEmpty(dicId))
            {
                dicDetailList = bll.GetDicDetailList(pageInfo, out total, int.Parse(dicId));
            }
            jsonData.draw = pageInfo.Draw++;
            jsonData.iTotalDisplayRecords = total;
            jsonData.iTotalRecords = total;
            jsonData.aaData = dicDetailList;
            return Json(jsonData);
        }
        /// <summary>
        /// 用于返回模态框的的部分视图
        /// </summary>
        /// <param name="id">详细字典id</param>
        /// <returns></returns>
        public ActionResult GetPartialModal(int id = 0)
        {
            DataDicDetailViewModel dataDicDetailViewModel = new DataDicDetailViewModel();
            if (id != 0)
            {
                BackgroundBLL bll = new BackgroundBLL();
                dataDicDetailViewModel = bll.GetSigleDicDetail(id);
                return PartialView("_DetailPartial", dataDicDetailViewModel);
            }
            else
            {
                return PartialView("_DetailPartial");
            }
        }
        /// <summary>
        /// 新增字典详细信息
        /// </summary>
        /// <param name="dataDetail">字典详细信息实体</param>
        /// <returns></returns>
        public ActionResult SaveDataDetail(DataDicDetailViewModel dataDetail)
        {
            BackgroundBLL bll = new BackgroundBLL();
            string message;
            if (dataDetail.Id == 0)
            {
                dataDetail.Founder = (int) Session["UserId"];
                dataDetail.ModifyPerson = (int) Session["UserId"];
                int count = 0;
                if (ModelState.IsValid)
                {
                    try
                    {
                        count = bll.AddDicDetail(dataDetail);
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
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    return Json(errors);
                }
            }
            else
            {
                dataDetail.ModifyPerson = (int)Session["UserId"];
                int count = 0;
                if (ModelState.IsValid)
                {
                    try
                    {
                        count = bll.UpdateDicdetail(dataDetail);
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
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    return Json(errors);
                }
            }
        }
        /// <summary>
        /// 检查字典详细中文名是否重复
        /// </summary>
        /// <param name="deChName">字典详细中文名</param>
        /// <param name="id">字典详细Id</param>
        /// <returns></returns>
        public ActionResult CheckDeChName(string deChName,int id=0)
        {
            BackgroundBLL bll = new BackgroundBLL();
            var check = bll.CheckDeChName(deChName,id);
            return Json(check);
        }
        /// <summary>
        /// 检查字典详细的英文名称是否重复
        /// </summary>
        /// <param name="deEnName">字典详细英文名称</param>
        /// <param name="id">字典详细Id</param>
        /// <returns></returns>
        public ActionResult CheckDeEnName(string deEnName,int id=0)
        {
            BackgroundBLL bll = new BackgroundBLL();
            var check = bll.CheckDeEnName(deEnName,id);
            return Json(check);
        }
        #endregion
    }
}