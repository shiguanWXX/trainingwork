using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NewsManage.Models
{
    /// <summary>
    /// 新闻表
    /// </summary>
    public class News
    {
        /// <summary>
        /// 新闻编号
        /// </summary>
        [Key]
        [Required]
        public int NewsId { get; set; }
        /// <summary>
        /// 新闻标题
        /// </summary>
        [Required]
        [StringLength(35, ErrorMessage = "名字长度不能大于35")]
        public string NewsName { get; set; }
        /// <summary>
        /// 新闻内容
        /// </summary>
        [Column(TypeName = "text")]
        public string NewsContent { get; set; }
        /// <summary>
        /// 作者（用户编号）
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 用户类
        /// </summary>
        public User User { get; set; }
    }
}