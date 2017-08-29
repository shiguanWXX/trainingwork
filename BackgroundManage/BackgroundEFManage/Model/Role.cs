using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackgroundEFManage.Model
{
    /// <summary>
    /// 角色类
    /// </summary>
    public class Role
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 角色名
        /// </summary>
        public string RName { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int  Founder { get; set; }
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
        /// 用户集合
        /// </summary>
        public virtual ICollection<Users> Userses { get; set; }
        /// <summary>
        /// 模块集合
        /// </summary>
        public virtual  ICollection<Module> Modules { get; set; }

    }
}
