using SchoolEFManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;

using  SchoolEFManager.TypeConfig;
namespace SchoolEFManager
{
    public class SchoolDb:DbContext
    {
        public SchoolDb() : base("SchoolConnection")
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TeacherConfig());
            modelBuilder.Configurations.Add(new ClasssConfig());
            modelBuilder.Configurations.Add(new StudentConfig());
            modelBuilder.Configurations.Add(new CourseConfig());
            modelBuilder.Configurations.Add(new GradeConfig());

        }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Classs> Classses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Grade> Grades { get; set; }
    }
}