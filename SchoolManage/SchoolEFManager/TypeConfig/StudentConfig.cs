using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

using SchoolEFManager.Models;

namespace SchoolEFManager.TypeConfig
{
    /// <summary>
    /// 学生表属性配置
    /// </summary>
    public class StudentConfig:EntityTypeConfiguration<Student>
    {
        public StudentConfig()
        {
            this.ToTable("Student");
            this.HasKey(student => student.Id);
            this.HasMany(student => student.Grades).WithRequired(grade => grade.Student).HasForeignKey(grade=>grade.StudentId).WillCascadeOnDelete(true);
            Property(student => student.Id).IsRequired();
            Property(student => student.Department).HasMaxLength(25);
            Property(student => student.Sex).HasMaxLength(2);
            Property(student => student.StuPassword).HasMaxLength(8).IsRequired();
            Property(student => student.StuName).HasMaxLength(8);
        }
    }
}