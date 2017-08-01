using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsManage.ViewModels
{
    public class TreeViewModel
    {
        /// <summary>
        /// 节点Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 父节点Id
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 节点名字
        /// </summary>
        public string NodeName { get; set; }
    }
}