using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolEFManager.Models
{
    public class Teacher
    {
        /// <summary>
        /// 教师表
        /// </summary>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// 教师姓名
        /// </summary>
        public string TecName { get; set; }
        /// <summary>
        /// 教师性别
        /// </summary>
        public string TecSex { get; set; }
        /// <summary>
        /// 教师密码
        /// </summary>
        public string TecPassword { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// 系别
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 班级集合
        /// </summary>
        public virtual ICollection<Classs> Classses { get; set; }

    }
}