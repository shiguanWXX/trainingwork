using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BackgroundEFManage.ViewModels
{
  public  class UserViewModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [DisplayName("用户Id")]
        public int Id { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        [DisplayName("用户账号")]
        [Required]
        [StringLength(8, ErrorMessage = "用户名的长度不能超过8")]
        public string Account { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        [DisplayName("用户密码")]
        public string Password { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        [DisplayName("真实姓名")]
        [StringLength(50,ErrorMessage = "名字的长度不能超过50")]
        public string RealName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [DisplayName("性别")]
        public string Sex { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [DisplayName("手机号码")]
        [StringLength(11,ErrorMessage = "手机号的长度不能超过11位")]
        [Required]
        public string TelPhone { get; set; }
        /// <summary>
        /// 办公电话
        /// </summary>
        [DisplayName("办公电话")]
        [StringLength(11, ErrorMessage = "办公电话的长度不能超过11位")]
        public string OfficePhone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [DisplayName("邮箱")]
        [StringLength(50, ErrorMessage = "邮箱的长度不能超过50位")]
        [Required]
        public string Email { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [DisplayName("是否启用")]
        public bool Enable { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [DisplayName("创建人")]
        public int Founder { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime FoundTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [DisplayName("修改人")]
        public int ModifyPerson { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [DisplayName("修改时间")]
        public DateTime ModifyTime { get; set; }
    }
}
