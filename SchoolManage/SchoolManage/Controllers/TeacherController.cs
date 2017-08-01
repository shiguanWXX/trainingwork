using System.Web.Mvc;


namespace SchoolEFManager.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            StudentBLL Tbll = new StudentBLL();
            //获取"市场营销"班级的学生年龄大于20岁的男同学
            var Students1 = Tbll.GetStudents();

            //查询"市场营销"班级所有学生"C#语言"的成绩，并按照高低排序
            var Teachers2 = Tbll.GetGrade();

            //查询"市场营销"班级的学生总数
            var Teachers3 = Tbll.GetCount();

            //查询"市场营销"班级学号在11-20之前的同学
            var Teachers4 = Tbll.GetRangeStudents();

            //查询"市场营销"班级学号最大的同学
            var Teachers5 = Tbll.GetMaxId();

            //查询"市场营销"班级男同学的平均成绩、最高成绩
            //var Teachers6 = Tbll.GetMaxAvg();

            return View();
        }
    }
}