using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundEFManage.ViewModels
{
   public  class PageInfo
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
        /// <summary>
        /// 用户名
        /// </summary>
        public string Acount { get; set; }
        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string Rname { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Mail { get; set; }
        /// <summary>
        /// 条件查询信息
        /// </summary>
        public List<SearchInfo> SearchInfos { get; set; }
    }
}
