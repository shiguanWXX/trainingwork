using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BackgroundEFManage.ViewModels
{
  public  class RoleViewModel
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [DisplayName("角色Id")]
        public int Id { get; set; }
        /// <summary>
        /// 角色名
        /// </summary>
        [DisplayName("角色名")]
        [Required]
        [StringLength(50,ErrorMessage = "名字的长度不能超过50")]
        public string RName { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        [DisplayName("编码")]
        [StringLength(80,ErrorMessage = "编码的长度不能超过80")]
        public string Code { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [DisplayName("描述")]
        [StringLength(600,ErrorMessage = "描述的长度不能超过600")]
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
