using System.Data.Entity.ModelConfiguration;

using SchoolEFManager.Models;

namespace SchoolEFManager.TypeConfig
{
    /// <summary>
    /// 教师表属性配置
    /// </summary>
    public class TeacherConfig:EntityTypeConfiguration<Teacher>
    {
        public TeacherConfig()
        {
            this.ToTable("Teacher");
            this.HasKey(teacher => teacher.Id);
            Property(teacher => teacher.TecName).HasMaxLength(8);
            Property(teacher => teacher.Id).IsRequired().HasMaxLength(6);
            Property(teacher => teacher.TecPassword).HasMaxLength(8).IsRequired();
            Property(teacher => teacher.Department).HasMaxLength(25);
            Property(teacher => teacher.Role).HasMaxLength(4);
            Property(teacher => teacher.TecSex).HasMaxLength(2);
        }
    }
}