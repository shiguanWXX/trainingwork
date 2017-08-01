
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolEFManager.Models
{
    /// <summary>
    /// 成绩表
    /// </summary>
    public class Grade
    {
        public int Id { get; set; }
        /// <summary>
        /// 成绩
        /// </summary>
        public float Grades { get; set; }
        /// <summary>
        /// 课程编号
        /// </summary>
        public string CourseId { get; set; }
        /// <summary>
        /// 课程类
        /// </summary>
        public Course Course { get; set; }
        /// <summary>
        /// 学号
        /// </summary>
        public int StudentId { get; set; }
        /// <summary>
        /// 学生类
        /// </summary>
        public Student Student { get; set; }

        /// </summary>
        /// 平均成绩
        /// </summary>
        [NotMapped]
        public float Avg { get; set; }
        /// <summary>
        /// 最高成绩
        /// </summary>
        public float MaxGrade { get; set; }

    }
}