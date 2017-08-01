using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsManage.ViewModels
{
    public class PageInfo
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int? PageIndex { get; set; }
        /// <summary>
        /// 页面显示条数
        /// </summary>
        public int? PageSize { get; set; }
        /// <summary>
        /// 用于刷新页面
        /// </summary>
        public int Draw { get; set; }
        /// <summary>
        /// 查询条件
        /// </summary>
        public string Search { get; set; }
        /// <summary>
        /// 排序的列
        /// </summary>
        public string SortCol { get; set; }
        /// <summary>
        /// 排序的方式
        /// </summary>
        public string SortDir { get; set; }
    }
}