using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackgroundEFManage.Model
{
    /// <summary>
    /// 用户类
    /// </summary>
   public class Users
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        ///[RegularExpression(^((13[0 - 9])| (14[5 | 7]) | (15([0 - 3] |[5 - 9])) | (18[0, 5 - 9]))\\d{8}$)
        public string TelPhone { get; set; }
        /// <summary>
        /// 办公电话
        /// </summary>
        public string OfficePhone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; }
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
        public virtual ICollection<Role> Roles { get; set; }

    }
}
