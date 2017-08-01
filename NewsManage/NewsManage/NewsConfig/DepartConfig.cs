using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using NewsManage.Models;

namespace NewsManage.NewsConfig
{
    public class DepartConfig:EntityTypeConfiguration<Depart>
    {
        /// <summary>
        /// 部门表配置
        /// </summary>
        public DepartConfig()
        {
            this.ToTable("Depart");
            this.HasKey(dept => dept.Id).HasMany(dept=>dept.Users).WithRequired(user=>user.Depart).HasForeignKey(user=>user.DepartId).WillCascadeOnDelete();
            Property(dept => dept.Id).IsRequired().HasMaxLength(15);
            Property(dept => dept.DeptName).HasMaxLength(20);
            Property(dept => dept.FdepartId).HasMaxLength(15);
            Property(dept => dept.DeptAddress).HasMaxLength(100);
            Property(dept => dept.DeptType).HasMaxLength(20);

        }
    }
}