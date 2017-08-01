using SchoolEFManager.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace SchoolEFManager
{
    public class StudentBLL
    {
        /// <summary>
        /// 获取"市场营销"班级的学生年龄大于20岁的男同学
        /// </summary>
        /// <returns></returns>
        public List<Student> GetStudents()
        {
            List<Student> studentss = new List<Student>();
            using (SchoolDb db = new SchoolDb())
            {
                //基于表达式查询
                //var query1 = (from student in db.Students
                //    join c in db.Classses
                //    on student.ClasssId equals c.Id
                //    where student.Age > 20 && student.Sex == "男" && c.ClassName == "市场营销"
                //    select student).ToList();

                //基于方法查询
                //var query1 = db.Students.Where(student => student.Age > 20
                //                                          && student.Sex == "男" && student.Classs.ClassName == "市场营销")
                //    .ToList();

                var query = db.Students
                    .Join(db.Classses, student => student.ClasssId, classs => classs.Id,
                        (student, classs) => new
                        {
                            student.Id,
                            student.StuName,
                            student.Age,
                            student.Sex,
                            student.ClasssId,
                            student.Department,
                            classs.ClassName
                        }).Where(stuclass => stuclass.Sex == "男" && stuclass.Age > 20 && stuclass.ClassName == "市场营销")
                    .ToList();
                foreach (var item in query)
                {
                    Student students = new Student();
                    students.Id = item.Id;
                    students.StuName = item.StuName;
                    students.Age = item.Age;
                    students.Sex = item.Sex;
                    students.ClasssId = item.ClasssId;
                    students.Department = item.Department;
                    studentss.Add(students);
                }
                return studentss;
            }
        }

        /// <summary>
        /// 查询"市场营销"班级所有学生"C#语言"的成绩，并按照高低排序
        /// </summary>
        /// <returns></returns>
        public List<Grade> GetGrade()
        {
            List<Grade> gradess = new List<Grade>();
            using (SchoolDb db = new SchoolDb())
            {
                //基于表达式查询
                //var query1 = (from u in db.Grades
                //    join course in db.Courses
                //    on grade.CourseId equals course.Id
                //    join student in db.Students
                //    on grade.StudentId equals student.Id
                //    where course.CourseName == "C#语言" && student.ClasssId ==
                //          (from classs in db.Classses where classs.ClassName == "市场营销" select classs.Id)
                //          .FirstOrDefault() orderby grade.Grades descending 
                //    select grade).ToList();

                //基于方法查询
                //var query1 = db.Grades
                //    .Where(grade => grade.Course.CourseName == "C#语言" && grade.Student.Classs.ClassName == "市场营销")
                //    .OrderByDescending(grade => grade.Grades)
                //    .ToList();

                var query = db.Grades
                    .Join(db.Courses, grade => grade.CourseId, course => course.Id, (grade, course) => new
                    {
                        grade.StudentId,
                        CourseId= grade.CourseId,
                        course.CourseName,
                        grade.Grades
                    }).Where(gradCour => gradCour.CourseName == "C#语言")
                    .Join(db.Students, gradCour => gradCour.StudentId, student => student.Id, (gradCour, student) => new
                    {
                        StudentId=student.Id,
                        gradCour.CourseId,
                        student.ClasssId,
                        gradCour.Grades
                    }).Join(db.Classses, student => student.ClasssId, classs => classs.Id, (student, classs) => new
                    {
                        student.CourseId,
                        student.StudentId,
                        student.Grades,
                        classs.ClassName
                    }).Where(stuClass => stuClass.ClassName == "市场营销").OrderByDescending(stuClass=> stuClass.Grades).ToList();
                foreach (var item in query)
                {
                    Grade grades = new Grade();
                    grades.StudentId = item.StudentId;
                    grades.CourseId = item.CourseId;
                    grades.Grades = item.Grades;
                    gradess.Add(grades);
                }
                return gradess;
                
            }
        }

        /// <summary>
        /// 查询"市场营销"班级的学生总数
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            using (SchoolDb db =new SchoolDb() )
            {
                //基于表达式查询
                //var count1 =( from student in db.Students
                //    join classs in db.Classses
                //    on student.ClasssId equals classs.Id
                //    where classs.ClassName == "市场营销"
                //    select student).Count();

                //基于方法查询
                //var count1 = db.Students.Count(student => student.Classs.ClassName == "市场营销");

                var count = db.Students
                    .Join(db.Classses, student => student.ClasssId, classs => classs.Id,
                        (student, classs) => new
                        {
                            student.Id,
                            student.ClasssId,
                            classs.ClassName
                        })
                    .Count(stuClass => stuClass.ClassName == "市场营销");
                return count;
            }

        }

        /// <summary>
        /// 查询"市场营销"班级学号在11-20之前的同学
        /// </summary>
        /// <returns></returns>
        public List<Student> GetRangeStudents()
        {
            List<Student> studentss = new List<Student>();
            using (SchoolDb db=new SchoolDb())
            {
                //基于表达式查询
                //var query1 =(from student in db.Students
                //    join classs in db.Classses
                //    on student.ClasssId equals classs.Id
                //    where student.Id > 11 && student.Id < 20 && classs.ClassName == "市场营销"
                //    select student).ToList();

                //基于方法查询
                //var query1 = db.Students.Where(student => student.Id > 11 && student.Id < 20 &&
                //                                         student.Classs.ClassName == "市场营销").ToList();

                var query = db.Students
                    .Join(db.Classses, student => student.ClasssId, classs => classs.Id,
                    (student, classs) => new
                    {
                        student.Id,
                        student.StuName,
                        student.Age,
                        student.Sex,
                        student.ClasssId,
                        student.Department,
                        classs.ClassName
                    })
                    .Where(stuClass => stuClass.ClassName == "市场营销" && stuClass.Id > 11 && stuClass.Id < 20).ToList();
                foreach (var item in query)
                {
                    Student students = new Student();
                    students.Id = item.Id;
                    students.StuName = item.StuName;
                    students.Age = item.Age;
                    students.Sex = item.Sex;
                    students.ClasssId = item.ClasssId;
                    students.Department = item.Department;
                    studentss.Add(students);
                }
                return studentss;
            }
            
        }

        /// <summary>
        /// 查询"市场营销"班级学号最大的同学
        /// </summary>
        /// <returns></returns>
        public Student GetMaxId()
        {
            Student students = new Student();
            using (SchoolDb db = new SchoolDb())
            {
                //基于表达式查询
                //var query1 = (from student in db.Students
                //    join classs in db.Classses
                //    on student.ClasssId equals classs.Id
                //    where classs.ClassName == "市场营销" orderby student.Id descending 
                //    select student).First();

                //基于方法查询
                //var query1 = db.Students.Where(student => student.Classs.ClassName == "市场营销")
                //    .OrderByDescending(student => student.Id).First();

                var query = db.Students
                    .Join(db.Classses, student => student.ClasssId, classs => classs.Id,
                        (student, classs) => new
                        {
                            student.Id,
                            student.StuName,
                            student.Age,
                            student.Sex,
                            student.ClasssId,
                            student.Department,
                            classs.ClassName
                        }).Where(stuClass => stuClass.ClassName == "市场营销").OrderByDescending(stuClass => stuClass.Id)
                    .FirstOrDefault();

                students.Id = query.Id;
                students.StuName = query.StuName;
                students.Age = query.Age;
                students.Sex = query.Sex;
                students.ClasssId = query.ClasssId;
                students.Department = query.Department;
                return students;
            }

        }

        /// <summary>
        /// 查询"市场营销"班级男同学的平均成绩、最高成绩
        /// </summary>
        /// <returns></returns>
        public List<Grade> GetMaxAvg()
        {
            using (SchoolDb db = new SchoolDb())
            {
                var grades = new List<Grade>();
                //基于表达式查询
                //var Gradess1 = (from u in db.Grades
                //    join c in db.Students
                //    on u.StudentId equals c.Id
                //    where c.Sex=="男" && c.ClasssId == (from m in db.Classses
                //              where m.ClassName == "市场营销"
                //              select m.Id).FirstOrDefault()
                //    group u by new {CourseId = u.CourseId}
                //    into grouped
                //    orderby grouped.Max(n => n.Grades)
                //    select new
                //    {
                //        CourseId = grouped.Key.CourseId,
                //        MaxGrade= grouped.Max(n => n.Grades),
                //        AvgGrades = grouped.Average(n => n.Grades)
                //    }).ToList();

                //基于方法查询
                //var gradess1 = db.Grades
                //    .Where(grade => grade.Student.Classs.ClassName == "市场营销" && grade.Student.Sex == "男")
                //    .GroupBy(grade => grade.CourseId)
                //    .Select(grade => new
                //    {
                //        CourseId = grade.Key,
                //        MaxGrade = grade.Max(gradest => gradest.Grades),
                //        AvgGrades = grade.Average(gradest => gradest.Grades)
                //    }).ToList();

                var gradess = db.Grades.Join(db.Students, grade => grade.StudentId, student => student.Id,
                        (grade, student) => new
                        {
                            grade.CourseId,
                            grade.Grades,
                            student.ClasssId,
                            StudentId = student.Id,
                            student.Sex
                        }).Where(gradStu => gradStu.Sex == "男").Join(db.Classses, gradStu => gradStu.ClasssId,
                        classs => classs.Id, (gradStu, classs) => new
                        {
                            gradStu.CourseId,
                            gradStu.Grades,
                            gradStu.StudentId,
                            classs.ClassName
                        }).Where(classs => classs.ClassName == "市场营销")
                    .GroupBy(grade => grade.CourseId)
                    .Select(grade => new
                    {
                        CourseId = grade.Key,
                        MaxGrade = grade.Max(gradest => gradest.Grades),
                        AvgGrades = grade.Average(gradest => gradest.Grades)
                    }).ToList();
                foreach (var item in gradess)
                {
                    var grademodel = new Grade();
                    grademodel.CourseId = item.CourseId;
                    grademodel.Avg = item.AvgGrades;
                    grademodel.MaxGrade = item.MaxGrade;
                    grades.Add(grademodel);
                }
                ;
                return grades;
            };


        }
    }

}