using System.Data.Entity;
using BackgroundEFManage.DataConfig;

using BackgroundEFManage.Model;

namespace BackgroundEFManage.DataDAL
{
   public  class BackgroundDAL:DbContext
    {
        public BackgroundDAL() : base("BackgroundConnection")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BackgroundDAL>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UsersConfig());
            modelBuilder.Configurations.Add(new RoleConfig());
            modelBuilder.Configurations.Add(new ModuleConfig());
            modelBuilder.Configurations.Add(new DataDicConfig());
            modelBuilder.Configurations.Add(new DataDicDetailConfig());
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Module> Module { get; set; }
        public DbSet<DataDic> DataDic { get; set; }
        public DbSet<DataDicDetail> DataDicDetial { get; set; }

    }
}
