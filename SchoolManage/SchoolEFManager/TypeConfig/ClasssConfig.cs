using SchoolEFManager.Models;
using System.Data.Entity.ModelConfiguration;

namespace SchoolEFManager.TypeConfig
{
    public class ClasssConfig:EntityTypeConfiguration<Classs>
    {
        /// <summary>
        /// 班级表属性配置
        /// </summary>
        public ClasssConfig()
        {
            this.ToTable("Classs");
            this.HasKey(classs => classs.Id);
            this.HasMany(classs => classs.Students).WithRequired(student => student.Classs).HasForeignKey(student => student.ClasssId).WillCascadeOnDelete(true);
            Property(classs => classs.Id).IsRequired().HasMaxLength(5);
            Property(classs => classs.ClassName).HasMaxLength(10);
        }
    }
}