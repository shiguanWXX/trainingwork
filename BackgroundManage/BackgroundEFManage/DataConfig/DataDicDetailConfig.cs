using System.Data.Entity.ModelConfiguration;
using BackgroundEFManage.Model;

namespace BackgroundEFManage.DataConfig
{
  public  class DataDicDetailConfig:EntityTypeConfiguration<DataDicDetail>
    {
        public DataDicDetailConfig()
        {
            this.ToTable("DataDicDetial");
            this.HasKey(datadetail => datadetail.Id);
            Property(datadetail => datadetail.DeChName).HasMaxLength(50).IsRequired();
            Property(datadetail => datadetail.DeEnName).HasMaxLength(50).IsRequired();
            Property(datadetail => datadetail.Description).HasMaxLength(600);
            Property(datadetail => datadetail.Sort).HasMaxLength(20).IsRequired();
            Property(datadetail => datadetail.Founder).IsRequired();
            Property(datadetail => datadetail.FoundTime).IsRequired();
        }
    }
}
