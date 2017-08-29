using System.Data.Entity.ModelConfiguration;
using BackgroundEFManage.Model;

namespace BackgroundEFManage.DataConfig
{
    /// <summary>
    /// 模块属性配置
    /// </summary>
   public  class ModuleConfig:EntityTypeConfiguration<Module>
    {
        public ModuleConfig()
        {
            this.ToTable("Module");
            this.HasKey(module => module.Id);
            Property(module => module.MChName).HasMaxLength(50).IsRequired();
            Property(module => module.MEnName).HasMaxLength(50).IsRequired();
            Property(module => module.MSort).HasMaxLength(20).IsRequired();
            Property(module => module.Code).HasMaxLength(80).IsRequired();
            Property(module => module.Description).HasMaxLength(600);
            Property(module => module.NavigatePic).HasMaxLength(35);
            Property(module => module.URL).HasMaxLength(50);
            Property(module => module.Icon).HasMaxLength(35).IsRequired();
            Property(module => module.Founder).IsRequired();
            Property(module => module.FoundTime).IsRequired();
        }
    }
}
