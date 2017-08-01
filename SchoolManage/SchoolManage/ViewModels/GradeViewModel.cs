using System.ComponentModel.DataAnnotations;

namespace SchoolManage
{
    
    public  class test { }
}

namespace SchoolEFManager.ViewModels
{
    public class GradeViewModel
    {
        /// <summary>
        /// 成绩
        /// </summary>
        [Display(Name = "成绩")]
        public float Grades { get; set; }
        /// <summary>
        /// 课程编号
        /// </summary>
        [Display(Name = "课程编号")]
        public string CourseId { get; set; }
        /// <summary>
        /// 学号
        /// </summary>
        [Display(Name = "学号")]
        public int StudentId { get; set; }
    }
}