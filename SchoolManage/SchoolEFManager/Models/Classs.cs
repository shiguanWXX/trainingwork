using System.Collections.Generic;

namespace SchoolEFManager.Models
{
    /// <summary>
    /// 课程表
    /// </summary>
    public class Classs
    {
        /// <summary>
        /// 班级编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 班级名
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 教师集合
        /// </summary>
        public virtual ICollection<Teacher> Teachers { get; set; }
        /// <summary>
        /// 学生集合
        /// </summary>
        public virtual ICollection<Student> Students { get; set; }
    }
}