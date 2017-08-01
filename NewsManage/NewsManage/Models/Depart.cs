using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewsManage.Models
{
    /// <summary>
    /// 部门表
    /// </summary>
    public class Depart
    {
        /// <summary>
        /// 部门编号
        /// </summary>
        [Key]
        [Required]
        [StringLength(15, ErrorMessage = "名字长度不能大于15")]
        public string Id { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        [Required]
        [StringLength(20, ErrorMessage = "名字长度不能大于20")]
        public string DeptName { get; set; }
        /// <summary>
        /// 上级部门编号
        /// </summary>
        [StringLength(15, ErrorMessage = "名字长度不能大于15")]
        public string FdepartId { get; set; }
        /// <summary>
        /// 部门类型
        /// </summary>
        [StringLength(20, ErrorMessage = "名字长度不能大于20")]
        public string DeptType { get; set; }
        /// <summary>
        /// 部门地址
        /// </summary>
        [StringLength(100, ErrorMessage = "名字长度不能大于100")]
        public string DeptAddress { get; set; }
        /// <summary>
        /// 部门经理
        /// </summary>
        public int  DeptManager { get; set; }
        /// <summary>
        /// 用户集合
        /// </summary>
        public virtual ICollection<User> Users { get; set; }

    }
}