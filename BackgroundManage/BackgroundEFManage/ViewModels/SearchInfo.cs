using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundEFManage.ViewModels
{
    /// <summary>
    /// 条件查询信息
    /// </summary>
   public class SearchInfo
    {    
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

    }
}
