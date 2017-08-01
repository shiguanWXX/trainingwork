using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolEFManager.ViewModels;

namespace SchoolEFManager.Controllers
{
    public class SchoolController : Controller
    {
        // GET: Student
        /// <summary>
        /// 获取"市场营销"班级的学生年龄大于20岁的男同学
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            StudentBLL stuBll=new StudentBLL();
            var ageOver = stuBll.GetStudents();
            List<StudentViewModel> studentViewModels = new List<StudentViewModel>();
            foreach (var item in ageOver)
            {
                StudentViewModel studentViewModel = new StudentViewModel();
                studentViewModel.Id = item.Id;
                studentViewModel.StuName = item.StuName;
                studentViewModel.Age = item.Age;
                studentViewModel.ClasssId = item.ClasssId;
                studentViewModel.Department = item.Department;
                studentViewModel.Sex = item.Sex;
                studentViewModels.Add(studentViewModel);
            }
            return View(studentViewModels);
        }

        /// <summary>
        /// 查询"市场营销"班级所有学生"C#语言"的成绩，并按照高低排序
        /// </summary>
        /// <returns></returns>
        public ActionResult CsharpGrade()
        {
            StudentBLL stuBll = new StudentBLL();
            var chasrpGrade = stuBll.GetGrade();
            List<GradeViewModel> gradeViewModels = new List<GradeViewModel>();
            foreach (var item in chasrpGrade)
            {
                GradeViewModel gradeViewModel = new GradeViewModel();
                gradeViewModel.Grades = item.Grades;
                gradeViewModel.CourseId = item.CourseId;
                gradeViewModel.StudentId = item.StudentId;
                gradeViewModels.Add(gradeViewModel);
            }
            return View(gradeViewModels);
        }

        /// <summary>
        /// 查询"市场营销"班级的学生总数
        /// </summary>
        /// <returns></returns>
        public ActionResult CountStudent()
        {
            StudentBLL stuBll = new StudentBLL();
            ViewBag.count = stuBll.GetCount();
            return View();
        }

        /// <summary>
        /// 查询"市场营销"班级学号在11-20之前的同学
        /// </summary>
        /// <returns></returns>
        public ActionResult StuIdRange()
        {
            StudentBLL stuBll = new StudentBLL();
            var stuIdRange = stuBll.GetRangeStudents();
            List<StudentViewModel> studentViewModels = new List<StudentViewModel>();
            foreach (var item in stuIdRange)
            {
                StudentViewModel studentViewModel = new StudentViewModel();
                studentViewModel.Id = item.Id;
                studentViewModel.StuName = item.StuName;
                studentViewModel.Age = item.Age;
                studentViewModel.ClasssId = item.ClasssId;
                studentViewModel.Department = item.Department;
                studentViewModel.Sex = item.Sex;
                studentViewModels.Add(studentViewModel);
            }
            return View(studentViewModels);
        }

        /// <summary>
        /// 查询"市场营销"班级学号最大的同学
        /// </summary>
        /// <returns></returns>
        public ActionResult StuIdMax()
        {
            StudentBLL stuBll = new StudentBLL();
            var stuIdRange = stuBll.GetMaxId();
            StudentViewModel studentViewModel = new StudentViewModel();
            
                studentViewModel.Id = stuIdRange.Id;
                studentViewModel.StuName = stuIdRange.StuName;
                studentViewModel.Age = stuIdRange.Age;
                studentViewModel.ClasssId = stuIdRange.ClasssId;
                studentViewModel.Department = stuIdRange.Department;
                studentViewModel.Sex = stuIdRange.Sex;
            
            return View(studentViewModel);
        }

        /// <summary>
        /// 查询"市场营销"班级男同学的平均成绩、最高成绩
        /// </summary>
        /// <returns></returns>
        public ActionResult MaxAvgGrade()
        {
            StudentBLL stuBll = new StudentBLL();
            var stuIdRange = stuBll.GetMaxAvg();
            List<MaxAvgViewModel> maxAvgViewModels = new List<MaxAvgViewModel>();
            foreach (var item in stuIdRange)
            {
                MaxAvgViewModel maxAvgViewModel = new MaxAvgViewModel();
                maxAvgViewModel.CourseId = item.CourseId;
                maxAvgViewModel.Avg = item.Avg;
                maxAvgViewModel.MaxGrade = item.MaxGrade;
                maxAvgViewModels.Add(maxAvgViewModel);
            }
            return View(maxAvgViewModels);
        }
    }
}