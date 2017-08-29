using System.Data.Entity.ModelConfiguration;
using BackgroundEFManage.Model;

namespace BackgroundEFManage.DataConfig
{
    /// <summary>
    /// 数据字典属性配置
    /// </summary>
    public  class DataDicConfig:EntityTypeConfiguration<DataDic>
    {
        public DataDicConfig()
        {
            this.ToTable("DataDic");
            this.HasKey(datadic => datadic.Id).HasMany(datadic=> datadic.DataDicDetails)
                .WithRequired(datadicde=>datadicde.DataDic)
                .HasForeignKey(datadicde => datadicde.DId)
                .WillCascadeOnDelete();
            Property(datadic => datadic.DChName).HasMaxLength(50);
            Property(datadic => datadic.DEnName).HasMaxLength(50);
            Property(datadic => datadic.Description).HasMaxLength(600);
            Property(datadic => datadic.Founder).IsRequired();
            Property(datadic => datadic.FoundTime).IsRequired();
        }
    }
}
