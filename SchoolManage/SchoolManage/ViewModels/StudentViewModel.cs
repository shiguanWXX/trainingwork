using System.ComponentModel.DataAnnotations;

namespace SchoolEFManager.ViewModels
{
    public class StudentViewModel
    {
        /// <summary>
        /// 学号
        /// </summary>
        [Display(Name = "学号")]
        public int Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "姓名")]
        public string StuName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Display(Name = "性别")]
        public string Sex { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        [Display(Name = "年龄")]
        public int Age { get; set; }
        /// <summary>
        /// 系别
        /// </summary>
        [Display(Name = "系别")]
        public string Department { get; set; }
        /// <summary>
        /// 班级
        /// </summary>
        [Display(Name = "班级")]
        public string ClasssId { get; set; }

    }
}