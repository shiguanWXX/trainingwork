using System.Collections.Generic;

namespace SchoolEFManager.Models
{
    /// <summary>
    /// 学生表
    /// </summary>
    public class Student
    {   
        /// <summary>
        /// 学号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string StuName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string StuPassword { get; set; }
        /// <summary>
        /// 系别
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 班级
        /// </summary>
        public string ClasssId { get; set; }
        /// <summary>
        /// 班级类
        /// </summary>
        public Classs Classs { get; set; }
        /// <summary>
        /// 成绩集合
        /// </summary>
        public virtual ICollection<Grade> Grades { get; set; }
    }
}