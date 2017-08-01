using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewsManage.Models
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class User
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Key]
        [Required]
        public int UserId { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        [Required]
        [StringLength(8, ErrorMessage = "用户名长度不能大于8")]
        public string Account { get; set; }
        /// <summary>
        /// 用户真实姓名
        /// </summary>
        [StringLength(50, ErrorMessage = "真实名字长度不能大于50")]
        public string RealName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        [Required]
        [StringLength(60, MinimumLength = 6, ErrorMessage = "密码长度不能小于6")]
        public string Password { get; set; }
        /// <summary>
        /// 用户角色
        /// </summary>
        [StringLength(4)]
        public string Role { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UserStatus Status
        {
            get {

                if (Role == "1")
                {
                    return UserStatus.AuthenticatedAdmin;
                }
                else {
                    return UserStatus.AuthentucatedUser;
                }
            }
        }
        /// <summary>
        /// 部门编号
        /// </summary>
        public string DepartId { get; set; }
        /// <summary>
        /// 部门类
        /// </summary>
        public Depart Depart { get; set; }
        /// <summary>
        /// 新闻集合
        /// </summary>
        public virtual ICollection<News> Newses { get; set; }
    }
}