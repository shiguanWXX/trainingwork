using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace NewsManage.ViewModels
{
    public class NewsViewModel
    {
        [DisplayName("新闻编号")]
        public int NewsId { get; set; }

        [DisplayName("新闻标题")]
        public string NewsName { get; set; }

        [DisplayName("新闻内容")]
        public string NewsContent { get; set; }

        [DisplayName("作者Id")]
        public int UserId { get; set; }
        public string Disabled { get; set; }
        [DisplayName("当前登录人")]
        public string CurrentUser { get; set; }
        [DisplayName("作者")]
        public string RealName { get; set; }
        public FooterViewModel FooterData { get; set; }

    }
}