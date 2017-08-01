using System.Data.Entity.ModelConfiguration;

using SchoolEFManager.Models;

namespace SchoolEFManager.TypeConfig
{
    /// <summary>
    /// 成绩表属性配置
    /// </summary>
    public class GradeConfig:EntityTypeConfiguration<Grade>
    {
        public GradeConfig()
        {
            this.ToTable("Grade");
            this.HasKey(grade => grade.Id);
            this.Ignore(grade => grade.MaxGrade);
        }
    }
}