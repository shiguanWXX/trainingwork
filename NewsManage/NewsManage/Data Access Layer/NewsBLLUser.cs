using NewsManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using Microsoft.ApplicationInsights.DataContracts;
using NewsManage.ViewModels;

namespace NewsManage.Data_Access_Layer
{
    public class NewsBLLUser
    {
        /// <summary>
        /// 检查用户是否存在于数据库
        /// </summary>
        /// <param name="user">登陆者信息</param>
        /// <returns></returns>
        public User CheckUser(User user)
        {
            NewsDAL newsDal = new NewsDAL();
            user.Password = Sha256(user.Password);
            var userE = newsDal.User.FirstOrDefault(u => u.Account == user.Account && u.Password == user.Password);
            return userE;
        }

        /// <summary>
        /// 初始化一位用户
        /// </summary>
        public void NewUser()
        {
            NewsDAL newsDal = new NewsDAL();
            var userCount = newsDal.User.Count();
            if (userCount == 0)
            {
                Depart dept = new Depart { Id = "1", DeptName = "新闻部", DeptType = "1", DeptAddress = "地址" };
                newsDal.Depart.Add(dept);
                newsDal.SaveChanges();
                string password = "123456";
                password = Sha256(password);
                User user = new User { Account = "030293", RealName = "李茉", Password = password, Role = "1" };
                newsDal.User.Add(user);
                newsDal.SaveChanges();
            }
            
        }

        /// <summary>
        /// 对用户密码进行加密（密码加密采用SHA256 算法）
        /// </summary>
        /// <param name="plainText">密码</param>
        /// <returns></returns>
        public static string Sha256(string plainText)
        {
            SHA256Managed _sha256 = new SHA256Managed();
            byte[] _cipherText = _sha256.ComputeHash(Encoding.Default.GetBytes(plainText));
            return Convert.ToBase64String(_cipherText);
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="pageInfo">页面信息</param>
        /// <param name="total">用户总数</param>
        /// <param name="departId">部门编号</param>
        /// <returns></returns>
        public List<UserViewModel> GetUserList(PageInfo pageInfo,out int total,string departId)
        {
            List<UserViewModel> userViewModels = new List<UserViewModel>();
            NewsDAL newsDal = new NewsDAL();
            var query = from user in newsDal.User
                        join dept in newsDal.Depart
                        on user.DepartId equals dept.Id
                        select new
                        {
                            user.UserId,
                            user.RealName,
                            user.Account,
                            user.Role,
                            dept.DeptName,
                            user.DepartId
                        };
            if (departId != "")
            {
                query = query.Where(user => user.DepartId == departId);
            }
            if (pageInfo.Search == "" || pageInfo.Search==null)
            {
                if (pageInfo.SortDir == "asc")
                {
                    query = query.OrderBy(user => user.UserId);
                }
                else
                {
                    query = query.OrderByDescending(user => user.UserId);
                }
            }
            else
            {
                query = query.Where(user => user.RealName.Contains(pageInfo.Search));
            }

            foreach (var item  in query)
            {
                UserViewModel userViewModel = new UserViewModel();
                userViewModel.UserId = item.UserId;
                userViewModel.RealName = item.RealName;
                userViewModel.Account = item.Account;
                userViewModel.Role = item.Role;
                userViewModel.DeptName = item.DeptName;
                userViewModel.DepartId = item.DepartId;
                userViewModels.Add(userViewModel);
            }
            total = userViewModels.Count();
            return userViewModels.Skip(pageInfo.PageIndex.Value).Take(pageInfo.PageSize.Value).ToList();
        }
        /// <summary>
        /// 获取单条用户信息
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public User GetSingleUser(int userId)
        {
            NewsDAL newsDal = new NewsDAL();
            var query = (from user in newsDal.User
                where user.UserId == userId
                select user).FirstOrDefault();
            return query;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <returns></returns>
        public int AddUser(User user) {
            NewsDAL newsDal = new NewsDAL();
            user.Password = Sha256(user.Password);
            newsDal.User.Add(user);
            return newsDal.SaveChanges();
        }
        /// <summary>
        /// 更新单条数据
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <returns></returns>
        public int UpdateUser(User user)
        {
            NewsDAL newsDal=new NewsDAL();
            var users = newsDal.User.Single(userd => userd.UserId == user.UserId);
            users.Password = user.Password;
            users.RealName = user.RealName;
            users.Role = user.Role;
            users.DepartId = user.DepartId;
            users.Account = user.Account;
           return  newsDal.SaveChanges();
        }
        /// <summary>
        /// 检查用户名是否重复
        /// </summary>
        /// <param name="account">用户名</param>
        /// <returns></returns>
        public bool CheckAccount(string account)
        {
            NewsDAL newsDal = new NewsDAL();
            var count = newsDal.User.Count(user => user.Account == account);
            bool check = false;
            if (count > 0)
            {
                check = false;
            }
            else
            {
                check = true;
            }
            return check;
        }

    }
}