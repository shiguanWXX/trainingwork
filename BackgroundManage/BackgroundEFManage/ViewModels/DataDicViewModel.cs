using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BackgroundEFManage.ViewModels
{
   public  class DataDicViewModel
    {
        /// <summary>
        /// 字典Id
        /// </summary>
        [DisplayName("字典Id")]
        public int Id { get; set; }
        /// <summary>
        /// 中文名称
        /// </summary>
        [DisplayName("中文名称")]
        [StringLength(50,ErrorMessage = "中文名称不能超过50")]
        public string DChName { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        [DisplayName("英文名称")]
        [StringLength(50, ErrorMessage = "英文名称不能超过50")]
        public string DEnName { get; set; }
        /// <summary>
        /// 是否只读
        /// </summary>
        [DisplayName("是否只读")]
        public bool DReadonly { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [DisplayName("描述")]
        [StringLength(600, ErrorMessage = "描述不能超过600")]
        public string Description { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [DisplayName("创建人")]
        public int Founder { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime FoundTime { get; set; }
    }
}
