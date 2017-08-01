using NewsManage.Data_Access_Layer;
using NewsManage.Models;
using NewsManage.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsManage.Filter;

namespace NewsManage.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        //[Authorize]
        /// <summary>
        /// 新闻列表页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //NewsBLL newsBll = new NewsBLL();
            //BaseJsonData<NewsViewModel> jsonData = new BaseJsonData<NewsViewModel>();
            //int total = 0;
            //List<NewsViewModel> newsList = new List<NewsViewModel>();
            //PageInfo pageInfo = new PageInfo();
            //pageInfo.PageSize = 10;
            //newsList = newsBll.GetList(pageInfo, out total);
            //jsonData.draw = pageInfo.Draw++;
            //jsonData.iTotalDisplayRecords = total;
            //jsonData.iTotalRecords = total;
            //jsonData.aaData = newsList;
            //return Json(jsonData, JsonRequestBehavior.AllowGet);
            return View();
        }

        /// <summary>
        /// 检验当前登录人是否有增加新闻的权利
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAddNewLink()
        {
            if (Convert.ToBoolean(Session["IsAdmin"]))
            {
                return PartialView("AddNewLink");
            }
            else
            {
                return new EmptyResult();
            }

        }

        /// <summary>
        /// 新增新闻
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        [AdminFilter]
        public ActionResult AddNews()
        {
            
            return View();
        }
        /// <summary>
        /// 新增新闻保存
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddNews(News news)
        {
            if (ModelState.IsValid)
            {

                //还需获取当前登录人员的UserId
                news.UserId = (int)Session["UserId"];
                NewsBLL newsBll = new NewsBLL();
                var count = 0;
                try
                {
                    count = newsBll.AddNews(news);

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                if (count > 0)
                {
                    return RedirectToAction("Index");

                }
                else
                {
                    return View();

                }
            }
            else
            {
                return View();

            }

        }
        /// <summary>
        /// 更新新闻
        /// </summary>
        /// <param name="id">新闻id</param>
        /// <returns></returns>
        //[Authorize]
        public ActionResult UpdateNews(int  id)
        {

            NewsBLL newsBll = new NewsBLL();
            var news= newsBll.GeSingNews(id);
            return View(news);
        }
        /// <summary>
        /// 更新新闻保存
        /// </summary>
        /// <param name="news">新闻实体</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateNews(News news)//测试失败的情况
        {
            if (ModelState.IsValid)
            {
                NewsBLL newsBll = new NewsBLL();
                var count = 0;
                try
                {
                   count = newsBll.UpdateNews(news);

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                if (count > 0)
                {
                    return RedirectToAction("Index");

                }
                else
                {
                    return View();

                }
            }
            else
            {
                
                //ModelState.AddModelError("CredentialError", "Invalid Username or Password");
                return View();
            }

        }
        /// <summary>
        /// 删除单条新闻
        /// </summary>
        /// <param name="id">新闻id</param>
        /// <returns></returns>
        public JsonResult DeteleNews(int id)
        {
            NewsBLL newsBll = new NewsBLL();
            var result = 0;
            try
            {
                result = newsBll.DeleteNews(id);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result>0 ?Json("删除成功"):Json("删除失败");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Test()
        {
            return View();
        }

        public ActionResult GetTable()
        {
            return View();
        }
        /// <summary>
        /// 获取新闻列表数据
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetTable(PageInfo pageInfo)
        {
            NewsBLL newsBll = new NewsBLL();
            BaseJsonData<NewsViewModel> jsonData = new BaseJsonData<NewsViewModel>();
            int total = 0;
            List<NewsViewModel> newsList = new List<NewsViewModel>();
            if (pageInfo.Search == ""|| pageInfo.Search==null)
            {
                newsList = newsBll.GetList(pageInfo, out total);
            }
            else
            {
                newsList = newsBll.GetListSer(pageInfo, out total);
            }
            jsonData.draw = pageInfo.Draw++;
            jsonData.iTotalDisplayRecords = total;
            jsonData.iTotalRecords = total;
            jsonData.aaData = newsList;
            return Json(jsonData);
        }

        /// <summary>
        /// 检查新闻标题是否重复
        /// </summary>
        /// <param name="newsName">标题</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CheckNewsName(string newsName)
        {
            NewsBLL newsBll = new NewsBLL();
            var check = newsBll.CheckNewsName(newsName);
            return Json(check);
        }


    }
}