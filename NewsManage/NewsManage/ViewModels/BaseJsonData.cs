using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsManage.ViewModels
{
    public class BaseJsonData<T>
    {
        /// <summary>
        /// 请求次数
        /// </summary>
        public int draw { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public int iTotalRecords { get; set; }

        /// <summary>
        /// 结果总数
        /// </summary>
        public int iTotalDisplayRecords { get; set; }

        /// <summary>
        /// 分页查询后的数据结果
        /// </summary>
        public IEnumerable<T> aaData { get; set; }

    }
}