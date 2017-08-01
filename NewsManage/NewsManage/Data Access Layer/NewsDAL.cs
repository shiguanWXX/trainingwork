using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using NewsManage.Models;
namespace NewsManage.Data_Access_Layer
{
    public class NewsDAL:DbContext
    {
        public NewsDAL() : base("NewsConnection")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<News>().ToTable("NewsDetial");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Depart>().ToTable("Depart");
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<News> News { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Depart> Depart { get; set; }

    }
}