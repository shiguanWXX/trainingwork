using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolEFManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEFManager.Tests
{
    [TestClass()]
    public class SchoolBLLTests
    {
        /// <summary>
        /// 获取"市场营销"班级的学生年龄大于20岁的男同学
        /// </summary>
        [TestMethod()]
        public void GetStudentsTest()
        {
            StudentBLL studentServeice = new StudentBLL();
            var students = studentServeice.GetStudents();

            foreach (var student in students)
            {
                Console.WriteLine("studentId=" + student.Id + ";stuName=" + student.StuName);
                //Assert.Fail();
            }
        }

        /// <summary>
        ///  查询"市场营销"班级所有学生"C#语言"的成绩，并按照高低排序
        /// </summary>
        [TestMethod()]
        public void GetGradeTest()
        {
            StudentBLL studentServeice = new StudentBLL();
            var grade = studentServeice.GetGrade();

            //Assert.Fail();
        }

        /// <summary>
        ///  查询"市场营销"班级的学生总数
        /// </summary>
        [TestMethod()]
        public void GetCountTest()
        {
            StudentBLL studentServeice = new StudentBLL();
            var count = studentServeice.GetCount();
            // Assert.Fail();
        }

        /// <summary>
        /// 查询"市场营销"班级学号在11-20之前的同学
        /// </summary>
        [TestMethod()]
        public void GetRangeStudentsTest()
        {
            StudentBLL studentServeice = new StudentBLL();
            var stuRange = studentServeice.GetRangeStudents();
            //Assert.Fail();
        }

        /// <summary>
        /// 查询"市场营销"班级学号最大的同学
        /// </summary>
        [TestMethod()]
        public void GetMaxIdTest()
        {
            StudentBLL studentServeice = new StudentBLL();
            var maxId = studentServeice.GetMaxId();
            // Assert.Fail();
        }

        /// <summary>
        /// 查询"市场营销"班级男同学的平均成绩、最高成绩
        /// </summary>
        [TestMethod()]
        public void GetMaxAvgTest()
        {
            StudentBLL studentServeice = new StudentBLL();
            var maxAvg = studentServeice.GetMaxAvg();
            //Assert.Fail();
        }
    }
}