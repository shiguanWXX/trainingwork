using System.Data.Entity.ModelConfiguration;
using BackgroundEFManage.Model;

namespace BackgroundEFManage.DataConfig
{
    /// <summary>
    /// 用户属性配置
    /// </summary>
   public  class UsersConfig:EntityTypeConfiguration<Users>
    {
        public UsersConfig()
        {
            this.ToTable("Users");
            this.HasKey(user => user.Id);
            Property(user => user.Account).IsRequired().HasMaxLength(8);
            Property(user => user.Password).IsRequired().HasMaxLength(60);
            Property(user => user.RealName).HasMaxLength(50);
            Property(user => user.Sex).HasMaxLength(2);
            Property(user => user.TelPhone).HasMaxLength(11).IsRequired();
            Property(user => user.OfficePhone).HasMaxLength(11);
            Property(user => user.Email).HasMaxLength(50).IsRequired();
            Property(user => user.FoundTime).IsRequired();
            Property(user => user.Founder).IsRequired();
            Property(user => user.ModifyPerson).IsRequired();
            Property(user => user.ModifyTime).IsRequired();
        }
    }
}
