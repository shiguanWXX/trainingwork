using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsManage.ViewModels
{
    public class NewsListViewModel
    {
        public List<NewsViewModel> News { get; set; }
        public string UserName { get; set; }
        public FooterViewModel FooterData { get; set; }

    }
}