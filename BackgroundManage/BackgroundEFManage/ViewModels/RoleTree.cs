using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundEFManage.ViewModels
{
   public class RoleTree
    {
        /// <summary>
        /// 节点id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 节点状态
        /// </summary>
        public State state { get; set; }
        /// <summary>
        /// 子节点
        /// </summary>
        public List<Children> children{ get; set; }
    }
}
