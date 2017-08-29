using BackgroundEFManage.Model;
using BackgroundEFManage.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BackgroundEFManage.DataDAL
{
  public  class BackgroundsDAL
    {
        #region 用户相关操作
        /// <summary>
        /// 初始化一位用户
        /// </summary>
        public void InitializaUser()
        {
            using (BackgroundDAL context = new BackgroundDAL())
            {
                var userCount = context.Users.Count();
                if (userCount == 0)
                {
                    string password = "123456";
                    password = Sha256(password);
                    Users user = new Users
                    {
                        Account = "admin",
                        Password = password,
                        RealName = "李茉",
                        Sex = "女",
                        TelPhone = "13048596045",
                        Email = "wxx@souhu.com",
                        FoundTime = DateTime.Now,
                        Founder = 0,
                        ModifyPerson = 0,
                        ModifyTime = DateTime.Now
                    };
                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }
        }
        /// <summary>
        /// 检查用户是否存在与数据库
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <returns></returns>
        public Users CheckUsers(Users user)
        {
            using (BackgroundDAL context = new BackgroundDAL())
            {
                if (user.Password == null)
                {
                    user.Password = "";
                }
                user.Password = Sha256(user.Password);
                var userE = context.Users.FirstOrDefault(users => users.Account == user.Account &&
                                                              users.Password == user.Password);
                return userE;
            }
        }
        /// <summary>
        /// 对用户密码进行加密（密码加密采用SHA256 算法）
        /// </summary>
        /// <param name="plainText">加密字段</param>
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
        /// <param name="total">数据总数</param>
        /// <returns></returns>
        public List<Users> GetUserList(PageInfo pageInfo, out int total)
        {
            using (BackgroundDAL context = new BackgroundDAL())
            {
               IQueryable<Users> query = context.Users;
                if (!string.IsNullOrEmpty(pageInfo.Acount))
                {
                    query = query.Where(user => user.Account.Contains(pageInfo.Acount));
                }
                if (!string.IsNullOrEmpty(pageInfo.Rname))
                {
                    query = query.Where(user => user.RealName.Contains(pageInfo.Rname));
                }
                if (!string.IsNullOrEmpty(pageInfo.Mail))
                {
                    query = query.Where(user => user.Email.Contains(pageInfo.Mail));
                }
                if (pageInfo.SortDir == "asc")
                {
                    query = query.OrderBy(user => user.Account);
                }
                else
                {
                    query = query.OrderByDescending(user => user.Account);
                }
                total = query.Count();
                return query.Skip(pageInfo.PageIndex.Value).Take(pageInfo.PageSize.Value).ToList();
            }
        }
        /// <summary>
        /// 查询单个用户信息
        /// </summary>
        /// <param name="id">用户Id</param>
        /// <returns></returns>
        public Users GetSingleUser(int id)
        {
            using (BackgroundDAL context = new BackgroundDAL())
            {
                var query = context.Users.First(user => user.Id == id);
                return query;
            }
        }
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <param name="listId">角色选中Id</param>
        /// <returns></returns>
        public int AddUser(Users user, int[] listId)
        {
            using (BackgroundDAL context = new BackgroundDAL())
            {
                user.Roles = context.Role.Where(roleN => listId.Contains(roleN.Id)).ToList();
                user.Password = Sha256(user.Password);
                context.Users.Add(user);
                return context.SaveChanges();
            }
        }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <param name="listId">角色选中Id</param>
        /// <returns></returns>
        public int UpdateUser(Users user, int[] listId)
        {
            using (BackgroundDAL context = new BackgroundDAL())
            {
                var users = context.Users.Single(useres => useres.Id == user.Id);
                users.Account = user.Account;
                users.RealName = user.RealName;
                users.TelPhone = user.TelPhone;
                users.OfficePhone = user.OfficePhone;
                users.Email = user.Email;
                users.Enable = user.Enable;
                users.ModifyPerson = user.ModifyPerson;
                users.ModifyTime = DateTime.Now;
                List<Role> roleList = users.Roles.ToList();
                foreach (var item in roleList)
                {
                    users.Roles.Remove(item);
                }
                //dal.Users.Attach(user);
                users.Roles = context.Role.Where(roleN => listId.Contains(roleN.Id)).ToList();
                return context.SaveChanges();
            }
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        public int DeleteUser(int id)
        {
            using (BackgroundDAL context = new BackgroundDAL())
            {
                var user = context.Users.First(users => users.Id == id);
                context.Users.Remove(user);
                return context.SaveChanges();
            }
        }
        /// <summary>
        /// 检查用户名是否重复
        /// </summary>
        /// <param name="account">用户名</param>
        /// <param name="id">用户名Id</param>
        /// <returns></returns>
        public bool CheckAccount(string account,int id)
        {
            using (BackgroundDAL context = new BackgroundDAL())
            {
                var users = context.Users.SingleOrDefault(user => user.Account == account);
                if (users != null)
                {
                    return users.Id == id;
                }
                return true;
            }
        }

        #endregion

        #region 数据字典操作
        #region 字典信息操作
        /// <summary>
        /// 获取字典信息
        /// </summary>
        /// <param name="dicId">字典Id</param>
        /// <returns></returns>
        public List<DataDic> GetDicList(PageInfo pageInfo, out int total, int dicId)
        {
            using (BackgroundDAL context = new BackgroundDAL())
            {
                var query = context.DataDic.Where(dic => dic.Id == dicId).OrderBy(dic=>dic.Id);
                total = query.Count();
                return query.Skip(pageInfo.PageIndex.Value).Take(pageInfo.PageSize.Value).ToList();
            }
        }
        /// <summary>
        /// 获取单条字典信息
        /// </summary>
        /// <param name="detailId">字典Id</param>
        /// <returns></returns>
        public DataDic GetSigleDic(int detailId)
        {
            using (BackgroundDAL context = new BackgroundDAL())
            {
                var query = context.DataDic.Single(detail => detail.Id == detailId);
                return query;
            }
        }
        /// <summary>
        /// 新增数据字典
        /// </summary>
        /// <param name="dataDic">字典实体</param>
        /// <returns></returns>
        public int AddDataDic(DataDic dataDic)
        {
            using (BackgroundDAL context = new BackgroundDAL())
            {
                context.DataDic.Add(dataDic);
                return context.SaveChanges();
            }
        }
        /// <summary>
        /// 修改数据字典
        /// </summary>
        /// <param name="dataDic">字典实体</param>
        /// <returns></returns>
        public int UpdateDataDic(DataDic dataDic)
        {
            using (BackgroundDAL context = new BackgroundDAL())
            {
                var dic = context.DataDic.First(datadics => datadics.Id == dataDic.Id);
                dic.DChName = dataDic.DChName;
                dic.DEnName = dataDic.DEnName;
                dic.DReadonly = dataDic.DReadonly;
                dic.Description = dataDic.Description;
                dic.FoundTime = dataDic.FoundTime;
                dic.Founder = dataDic.Founder;
                return context.SaveChanges();
            }
        }
        /// <summary>
        /// 检查字典中文名称是否重复
        /// </summary>
        /// <param name="dChName">字典中文名称</param>
        /// <param name="id">字典中文名称</param>
        /// <returns></returns>
        public bool CheckDChName(string dChName,int id)
        {
            using (BackgroundDAL context = new BackgroundDAL())
            {
                var dataDic = context.DataDic.SingleOrDefault(dic => dic.DChName == dChName);
                if (dataDic != null)
                {
                    return dataDic.Id == id;
                }
                return true;
            }
        }
        /// <summary>
        ///检查字典英文名称是否重复
        /// </summary>
        /// <param name="dEnName">字典的英文名称</param>
        ///  <param name="id">字典的Id</param>
        /// <returns></returns>
        public bool CheckDEnName(string dEnName,int id)
        {
            using (BackgroundDAL context = new BackgroundDAL())
            {
                var dataDic = context.DataDic.SingleOrDefault(dic => dic.DEnName == dEnName);
                if (dataDic != null)
                {
                    return dataDic.Id == id;
                }
                return true;
            }
        }
        #endregion

        #region 字典详细信息的操作

        /// <summary>
        /// 获取字典详细信息
        /// </summary>
        /// <param name="pageInfo">页面信息</param>
        /// <param name="total">数据条数</param>
        /// <param name="dicId">字典Id</param>
        /// <returns></returns>
        public List<DataDicDetailViewModel> GetDicDetailList(PageInfo pageInfo, out int total, int dicId)
        {
            using (BackgroundDAL context = new BackgroundDAL())
            {
                List<DataDicDetailViewModel> dataDicDetailViewModels = new List<DataDicDetailViewModel>();
                var query = context.DataDicDetial.Join(context.Users, detail => detail.ModifyPerson, user => user.Id, (detail, user) => new {
                    detail.Id,
                    detail.DeChName,
                    detail.DeEnName,
                    detail.Sort,
                    detail.Enable,
                    detail.DId,
                    ModifyTime = detail.ModifyTime.ToString(),
                    detail.ModifyPerson,
                    detail.Description,
                    user.RealName
                }).Where(detail => detail.DId == dicId);
                query = query.OrderBy(detial => detial.Sort);
                foreach (var item in query)
                {
                    DataDicDetailViewModel dataDicDetailViewModel = new DataDicDetailViewModel();
                    dataDicDetailViewModel.Id = item.Id;
                    dataDicDetailViewModel.DeChName = item.DeChName;
                    dataDicDetailViewModel.DeEnName = item.DeEnName;
                    dataDicDetailViewModel.Description = item.Description;
                    dataDicDetailViewModel.Sort = item.Sort;
                    dataDicDetailViewModel.Enable = item.Enable;
                    dataDicDetailViewModel.ModifyPerson = item.ModifyPerson;
                    dataDicDetailViewModel.ModifyTime = item.ModifyTime;
                    dataDicDetailViewModel.RealName = item.RealName;
                    dataDicDetailViewModels.Add(dataDicDetailViewModel);
                }
                total = dataDicDetailViewModels.Count();
                return dataDicDetailViewModels.Skip(pageInfo.PageIndex.Value).Take(pageInfo.PageSize.Value).ToList();
            }
            
        }
        /// <summary>
        /// 获取单条字典详细信息
        /// </summary>
        /// <param name="detailId">字典详细信息Id</param>
        /// <returns></returns>
        public DataDicDetail GetSigleDicDetail(int detailId)
        {
            using (BackgroundDAL context = new BackgroundDAL())
            {
                var query = context.DataDicDetial.Single(detail => detail.Id == detailId);
                return query;
            }
        }
        /// <summary>
        /// 新增单条字典详细信息
        /// </summary>
        /// <param name="detail">字典详细信息实体</param>
        /// <returns></returns>
        public int AddDicDetail(DataDicDetail detail)
        {
            using (BackgroundDAL context = new BackgroundDAL())
            {
                context.DataDicDetial.Add(detail);
                return context.SaveChanges();
            }
        }
        /// <summary>
        /// 更新单条详细字典信息
        /// </summary>
        /// <param name="detail">字典详细信息</param>
        /// <returns></returns>
        public int UpdateDicdetail(DataDicDetail detail)
        {
            using (BackgroundDAL context = new BackgroundDAL())
            {
                var dicDetail = context.DataDicDetial.Single(details => details.Id == detail.Id);
                dicDetail.DeChName = detail.DeChName;
                dicDetail.DeEnName = detail.DeEnName;
                dicDetail.Sort = detail.Sort;
                dicDetail.Enable = detail.Enable;
                dicDetail.ModifyTime = detail.ModifyTime;
                dicDetail.ModifyPerson = detail.ModifyPerson;
                dicDetail.Description = detail.Description;
                return context.SaveChanges();
            }
        }
        /// <summary>
        /// 检查字典详细中文名是否重复
        /// </summary>
        /// <param name="deChName">字典详细中文名</param>
        /// <param name="id">字典详细Id</param>
        /// <returns></returns>
        public bool CheckDeChName(string deChName,int id)
        {
            using (BackgroundDAL context = new BackgroundDAL())
            {
                var details = context.DataDicDetial.SingleOrDefault(detail => detail.DeChName == deChName);
                if (details != null)
                {
                    return details.Id == id;
                }
                return true;
            }
        }
        /// <summary>
        /// 检查字典详细的英文名称是否重复
        /// </summary>
        /// <param name="deEnName">字典详细英文名称</param>
        /// <param name="id">字典详细Id</param>
        /// <returns></returns>
        public bool CheckDeEnName(string deEnName,int id)
        {
            using (BackgroundDAL context = new BackgroundDAL())
            {
                var details = context.DataDicDetial.SingleOrDefault(detail => detail.DeEnName == deEnName);
                if (details != null)
                {
                    return details.Id == id;
                }
                return true;
            }
        }
        #endregion
        #endregion
        #region 角色信息操作
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="pageInfo">页面信息</param>
        /// <param name="total">数据条数</param>
        /// <param name="roleName">角色名</param>
        /// <returns></returns>
        public List<Role> GetRoleList(PageInfo pageInfo, out int total, string roleName)
        {
            using (BackgroundDAL context = new BackgroundDAL())
            {
                IQueryable<Role> query = context.Role;
                if (!string.IsNullOrEmpty(roleName))
                {
                    query = query.Where(role => role.RName.Contains(roleName));
                }
                if (pageInfo.SortDir == "asc")
                {
                    query = query.OrderBy(role => role.RName);
                }
                else
                {
                    query = query.OrderByDescending(role => role.RName);
                }
                total = query.Count();
                return query.Skip(pageInfo.PageIndex.Value).Take(pageInfo.PageSize.Value).ToList();
            }
        }
        /// <summary>
        /// 获取单条角色信息
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <returns></returns>
        public Role GetSigleRole(int id)
        {
            using (BackgroundDAL context = new BackgroundDAL())
            {
                RoleViewModel roleViewModel = new RoleViewModel();
                var role = context.Role.Single(roles => roles.Id == id);
                roleViewModel.Id = role.Id;
                roleViewModel.RName = role.RName;
                roleViewModel.Code = role.Code;
                roleViewModel.Description = role.Description;
                roleViewModel.Founder = role.Founder;
                roleViewModel.FoundTime = role.FoundTime;
                roleViewModel.ModifyPerson = role.ModifyPerson;
                roleViewModel.ModifyTime = role.ModifyTime;
                return role;
            }
        }
        /// <summary>
        /// 新增角色信息
        /// </summary>
        /// <param name="role">角色实体</param>
        /// <param name="listIdA">管理员角色Id集合</param>
        /// <param name="listIdM">模块角色Id集合</param>
        /// <returns></returns>
        public int AddRole(Role role, int[] listIdA, int[] listIdM)
        {
            using (BackgroundDAL context = new BackgroundDAL())
            {
                role.Userses = context.Users.Where(userN => listIdA.Contains(userN.Id)).ToList();
                role.Modules = context.Module.Where(moduleN => listIdM.Contains(moduleN.Id)).ToList();
                context.Role.Add(role);
                return context.SaveChanges();
            }
        }
        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <param name="role">角色实体</param>
        /// <param name="listIdA">管理员角色Id集合</param>
        /// <param name="listIdM">模块角色Id集合</param>
        /// <returns></returns>
        public int UpdateRole(Role role, int[] listIdA, int[] listIdM)
        {
            using (BackgroundDAL context = new BackgroundDAL())
            {
                var roleN = context.Role.First(roles => roles.Id == role.Id);
                roleN.RName = role.RName;
                roleN.Code = role.Code;
                roleN.Description = role.Description;
                roleN.ModifyPerson = role.ModifyPerson;
                roleN.ModifyTime = role.ModifyTime;
                List<Users> userList = roleN.Userses.ToList();
                foreach (var item in userList)
                {
                    roleN.Userses.Remove(item);
                }
                List<Module> moduleList = roleN.Modules.ToList();
                foreach (var item in moduleList)
                {
                    roleN.Modules.Remove(item);
                }
                roleN.Userses = context.Users.Where(userN => listIdA.Contains(userN.Id)).ToList();
                roleN.Modules = context.Module.Where(moduleN => listIdM.Contains(moduleN.Id)).ToList();
                return context.SaveChanges();
            }
        }
        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <returns></returns>
        public int DeleteRole(int id)
        {
            using (BackgroundDAL context = new BackgroundDAL())
            {
                var role = context.Role.First(roles => roles.Id == id);
                context.Role.Remove(role);
                return context.SaveChanges();
            }
        }
        /// <summary>
        /// 检查角色名是否重复
        /// </summary>
        /// <param name="roleName">角色名</param>
        /// <param name="id">角色名</param>
        /// <returns></returns>
        public bool CheckRoleName(string roleName,int id)
        {
            using (BackgroundDAL context = new BackgroundDAL())
            {
                var roles = context.Role.SingleOrDefault(role => role.RName == roleName);
                if (roles != null)
                {
                    return roles.Id == id;
                }
                return true;
            }
        }

        #endregion
        #region 模块信息操作
        /// <summary>
        /// 获得模块信息列表
        /// </summary>
        /// <param name="pageInfo">页面信息</param>
        /// <param name="total">数据条数</param>
        /// <param name="mId">模块Id</param>
        /// <returns></returns>
        public List<Module> GetModuleList(PageInfo pageInfo, out int total, int mId)
        {
            using (BackgroundDAL context = new BackgroundDAL())
            {
                var query = context.Module.Where(module => module.Id == mId).OrderBy(module=>module.MSort);
                total = query.Count();
                return query.Skip(pageInfo.PageIndex.Value).Take(pageInfo.PageSize.Value).ToList();
            }
        }
        /// <summary>
        /// 获取单个模块信息
        /// </summary>
        /// <param name="id">模块Id</param>
        /// <returns></returns>
        public Module GetSingleModule(int id)
        {
            using (BackgroundDAL context = new BackgroundDAL())
            {
                var query = context.Module.First(module => module.Id == id);
                return query;
            }
        }
        /// <summary>
        /// 新增模块信息
        /// </summary>
        /// <param name="module">模块实体</param>
        /// <param name="listId">角色选择Id</param>
        /// <returns></returns>
        public int AddModule(Module module, int[] listId)
        {
            using (BackgroundDAL context = new BackgroundDAL())
            {
                module.Roles = context.Role.Where(roleN => listId.Contains(roleN.Id)).ToList();
                context.Module.Add(module);
                return context.SaveChanges();
            }
        }

        /// <summary>
        /// 修改模块信息
        /// </summary>
        /// <param name="module">模块实体</param>
        /// <param name="listId">角色选择Id</param>
        /// <returns></returns>
        public int UpdateModule(Module module, int[] listId)
        {
            using (BackgroundDAL context = new BackgroundDAL())
            {
                var moduleO = context.Module.Single(modules => modules.Id == module.Id);
                moduleO.MChName = module.MChName;
                moduleO.MEnName = module.MEnName;
                moduleO.MSort = module.MSort;
                moduleO.Display = module.Display;
                moduleO.Icon = module.Icon;
                moduleO.URL = module.URL;
                moduleO.Code = module.Code;
                moduleO.Description = module.Description;
                moduleO.Enable = module.Enable;
                moduleO.ModifyPerson = module.ModifyPerson;
                moduleO.ModifyTime = module.ModifyTime;
                List<Role> roleList = moduleO.Roles.ToList();
                foreach (var item in roleList)
                {
                    moduleO.Roles.Remove(item);
                }
                moduleO.Roles = context.Role.Where(roleN => listId.Contains(roleN.Id)).ToList();
                return context.SaveChanges();
            }
        }
        /// <summary>
        /// 检查模块的中文名称是否重复
        /// </summary>
        /// <param name="mChName">模块的中文名称</param>
        /// <param name="id">模块的Id</param>
        /// <returns></returns>
        public bool CheckMChName(string mChName,int id)
        {
            using (BackgroundDAL context = new BackgroundDAL())
            {
                var moduels = context.Module.SingleOrDefault(module => module.MChName == mChName);
                if (moduels != null)
                {
                    return moduels.Id == id;
                }
                return true;
            }
        }
        /// <summary>
        /// 检查模块的英文名称是否重复
        /// </summary>
        /// <param name="mEnName">模块的英文名称</param>
        /// <param name="id">模块的Id</param>
        /// <returns></returns>
        public bool CheckMEnName(string mEnName,int id)
        {
            using (BackgroundDAL context = new BackgroundDAL())
            {
                var moduels = context.Module.SingleOrDefault(module => module.MEnName == mEnName);
                if (moduels != null)
                {
                    return moduels.Id == id;
                }
                return true;
            }
        }
        #endregion
    }
}
