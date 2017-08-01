using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Net.Mime;
using System.Web;
using NewsManage.Models;

namespace NewsManage.NewsConfig
{
    /// <summary>
    /// 新闻表配置
    /// </summary>
    public class NewsConfig:EntityTypeConfiguration<News>
    {
        public NewsConfig()
        {
            this.ToTable("News");
            this.HasKey(news => news.NewsId);
            Property(news => news.NewsId).IsRequired();
            Property(news => news.NewsContent).IsRequired().HasColumnType("text");
            Property(news => news.NewsName).HasMaxLength(35);
        }
    }
}