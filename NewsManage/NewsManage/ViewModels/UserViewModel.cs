using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewsManage.ViewModels
{
    public class UserViewModel
    {
        [DisplayName("用户编号")]
        [Required]
        public int UserId { get; set; }

        [DisplayName("用户账号")]
        public string Account { get; set; }

        [DisplayName("用户姓名")]
        public string RealName { get; set; }


        [DisplayName("用户密码")]
        public string Password { get; set; }

        [DisplayName("用户角色")]
        public string Role { get; set; }

        [DisplayName("部门Id")]
        public string DepartId { get; set; }

        [DisplayName("部门名称")]
        public string DeptName { get; set; }

    }
}