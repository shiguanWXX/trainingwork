using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewsManage.ViewModels;

namespace NewsManage.Data_Access_Layer
{
    public class NewsDepartBLL
    {
        public List<TreeViewModel> GetDeptTree()
        {
            NewsDAL newsDal = new NewsDAL();
            List<TreeViewModel> treeViewModels = new List<TreeViewModel>();
            //var query = from dept in newsDal.Depart
            //    where dept.FdepartId == ""
            //    select new
            //    {
            //        dept.Id,
            //        dept.FdepartId,
            //        dept.DeptName
            //    };
            var query = newsDal.Depart.Where(dept => dept.FdepartId == "").Select(dept => new
            {
                dept.Id,
                dept.FdepartId,
                dept.DeptName
            });
            foreach (var item in query)
            {
                TreeViewModel treeViewModel = new TreeViewModel();
                treeViewModel.Id = item.Id;
                treeViewModel.ParentId = item.FdepartId;
                treeViewModel.NodeName = item.DeptName;
                treeViewModels.Add(treeViewModel);
            }
            return treeViewModels;
        }
    }
}