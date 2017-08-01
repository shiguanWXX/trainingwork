using NewsManage.Models;
using NewsManage.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.Provider;

namespace NewsManage.Data_Access_Layer
{
    public class NewsBLL
    {
        /// <summary>
        /// 从数据库中获取News的详细内容(查询)
        /// </summary>
        /// <returns></returns>
        public List<News> GetNews()
        {
            NewsDAL newsDal = new NewsDAL();
            return newsDal.News.ToList();
        }
        public List<NewsViewModel> GetNewss()
        {
            List<NewsViewModel> newsViewModels = new List<NewsViewModel>();
            NewsDAL newsDal =new NewsDAL();
            var query =( from news in newsDal.News
                        join user in newsDal.User
                        on news.UserId equals user.UserId
                        select new
                        {
                            NewsId = news.NewsId,
                            NewsName = news.NewsName,
                            NewsContent = news.NewsContent,
                            RealName = user.RealName
                        }).ToList();
            foreach (var item in query)
            {
                NewsViewModel newsViewModel = new NewsViewModel();
                newsViewModel.NewsId = item.NewsId;
                newsViewModel.NewsName = item.NewsName;
                newsViewModel.NewsContent = item.NewsContent;
                newsViewModel.RealName = item.RealName;
                newsViewModels.Add(newsViewModel);
            }
            return newsViewModels;
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public News GeSingNews(int id)
        {
            NewsDAL newsDal = new NewsDAL();
            var newss = newsDal.News.First(u => u.NewsId ==id);
            return newss;

        }
        
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="NewTitle">搜索词</param>
        /// <returns></returns>
        public List<NewsViewModel> ConditionQuery(string NewTitle)
        {
            List<NewsViewModel> newsViewModels = new List<NewsViewModel>();
            NewsDAL newsDal = new NewsDAL();
            var query =( from news in newsDal.News
                        join user in newsDal.User
                        on news.UserId equals user.UserId
                        where news.NewsName.Contains(NewTitle)
                select new {
                    NewsId = news.NewsId,
                    NewsName = news.NewsName,
                    NewsContent = news.NewsContent,
                    RealName = user.RealName
                }).ToList();
            foreach (var item in query)
            {
                NewsViewModel newsViewModel = new NewsViewModel();
                newsViewModel.NewsId = item.NewsId;
                newsViewModel.NewsName = item.NewsName;
                newsViewModel.NewsContent = item.NewsContent;
                newsViewModel.RealName = item.RealName;
                newsViewModels.Add(newsViewModel);
            }
            return newsViewModels;
        }

        /// <summary>
        /// 新增新闻（缺少对数据插入成功与否的判断）
        /// </summary>
        /// <param name="news"></param>
        public int  AddNews(News news)
        {
            NewsDAL newsDal = new NewsDAL();
            newsDal.News.Add(news);
            return newsDal.SaveChanges();
        }

        /// <summary>
        /// 更新新闻内容
        /// </summary>
        /// <param name="news">新闻实体</param>
        /// <returns></returns>
        public int  UpdateNews(News news)
        {

            NewsDAL newsDal = new NewsDAL();
            var newss=newsDal.News.Single(u=>u.NewsId==news.NewsId);
            newss.NewsContent = news.NewsContent;
            newss.NewsName = news.NewsName;
            newss.UserId = news.UserId;
            return  newsDal.SaveChanges();
        }

        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="id">新闻id</param>
        /// <returns></returns>
        public int  DeleteNews(int id)
        {
            NewsDAL newsDal = new NewsDAL();
            var news= newsDal.News.First(u=>u.NewsId==id);
            newsDal.News.Remove(news);
           return newsDal.SaveChanges();

        }

        /// <summary>
        /// 从数据库中获取User的详细内容（查询）
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers()
        {
            NewsDAL newsDal = new NewsDAL();
            return newsDal.User.ToList();
        }

        //DataTable
        /// <summary>
        /// 获取新闻列表（DataTable）
        /// </summary>
        /// <param name="pageInfo">页面信息</param>
        /// <param name="total">新闻条数</param>
        /// <returns></returns>
        public List<NewsViewModel> GetList(PageInfo pageInfo,out int total)
        {
            List<NewsViewModel> newsViewModels = new List<NewsViewModel>();
            NewsDAL newsDal = new NewsDAL();
                var query = (from news in newsDal.News
                    join user in newsDal.User
                    on news.UserId equals user.UserId
                    select new
                    {
                        NewsId = news.NewsId,
                        NewsName = news.NewsName,
                        NewsContent = news.NewsContent,
                        RealName = user.RealName
                    });

            if (pageInfo.SortDir == "asc")
            {
                query= query.OrderBy(news => news.NewsId);
            }
            else
            {
                query=query.OrderByDescending(news => news.NewsId);
            }
            foreach (var item in query)
            {
                NewsViewModel newsViewModel = new NewsViewModel();
                newsViewModel.NewsId = item.NewsId;
                newsViewModel.NewsName = item.NewsName;
                newsViewModel.NewsContent = item.NewsContent;
                newsViewModel.RealName = item.RealName;
                newsViewModels.Add(newsViewModel);
            }
           
            total = newsViewModels.Count();
            return newsViewModels.Skip(pageInfo.PageIndex.Value).Take(pageInfo.PageSize.Value).ToList();
        }
        /// <summary>
        /// 获取带条件的新闻里表（DataTable）
        /// </summary>
        /// <param name="pageInfo">页面信息</param>
        /// <param name="total">新闻条数</param>
        /// <returns></returns>
        public List<NewsViewModel> GetListSer(PageInfo pageInfo, out int total)
        {
            List<NewsViewModel> newsViewModels = new List<NewsViewModel>();
            NewsDAL newsDal = new NewsDAL();
            var query = (from news in newsDal.News
                join user in newsDal.User
                on news.UserId equals user.UserId
                where news.NewsName.Contains(pageInfo.Search)
                select new
                {
                    NewsId = news.NewsId,
                    NewsName = news.NewsName,
                    NewsContent = news.NewsContent,
                    RealName = user.RealName
                }).ToList();
            foreach (var item in query)
            {
                NewsViewModel newsViewModel = new NewsViewModel();
                newsViewModel.NewsId = item.NewsId;
                newsViewModel.NewsName = item.NewsName;
                newsViewModel.NewsContent = item.NewsContent;
                newsViewModel.RealName = item.RealName;
                newsViewModels.Add(newsViewModel);
            }
            total = newsViewModels.Count();
            return newsViewModels.Skip(pageInfo.PageIndex.Value).Take(pageInfo.PageSize.Value).ToList();
        }

        /// <summary>
        /// 检查新闻标题是否存在
        /// </summary>
        /// <param name="newsName">新闻标题</param>
        /// <returns></returns>
        public bool CheckNewsName(string newsName)
        {
            NewsDAL newsDal = new NewsDAL();
            var count = newsDal.News.Count(news => news.NewsName == newsName);
            bool check = false;
            if (count > 0)
            {
                check = false;
            }
            else
            {
                check = true ;
            }
            return check;
        }
    }
}