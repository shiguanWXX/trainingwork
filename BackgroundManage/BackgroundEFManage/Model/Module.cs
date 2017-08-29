using System;
using System.Collections.Generic;

namespace BackgroundEFManage.Model
{
    /// <summary>
    /// 模块类
    /// </summary>
   public class Module
    {
        /// <summary>
        /// 模块Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 上级模块Id
        /// </summary>
        public int FId { get; set; }
        /// <summary>
        /// 中文名称
        /// </summary>
        public string MChName { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        public string MEnName { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public string MSort { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 导航图片
        /// </summary>
        public string NavigatePic { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Display { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int Founder { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime FoundTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public int ModifyPerson { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }
        /// <summary>
        /// 角色集合
        /// </summary>
        public virtual  ICollection<Role> Roles { get; set; }
    }
}
