using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using NewsManage.Models;

namespace NewsManage.NewsConfig
{
    public class UserConfig:EntityTypeConfiguration<User>
    {
        /// <summary>
        /// 用户表配置
        /// </summary>
        public UserConfig()
        {
            this.ToTable("User");
            this.HasKey(user => user.UserId).HasMany(user=>user.Newses).WithRequired(news=>news.User).HasForeignKey(news=>news.UserId).WillCascadeOnDelete();
            Property(user => user.UserId).IsRequired();
            Property(user => user.Account).HasMaxLength(8);
            Property(user => user.Password).IsRequired().HasMaxLength(60);
            Property(user => user.RealName).HasMaxLength(50);
            Property(user => user.Role).HasMaxLength(4);
        }
    }
}