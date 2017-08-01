using System.ComponentModel.DataAnnotations;

namespace SchoolEFManager.ViewModels
{
    public class MaxAvgViewModel
    {
        /// <summary>
        /// 课程编号
        /// </summary>
        [Display(Name = "课程编号")]
        public string CourseId { get; set; }
        /// <summary>
        /// 平均成绩
        /// </summary>
        [Display(Name = "平均成绩")]
        public float Avg { get; set; }
        /// <summary>
        /// 最高成绩
        /// </summary>
        [Display(Name = "最高成绩")]
        public float MaxGrade { get; set; }
    }
}