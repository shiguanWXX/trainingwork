using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BackgroundEFManage.ViewModels
{
   public  class DataDicDetailViewModel
    {
        /// <summary>
        /// 字典详细Id
        /// </summary>
        [DisplayName("字典详细Id")]
        public int Id { get; set; }
        /// <summary>
        /// 中文名称
        /// </summary>
        [DisplayName("中文名称")]
        [StringLength(50, ErrorMessage = "中文名称的长度不能超过50")]
        [Required]
        public string DeChName { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        [DisplayName("英文名称")]
        [StringLength(50, ErrorMessage = "英文名称的长度不能超过50")]
        [Required]
        public string DeEnName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [DisplayName("描述")]
        [StringLength(600, ErrorMessage = "描述的长度不能超过600")]
        public string Description { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        [StringLength(20, ErrorMessage = "排序的长度不能超过20")]
        [Required]
        public string Sort { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [DisplayName("是否启用")]
        public bool Enable { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>创建人
        [DisplayName("")]
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
        /// 修改人真实姓名
        /// </summary>
        [DisplayName("真实姓名")]
        public string RealName { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [DisplayName("修改时间")]
        public string ModifyTime { get; set; }
        [DisplayName("字典Id")]
        public int DId { get; set; }

    }
}
