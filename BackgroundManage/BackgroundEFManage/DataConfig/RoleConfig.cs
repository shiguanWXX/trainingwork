using System.Data.Entity.ModelConfiguration;
using BackgroundEFManage.Model;

namespace BackgroundEFManage.DataConfig
{
    /// <summary>
    /// 角色属性配置
    /// </summary>
    public  class RoleConfig:EntityTypeConfiguration<Role>
    {
        public RoleConfig()
        {
            this.ToTable("Role");
            this.HasKey(role => role.Id);
            Property(role => role.RName).HasMaxLength(50).IsRequired();
            Property(role => role.Code).HasMaxLength(80);
            Property(role => role.Description).HasMaxLength(600);
            Property(role => role.FoundTime).IsRequired();
            Property(role => role.Founder).IsRequired();
            Property(role => role.ModifyPerson).IsRequired();
            Property(role => role.ModifyTime).IsRequired();
        }
    }
}
