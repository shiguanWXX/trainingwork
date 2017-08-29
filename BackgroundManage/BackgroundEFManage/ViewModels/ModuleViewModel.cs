using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BackgroundEFManage.ViewModels
{
 public  class ModuleViewModel
    {
        /// <summary>
        /// 模块Id
        /// </summary>
        [DisplayName("模块Id")]
        public int Id { get; set; }
        /// <summary>
        /// 上级模块Id
        /// </summary>
        [DisplayName("上级模块Id")]
        public int FId { get; set; }
        /// <summary>
        /// 中文名称
        /// </summary>
        [DisplayName("中文名称")]
        [StringLength(50,ErrorMessage = "中文名称长度不能超过50")]
        [Required]
        public string MChName { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        [DisplayName("英文名称")]
        [StringLength(50, ErrorMessage = "中文名称长度不能超过50")]
        [Required]
        public string MEnName { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        [DisplayName("链接地址")]
        [StringLength(50, ErrorMessage = "中文名称长度不能超过50")]
        public string URL { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [DisplayName("描述")]
        [StringLength(600, ErrorMessage = "中文名称长度不能超过600")]
        public string Description { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        [StringLength(20, ErrorMessage = "排序长度不能超过20")]
        [Required]
        public string MSort { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        [DisplayName("图标")]
        [StringLength(35, ErrorMessage = "图标长度不能超过35")]
        [Required]
        public string Icon { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        [DisplayName("编码")]
        public string Code { get; set; }
        /// <summary>
        /// 导航图片
        /// </summary>
        [DisplayName("导航图片")]
        public string NavigatePic { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [DisplayName("是否启用")]
        public bool Enable { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        [DisplayName("是否显示")]
        public bool Display { get; set; }
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
        /// <summary>
        /// 修改人
        /// </summary>
        [DisplayName("修改人")]
        public int ModifyPerson { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [DisplayName("修改时间")]
        public DateTime ModifyTime { get; set; }
    }
}
