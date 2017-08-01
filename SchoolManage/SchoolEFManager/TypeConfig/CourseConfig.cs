using System.Data.Entity.ModelConfiguration;

using SchoolEFManager.Models;

namespace SchoolEFManager.TypeConfig
{
    /// <summary>
    /// 课程表属性配置
    /// </summary>
    public class CourseConfig:EntityTypeConfiguration<Course>
    {
        public CourseConfig()
        {
            this.ToTable("Course");
            this.HasKey(course => course.Id);
            this.HasMany(course => course.Grades).WithRequired(grade => grade.Course).HasForeignKey(grade=>grade.CourseId).WillCascadeOnDelete(true);
            Property(course => course.Id).IsRequired().HasMaxLength(6);
            Property(course => course.CourseName).HasMaxLength(10);

        }
    }
}