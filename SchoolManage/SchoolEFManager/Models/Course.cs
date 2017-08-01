using System.Collections.Generic;

namespace SchoolEFManager.Models
{
    /// <summary>
    /// 课程表
    /// </summary>
    public class Course
    {
        /// <summary>
        /// 课程编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 课程名
        /// </summary>
        public string CourseName { get; set; }
        /// <summary>
        /// 成绩集合
        /// </summary>
        public virtual ICollection<Grade> Grades { get; set; }

    }
}