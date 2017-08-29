using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using BackgroundEFManage.DataDAL;
using BackgroundEFManage.Model;
using BackgroundEFManage.ViewModels;

namespace BackgroundEFManage.DataBLL
{
    public class BackgroundBLL
    {
        #region 用户相关操作旧
        /// <summary>
        /// 初始化一位用户
        /// </summary>
        public void InitializaUsers()
        {
            using (BackgroundDAL dal = new BackgroundDAL())
            {
                var userCount = dal.Users.Count();
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
                    dal.Users.Add(user);
                    dal.SaveChanges();
                }
            }
        }
        /// <summary>
        /// 检查用户是否存在与数据库
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <returns></returns>
        public Users CheckUsersO(Users user)
        {
            using (BackgroundDAL dal = new BackgroundDAL())
            {
                if (user.Password == null)
                {
                    user.Password = "";
                }
                user.Password = Sha256(user.Password);
                var userE = dal.Users.FirstOrDefault(users => users.Account == user.Account &&
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
        public List<UserViewModel> GetUserListO(PageInfo pageInfo,out int total) {
           using (BackgroundDAL dal = new BackgroundDAL())
           {
               List<UserViewModel> userViewModels = new List<UserViewModel>();
               var query = dal.Users.ToList();
               if (!string.IsNullOrEmpty(pageInfo.Acount))
               {
                   query = query.Where(user => user.Account.Contains(pageInfo.Acount)).ToList();
               }
               if (!string.IsNullOrEmpty(pageInfo.Rname))
               {
                   query = query.Where(user => user.RealName.Contains(pageInfo.Rname)).ToList();
               }
               if (!string.IsNullOrEmpty(pageInfo.Mail))
               {
                   query = query.Where(user => user.Email.Contains(pageInfo.Mail)).ToList();
               }
               if (pageInfo.SortDir == "asc")
               {
                   query = query.OrderBy(user => user.Account).ToList();
               }
               else
               {
                   query = query.OrderByDescending(user => user.Account).ToList();
               }
               foreach (var item in query)
               {
                   UserViewModel userViewModel = new UserViewModel();
                   userViewModel.Id = item.Id;
                   userViewModel.RealName = item.RealName;
                   userViewModel.Account = item.Account;
                   userViewModel.Sex = item.Sex;
                   userViewModel.TelPhone = item.TelPhone;
                   userViewModel.OfficePhone = item.OfficePhone;
                   userViewModel.Email = item.Email;
                   userViewModel.Enable = item.Enable;
                   userViewModel.Founder = item.Founder;
                   userViewModel.FoundTime = item.FoundTime;
                   userViewModel.ModifyPerson = item.ModifyPerson;
                   userViewModel.ModifyTime = item.ModifyTime;
                   userViewModels.Add(userViewModel);
               }
               total = userViewModels.Count();
               return userViewModels.Skip(pageInfo.PageIndex.Value).Take(pageInfo.PageSize.Value).ToList();
            }
        }
        /// <summary>
        /// 查询单个用户信息
        /// </summary>
        /// <param name="id">用户Id</param>
        /// <returns></returns>
        public UserViewModel GetSingleUserO(int id)
        {
            using (BackgroundDAL dal = new BackgroundDAL())
            {
                var query = dal.Users.First(user => user.Id == id);
                UserViewModel userViewModel = new UserViewModel();
                userViewModel.Id = query.Id;
                userViewModel.RealName = query.RealName;
                userViewModel.Account = query.Account;
                userViewModel.Password = query.Password;
                userViewModel.Sex = query.Sex;
                userViewModel.TelPhone = query.TelPhone;
                userViewModel.OfficePhone = query.OfficePhone;
                userViewModel.Email = query.Email;
                userViewModel.Enable = query.Enable;
                userViewModel.FoundTime = query.FoundTime;
                userViewModel.Founder = query.Founder;
                userViewModel.ModifyPerson = query.ModifyPerson;
                userViewModel.ModifyTime = query.ModifyTime;
                return userViewModel;
            }
        }
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <param name="str">角色选中Id</param>
        /// <returns></returns>
        public int AddUserO(Users user,string[] str)
        {
            using (BackgroundDAL dal = new BackgroundDAL())
            {
                List<Role> roles=new List<Role>();
                int id;
                foreach (var item in str)
                {
                    if (int.TryParse(item, out id))
                    {
                        var role = dal.Role.First(roleN => roleN.Id == id);
                        roles.Add(role);
                    }
                }
                user.Roles = roles;
                user.Password = Sha256(user.Password);
                dal.Users.Add(user);
                return dal.SaveChanges();
            }
        }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <param name="str">角色选中Id</param>
        /// <returns></returns>
        public int UpdateUser(Users user,string[] str)
        {
            using (BackgroundDAL dal = new BackgroundDAL())
            {
                List<Role> roles = new List<Role>();
                int id;
                foreach (var item in str)
                {
                    if (int.TryParse(item, out id))
                    {
                        var role = dal.Role.First(roleN => roleN.Id == id);
                        roles.Add(role);
                    }
                }
                var users = dal.Users.Single(useres => useres.Id == user.Id);
                users.Account = user.Account;
                users.RealName = user.RealName;
                users.Sex = user.Sex;
                users.TelPhone = user.TelPhone;
                users.OfficePhone = user.OfficePhone;
                users.Email = user.Email;
                users.Enable = user.Enable;
                users.FoundTime = user.FoundTime;
                users.Founder = user.Founder;
                users.ModifyPerson = user.ModifyPerson;
                users.ModifyTime = user.ModifyTime;
                dal.Database.ExecuteSqlCommand("delete from UsersRoles where Users_Id=" + user.Id);
                users.Roles = roles;
                return dal.SaveChanges();
            }
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        public int DeleteUserO(int id)
        {
            using (BackgroundDAL dal = new BackgroundDAL())
            {
                var user = dal.Users.First(users => users.Id == id);
                dal.Users.Remove(user);
                return dal.SaveChanges();
            }
        }
        /// <summary>
        /// 检查用户名是否重复
        /// </summary>
        /// <param name="account">用户名</param>
        /// <returns></returns>
        public bool CheckAccountO(string account)
        {
            using (BackgroundDAL dal = new BackgroundDAL())
            {
                var count = dal.Users.Count(user => user.Account == account);
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

        #endregion

        #region 数据字典相关操作

        #region 字典详细信息的操作

        /// <summary>
        /// 获取字典详细信息
        /// </summary>
        /// <param name="pageInfo">页面信息</param>
        /// <param name="total">数据条数</param>
        /// <param name="dicId">字典Id</param>
        /// <returns></returns>
        public List<DataDicDetailViewModel> GetDicDetailListO(PageInfo pageInfo, out int total, int dicId)
        {
            List<DataDicDetailViewModel> dataDicDetailViewModels = new List<DataDicDetailViewModel>();

            using (BackgroundDAL dal = new BackgroundDAL())
            {
                var query = dal.DataDicDetial.Join(dal.Users, detail=>detail.ModifyPerson,user=>user.Id,(detail,user)=> new{
                    detail.Id,
                    detail.DeChName,
                    detail.DeEnName,
                    detail.Sort,
                    detail.Enable,
                    detail.DId,
                    ModifyTime=detail.ModifyTime.ToString(),
                    detail.ModifyPerson,
                    detail.Description,
                    user.RealName
                }).Where(detail=>detail.DId==dicId);
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

            }
            total = dataDicDetailViewModels.Count();
            return dataDicDetailViewModels.Skip(pageInfo.PageIndex.Value).Take(pageInfo.PageSize.Value).ToList();
        }
        /// <summary>
        /// 获取单条字典详细信息
        /// </summary>
        /// <param name="detailId">字典详细信息Id</param>
        /// <returns></returns>
        public DataDicDetailViewModel GetSigleDicDetailO(int detailId)
        {
            using (BackgroundDAL dal = new BackgroundDAL())
            {
                var query = dal.DataDicDetial.Single(detail => detail.Id == detailId);
                DataDicDetailViewModel dataDicDetailViewModel = new DataDicDetailViewModel();
                dataDicDetailViewModel.Id = query.Id;
                dataDicDetailViewModel.DeEnName = query.DeEnName;
                dataDicDetailViewModel.DeChName = query.DeChName;
                dataDicDetailViewModel.Description = query.Description;
                dataDicDetailViewModel.Sort = query.Sort;
                dataDicDetailViewModel.Enable = query.Enable;
                dataDicDetailViewModel.Founder = query.Founder;
                dataDicDetailViewModel.FoundTime = query.FoundTime;
                dataDicDetailViewModel.ModifyTime = query.ModifyTime.ToString();
                dataDicDetailViewModel.ModifyPerson = query.ModifyPerson;
                return dataDicDetailViewModel;
            }
        }
        /// <summary>
        /// 新增单条字典详细信息
        /// </summary>
        /// <param name="detail">字典详细信息实体</param>
        /// <returns></returns>
        public int AddDicDetail(DataDicDetail detail)
        {
            using (BackgroundDAL dal = new BackgroundDAL())
            {
                dal.DataDicDetial.Add(detail);
                return dal.SaveChanges();
            }
        }
        /// <summary>
        /// 更新单条详细字典信息
        /// </summary>
        /// <param name="detail"></param>
        /// <returns></returns>
        public int UpdateDicdetail(DataDicDetail detail)
        {
            using (BackgroundDAL dal = new BackgroundDAL())
            {
                var dicDetail = dal.DataDicDetial.Single(details => details.Id == detail.Id);
                dicDetail.DeChName = detail.DeChName;
                dicDetail.DeEnName = detail.DeEnName;
                dicDetail.Sort = detail.Sort;
                dicDetail.Enable = detail.Enable;
                dicDetail.ModifyTime = detail.ModifyTime;
                dicDetail.ModifyPerson = detail.ModifyPerson;
                dicDetail.Description = detail.Description;
                return dal.SaveChanges();
            }
        }
        /// <summary>
        /// 检查字典详细中文名是否重复
        /// </summary>
        /// <param name="deChName">字典详细中文名</param>
        /// <returns></returns>
        public bool CheckDeChName(string deChName)
        {
            using (BackgroundDAL dal = new BackgroundDAL())
            {
                var count = dal.DataDicDetial.Count(detail => detail.DeChName == deChName);
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
        /// <summary>
        /// 检查字典详细的英文名称是否重复
        /// </summary>
        /// <param name="deEnName">字典详细英文名称</param>
        /// <returns></returns>
        public bool CheckDeEnName(string deEnName)
        {
            using (BackgroundDAL dal = new BackgroundDAL())
            {

                var count = dal.DataDicDetial.Count(detail => detail.DeEnName == deEnName);
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
        #endregion

        #region 字典信息操作
        /// <summary>
        /// 获取字典信息
        /// </summary>
        /// <param name="dicId">字典Id</param>
        /// <returns></returns>
        public List<DataDicViewModel> GetDicListO( PageInfo pageInfo ,out int total,int dicId)
        {
            using (BackgroundDAL dal = new BackgroundDAL())
            {
                List<DataDicViewModel> dataDicViewModels = new List<DataDicViewModel>();
                var query = dal.DataDic.Where(dic => dic.Id == dicId);
                foreach (var item in query)
                {
                    DataDicViewModel dataDicViewModel = new DataDicViewModel();
                    dataDicViewModel.Id = item.Id;
                    dataDicViewModel.DChName = item.DChName;
                    dataDicViewModel.DEnName = item.DEnName;
                    dataDicViewModel.Description = item.Description;
                    dataDicViewModel.DReadonly = item.DReadonly;
                    dataDicViewModels.Add(dataDicViewModel);
                }
                total = dataDicViewModels.Count();
                return dataDicViewModels.Skip(pageInfo.PageIndex.Value).Take(pageInfo.PageSize.Value).ToList();
            }
        }
        /// <summary>
        /// 获取单条字典信息
        /// </summary>
        /// <param name="detailId">字典Id</param>
        /// <returns></returns>
        public DataDicViewModel GetSigleDicO(int detailId)
        {
            using (BackgroundDAL dal = new BackgroundDAL())
            {
                var query = dal.DataDic.Single(detail => detail.Id == detailId);
                DataDicViewModel dataDic = new DataDicViewModel();
                dataDic.Id = query.Id;
                dataDic.DChName = query.DChName;
                dataDic.DEnName = query.DEnName;
                dataDic.DReadonly = query.DReadonly;
                dataDic.Description = query.Description;
                dataDic.Founder = query.Founder;
                dataDic.FoundTime = query.FoundTime;
                return dataDic;
            }
        }
        /// <summary>
        /// 新增数据字典
        /// </summary>
        /// <param name="dataDic">字典实体</param>
        /// <returns></returns>
        public int AddDataDicO(DataDic dataDic)
        {
            using (BackgroundDAL dal = new BackgroundDAL())
            {
                dal.DataDic.Add(dataDic);
                return dal.SaveChanges();
            }
        }
        /// <summary>
        /// 修改数据字典
        /// </summary>
        /// <param name="dataDic">字典实体</param>
        /// <returns></returns>
        public int UpdateDataDic(DataDic dataDic)
        {
            using (BackgroundDAL dal = new BackgroundDAL())
            {
                var dic = dal.DataDic.First(datadics => datadics.Id == dataDic.Id);
                dic.DChName = dataDic.DChName;
                dic.DEnName = dataDic.DEnName;
                dic.DReadonly = dataDic.DReadonly;
                dic.Description = dataDic.Description;
                dic.FoundTime = dataDic.FoundTime;
                dic.Founder = dataDic.Founder;
                return dal.SaveChanges();
            }
        }
        /// <summary>
        /// 检查字典中文名称是否重复
        /// </summary>
        /// <param name="dChName">字典中文名称</param>
        /// <returns></returns>
        public bool CheckDChName(string dChName)
        {
            using (BackgroundDAL dal = new BackgroundDAL())
            {
                var count = dal.DataDic.Count(dic => dic.DChName == dChName);
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
        /// <summary>
        ///检查字典英文名称是否重复
        /// </summary>
        /// <param name="dEnName">字典的英文名称</param>
        /// <returns></returns>
        public bool CheckDEnName(string dEnName)
        {
            using (BackgroundDAL dal = new BackgroundDAL())
            {
                var count = dal.DataDic.Count(dic => dic.DEnName == dEnName);
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
        public List<RoleViewModel> GetRoleListO(PageInfo pageInfo,out int total, string roleName)
        {
            using (BackgroundDAL dal = new BackgroundDAL())
            {
                List<RoleViewModel> roleViewModels=new List<RoleViewModel>();
                var query = dal.Role.ToList();
                if (!string.IsNullOrEmpty(roleName))
                {
                    query = query.Where(role => role.RName.Contains(roleName)).ToList();
                }
                if (pageInfo.SortDir == "asc")
                {
                    query = query.OrderBy(role => role.RName).ToList();
                }
                else
                {
                    query = query.OrderByDescending(role => role.RName).ToList();
                }
                foreach (var item in query)
                {
                    RoleViewModel roleViewModel=new RoleViewModel();
                    roleViewModel.Id = item.Id;
                    roleViewModel.RName  = item.RName;
                    roleViewModel.Description = item.Description;
                    roleViewModel.Code = item.Code;
                    roleViewModels.Add(roleViewModel);
                }
                total = roleViewModels.Count;
                return roleViewModels.Skip(pageInfo.PageIndex.Value).Take(pageInfo.PageSize.Value).ToList();
            }
        }
        /// <summary>
        /// 获取单条角色信息
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <returns></returns>
        public RoleViewModel GetSigleRoleO(int id)
        {
            using (BackgroundDAL dal = new BackgroundDAL())
            {
                RoleViewModel roleViewModel=new RoleViewModel();
                var role = dal.Role.Single(roles => roles.Id == id);
                    roleViewModel.Id = role.Id;
                    roleViewModel.RName = role.RName;
                    roleViewModel.Code = role.Code;
                    roleViewModel.Description = role.Description;
                    roleViewModel.Founder = role.Founder;
                    roleViewModel.FoundTime = role.FoundTime;
                    roleViewModel.ModifyPerson = role.ModifyPerson;
                    roleViewModel.ModifyTime = role.ModifyTime;
                return roleViewModel;
            }
        }
        /// <summary>
        /// 新增角色信息
        /// </summary>
        /// <param name="role">角色实体</param>
        /// <param name="strA">管理员角色Id集合</param>
        /// <param name="strM">模块角色Id集合</param>
        /// <returns></returns>
        public int AddRole(Role role,string[] strA,string[] strM)
        {
            using (BackgroundDAL dal = new BackgroundDAL())
            {
                List<Users> users = new List<Users>();
                int id;
                foreach (var item in strA)
                {
                    if (int.TryParse(item, out id))
                    {
                        var user = dal.Users.First(userN => userN.Id == id);
                        users.Add(user);
                    }
                }
                List<Module> modules = new List<Module>();
                int idM;
                foreach (var item in strM)
                {
                    if (int.TryParse(item, out idM))
                    {
                        var module = dal.Module.First(moduleN => moduleN.Id == idM);
                        modules.Add(module);
                    }
                }
                role.Userses = users;
                role.Modules = modules;
                dal.Role.Add(role);
                return dal.SaveChanges();
            }
        }
        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <param name="role">角色实体</param>
        /// <param name="strA">管理员角色Id集合</param>
        /// <param name="strM">模块角色Id集合</param>
        /// <returns></returns>
        public int UpdateRole(Role role,string[] strA,string[] strM)
        {
            using (BackgroundDAL dal = new BackgroundDAL())
            {
                List<Users> users = new List<Users>();
                int idU;
                foreach (var item in strA)
                {
                    if (int.TryParse(item, out idU))
                    {
                        var user = dal.Users.First(userN => userN.Id == idU);
                        users.Add(user);
                    }
                }
                List<Module> modules = new List<Module>();
                int idM;
                foreach (var item in strM)
                {
                    if (int.TryParse(item, out idM))
                    {
                        var module = dal.Module.First(moduleN => moduleN.Id == idM);
                        modules.Add(module);
                    }
                }
                var roleN = dal.Role.First(roles => roles.Id == role.Id);
                roleN.RName = role.RName;
                roleN.Code = role.Code;
                roleN.Description = role.Description;
                roleN.ModifyPerson = role.ModifyPerson;
                roleN.ModifyTime = role.ModifyTime;
                dal.Database.ExecuteSqlCommand("delete from RoleModules where Role_Id=" + role.Id);
                roleN.Modules = modules;
                dal.Database.ExecuteSqlCommand("delete from UsersRoles where Role_Id=" + role.Id);
                roleN.Userses = users;
                return dal.SaveChanges();
            }
        }
        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <returns></returns>
        public int DeleteRoleO(int id)
        {
            using (BackgroundDAL dal=new BackgroundDAL())
            {
                var role = dal.Role.First(roles => roles.Id == id);
                dal.Role.Remove(role);
                return dal.SaveChanges();
            }
        }
        /// <summary>
        /// 检查角色名是否重复
        /// </summary>
        /// <param name="roleName">角色名</param>
        /// <returns></returns>
        public bool CheckRoleName(string  roleName)
        {
            using (BackgroundDAL dal = new BackgroundDAL())
            {
                bool check = false;
                var count = dal.Role.Count(role => role.RName == roleName);
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

        #endregion

        #region 模块信息操作
        /// <summary>
        /// 获得模块信息列表
        /// </summary>
        /// <param name="pageInfo">页面信息</param>
        /// <param name="total">数据条数</param>
        /// <param name="mId">模块Id</param>
        /// <returns></returns>
        public List<ModuleViewModel> GetModuleListO(PageInfo pageInfo, out int total, int mId)
        {
            using (BackgroundDAL dal = new BackgroundDAL())
            {
                ModuleViewModel moduleViewModel=new ModuleViewModel();
                List<ModuleViewModel> moduleViewModels=new List<ModuleViewModel>();
                var query = dal.Module.Where(module => module.Id == mId);
                foreach (var item in query)
                {
                    moduleViewModel.Id = item.Id;
                    moduleViewModel.FId = item.FId;
                    moduleViewModel.MChName = item.MChName;
                    moduleViewModel.MEnName = item.MEnName;
                    moduleViewModel.MSort = item.MSort;
                    moduleViewModel.Display = item.Display;
                    moduleViewModel.Icon = item.Icon;
                    moduleViewModel.URL = item.URL;
                    moduleViewModel.Code = item.Code;
                    moduleViewModel.Description = item.Description;
                    moduleViewModel.Enable = item.Enable;
                    moduleViewModels.Add(moduleViewModel);
                }
                total = moduleViewModels.Count;
                return moduleViewModels.Skip(pageInfo.PageIndex.Value).Take(pageInfo.PageSize.Value).ToList();
            }
        }
        /// <summary>
        /// 获取单个模块信息
        /// </summary>
        /// <param name="id">模块Id</param>
        /// <returns></returns>
        public ModuleViewModel GetSingleModuleO(int id)
        {
            using (BackgroundDAL dal=new BackgroundDAL())
            {
                ModuleViewModel moduleViewModel=new ModuleViewModel();
                var query = dal.Module.First(module => module.Id == id);
                moduleViewModel.Id = query.Id;
                moduleViewModel.FId = query.FId;
                moduleViewModel.MChName = query.MChName;
                moduleViewModel.MEnName = query.MEnName;
                moduleViewModel.MSort = query.MSort;
                moduleViewModel.Display = query.Display;
                moduleViewModel.Icon = query.Icon;
                moduleViewModel.URL = query.URL;
                moduleViewModel.Code = query.Code;
                moduleViewModel.Description = query.Description;
                moduleViewModel.Enable = query.Enable;
                moduleViewModel.Founder = query.Founder;
                moduleViewModel.FoundTime = query.FoundTime;
                moduleViewModel.ModifyPerson = query.ModifyPerson;
                moduleViewModel.ModifyTime = query.ModifyTime;
                return moduleViewModel;
            }
        }
        /// <summary>
        /// 新增模块信息
        /// </summary>
        /// <param name="module">模块实体</param>
        /// <param name="str">角色选择Id</param>
        /// <returns></returns>
        public int AddModule(Module module,string[] str)
        {
            using (BackgroundDAL dal=new BackgroundDAL())
            {
                List<Role> roles = new List<Role>();
                int id;
                foreach (var item in str)
                {
                    if (int.TryParse(item, out id))
                    {
                        var role = dal.Role.First(roleN => roleN.Id == id);
                        roles.Add(role);
                    }
                }
                module.Roles = roles;
                dal.Module.Add(module);
                return dal.SaveChanges();
            }
        }
       
        /// <summary>
        /// 修改模块信息
        /// </summary>
        /// <param name="module">模块实体</param>
        /// <param name="str">角色选择Id</param>
        /// <returns></returns>
        public int UpdateModule(Module module,string[] str)
        {
            using (BackgroundDAL dal=new BackgroundDAL())
            {
                List<Role> roles = new List<Role>();
                int id;
                foreach (var item in str)
                {
                    if (int.TryParse(item, out id))
                    {
                        var role = dal.Role.First(roleN => roleN.Id == id);
                        roles.Add(role);
                    }
                }
                var moduleO = dal.Module.Single(modules => modules.Id == module.Id);
                moduleO.MChName = module.MChName;
                moduleO.MEnName = module.MEnName;
                moduleO.MSort = module.MSort;
                moduleO.Display = module.Display;
                moduleO.Icon = module.Icon;
                moduleO.URL = module.URL ;
                moduleO.Code  = module.Code ;
                moduleO.Description  = module.Description;
                moduleO.Enable = module.Enable;
                moduleO.Founder = module.Founder;
                moduleO.FoundTime = module.FoundTime;
                moduleO.ModifyPerson = module.ModifyPerson ;
                moduleO.ModifyTime = module.ModifyTime ;
                dal.Database.ExecuteSqlCommand("delete from RoleModules where Module_Id=" + module.Id);
                //moduleO.Roles = null;
                module.Roles = roles;
                moduleO.Roles = module.Roles;
                //moduleO.Roles = roles;
                return dal.SaveChanges();
            }
        }
        /// <summary>
        /// 检查模块的中文名称是否重复
        /// </summary>
        /// <param name="mChName">模块的中文名称</param>
        /// <returns></returns>
        public bool CheckMChName(string mChName)
        {
            using (BackgroundDAL dal=new BackgroundDAL())
            {
                var count = dal.Module.Count(module => module.MChName == mChName);
                var check =false;
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
        /// <summary>
        /// 检查模块的英文名称是否重复
        /// </summary>
        /// <param name="mEnName">模块的英文名称</param>
        /// <returns></returns>
        public bool CheckMEnName(string mEnName)
        {
            using (BackgroundDAL dal = new BackgroundDAL())
            {
                var count = dal.Module.Count(module => module.MEnName == mEnName);
                var check = false;
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
        #endregion

        #region 用户相关操作新
        /// <summary>
        /// 初始化一位用户
        /// </summary>
        public void InitializaUser()
        {
            BackgroundsDAL dal=new BackgroundsDAL();
            dal.InitializaUser();
        }
        /// <summary>
        /// 检查用户是否存在与数据库
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <returns></returns>
        public Users CheckUsers(UserViewModel user)
        {
            BackgroundsDAL dal = new BackgroundsDAL();
            Users users=new Users();
            users.Account = user.Account;
            users.Password = user.Password;
            return dal.CheckUsers(users);
        }
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="pageInfo">页面信息</param>
        /// <param name="total">数据总数</param>
        /// <returns></returns>
        public List<UserViewModel> GetUserList(PageInfo pageInfo, out int total)
        {
            BackgroundsDAL dal = new BackgroundsDAL();
            var userList = dal.GetUserList(pageInfo, out total);
            List<UserViewModel> userViewModels = new List<UserViewModel>();
            foreach (var item in userList)
            {
                UserViewModel userViewModel = new UserViewModel();
                userViewModel.Id = item.Id;
                userViewModel.RealName = item.RealName;
                userViewModel.Account = item.Account;
                userViewModel.Sex = item.Sex;
                userViewModel.TelPhone = item.TelPhone;
                userViewModel.OfficePhone = item.OfficePhone;
                userViewModel.Email = item.Email;
                userViewModel.Enable = item.Enable;
                userViewModel.Founder = item.Founder;
                userViewModel.FoundTime = item.FoundTime;
                userViewModel.ModifyPerson = item.ModifyPerson;
                userViewModel.ModifyTime = item.ModifyTime;
                userViewModels.Add(userViewModel);
            }
            return userViewModels;
        }
        /// <summary>
        /// 查询单个用户信息
        /// </summary>
        /// <param name="id">用户Id</param>
        /// <returns></returns>
        public UserViewModel GetSingleUser(int id)
        {
            BackgroundsDAL dal = new BackgroundsDAL();
            var user = dal.GetSingleUser(id);
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.Id = user.Id;
            userViewModel.RealName = user.RealName;
            userViewModel.Account = user.Account;
            userViewModel.Password = user.Password;
            userViewModel.Sex = user.Sex;
            userViewModel.TelPhone = user.TelPhone;
            userViewModel.OfficePhone = user.OfficePhone;
            userViewModel.Email = user.Email;
            userViewModel.Enable = user.Enable;
            userViewModel.FoundTime = user.FoundTime;
            userViewModel.Founder = user.Founder;
            userViewModel.ModifyPerson = user.ModifyPerson;
            userViewModel.ModifyTime = user.ModifyTime;
            return userViewModel;
        }
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <param name="str">角色选中Id</param>
        /// <returns></returns>
        public int AddUser(UserViewModel user, int[] listId)
        {
            BackgroundsDAL dal = new BackgroundsDAL();
            Users users=new Users();
            users.Id = user.Id;
            users.RealName = user.RealName;
            users.Account = user.Account;
            users.Password ="123456";
            users.Sex = user.Sex;
            users.TelPhone = user.TelPhone;
            users.OfficePhone = user.OfficePhone;
            users.Email = user.Email;
            users.Enable = user.Enable;
            users.Email = user.Email;
            users.Enable = user.Enable;
            users.FoundTime = DateTime.Now;
            users.ModifyTime = DateTime.Now;
            return dal.AddUser(users, listId);
        }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <param name="listId">角色选中Id</param>
        /// <returns></returns>
        public int UpdateUser(UserViewModel user, int[] listId)
        {
            BackgroundsDAL dal = new BackgroundsDAL();
            Users users = new Users();
            users.Id = user.Id;
            users.Account = user.Account;
            users.RealName = user.RealName;
            users.TelPhone = user.TelPhone;
            users.OfficePhone = user.OfficePhone;
            users.Email = user.Email;
            users.Enable = user.Enable;
            users.ModifyPerson = user.ModifyPerson;
            return dal.UpdateUser(users, listId);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        public int DeleteUser(int id)
        {
            BackgroundsDAL dal = new BackgroundsDAL();
            return dal.DeleteUser(id);
        }
        /// <summary>
        /// 检查用户名是否重复
        /// </summary>
        /// <param name="account">用户名</param>
        /// <param name="id">用户名Id</param>
        /// <returns></returns>
        public bool CheckAccount(string account,int id)
        {
            BackgroundsDAL dal = new BackgroundsDAL();
            return dal.CheckAccount(account,id);
        }
        #endregion

        #region 数据字典操作新

        #region 字典信息操作新
        /// <summary>
        /// 获取字典信息
        /// </summary>
        /// <param name="dicId">字典Id</param>
        /// <returns></returns>
        public List<DataDicViewModel> GetDicList(PageInfo pageInfo, out int total, int dicId)
        {
            BackgroundsDAL dal=new BackgroundsDAL();
            List<DataDicViewModel> dataDicViewModels = new List<DataDicViewModel>();
            var dataDic = dal.GetDicList(pageInfo, out total, dicId);
            foreach (var item in dataDic)
            {
                DataDicViewModel dataDicViewModel = new DataDicViewModel();
                dataDicViewModel.Id = item.Id;
                dataDicViewModel.DChName = item.DChName;
                dataDicViewModel.DEnName = item.DEnName;
                dataDicViewModel.Description = item.Description;
                dataDicViewModel.DReadonly = item.DReadonly;
                dataDicViewModels.Add(dataDicViewModel);
            }
            return dataDicViewModels;
        }
        /// <summary>
        /// 获取单条字典信息
        /// </summary>
        /// <param name="dicId">字典Id</param>
        /// <returns></returns>
        public DataDicViewModel GetSigleDic(int dicId)
        {
            BackgroundsDAL dal = new BackgroundsDAL();
            var dataDic = dal.GetSigleDic(dicId);
            DataDicViewModel dataDicN = new DataDicViewModel();
            dataDicN.Id = dataDic.Id;
            dataDicN.DChName = dataDic.DChName;
            dataDicN.DEnName = dataDic.DEnName;
            dataDicN.DReadonly = dataDic.DReadonly;
            dataDicN.Description = dataDic.Description;
            dataDicN.Founder = dataDic.Founder;
            dataDicN.FoundTime = dataDic.FoundTime;
            return dataDicN;
        }
        /// <summary>
        /// 新增数据字典
        /// </summary>
        /// <param name="dataDic">字典实体</param>
        /// <returns></returns>
        public int AddDataDic(DataDicViewModel dataDic)
        {
            BackgroundsDAL dal = new BackgroundsDAL();
            DataDic dataDicN = new DataDic();
            dataDicN.DChName = dataDic.DChName;
            dataDicN.DEnName = dataDic.DEnName;
            dataDicN.Description = dataDic.Description;
            dataDicN.DReadonly = dataDic.DReadonly;
            dataDicN.Founder = dataDic.Founder;
            dataDicN.FoundTime =DateTime.Now;
            return dal.AddDataDic(dataDicN);
        }
        /// <summary>
        /// 修改数据字典
        /// </summary>
        /// <param name="dataDic">字典实体</param>
        /// <returns></returns>
        public int UpdateDataDic(DataDicViewModel dataDic)
        {
            BackgroundsDAL dal = new BackgroundsDAL();
            DataDic dataDicN = new DataDic();
            dataDicN.Id = dataDic.Id;
            dataDicN.DChName = dataDic.DChName;
            dataDicN.DEnName = dataDic.DEnName;
            dataDicN.Description = dataDic.Description;
            dataDicN.DReadonly = dataDic.DReadonly;
            dataDicN.Founder = dataDic.Founder;
            dataDicN.FoundTime = DateTime.Now;
            return dal.UpdateDataDic(dataDicN);
        }
        /// <summary>
        /// 检查字典中文名称是否重复
        /// </summary>
        /// <param name="dChName">字典中文名称</param>
        /// <param name="id">字典中Id</param>
        /// <returns></returns>
        public bool CheckDChName(string dChName,int id)
        {
            BackgroundsDAL dal = new BackgroundsDAL();
            return dal.CheckDChName(dChName, id);
        }
        /// <summary>
        ///检查字典英文名称是否重复
        /// </summary>
        /// <param name="dEnName">字典的英文名称</param>
        ///  <param name="id">字典的Id</param>
        /// <returns></returns>
        public bool CheckDEnName(string dEnName, int id)
        {
            BackgroundsDAL dal = new BackgroundsDAL();
            return dal.CheckDChName(dEnName, id);
        }

        #endregion

        #region 字典详细操作
        /// <summary>
        /// 获取字典详细信息
        /// </summary>
        /// <param name="pageInfo">页面信息</param>
        /// <param name="total">数据条数</param>
        /// <param name="dicId">字典Id</param>
        /// <returns></returns>
        public List<DataDicDetailViewModel> GetDicDetailList(PageInfo pageInfo, out int total, int dicId)
        {
            BackgroundsDAL dal = new BackgroundsDAL();
            return dal.GetDicDetailList(pageInfo, out total, dicId);
        }
        /// <summary>
        /// 获取单条字典详细信息
        /// </summary>
        /// <param name="detailId">字典详细信息Id</param>
        /// <returns></returns>
        public DataDicDetailViewModel GetSigleDicDetail(int detailId)
        {
            BackgroundsDAL dal = new BackgroundsDAL();
            var detail = dal.GetSigleDicDetail(detailId);
            DataDicDetailViewModel dataDicDetailViewModel = new DataDicDetailViewModel();
            dataDicDetailViewModel.Id = detail.Id;
            dataDicDetailViewModel.DeEnName = detail.DeEnName;
            dataDicDetailViewModel.DeChName = detail.DeChName;
            dataDicDetailViewModel.Description = detail.Description;
            dataDicDetailViewModel.Sort = detail.Sort;
            dataDicDetailViewModel.Enable = detail.Enable;
            dataDicDetailViewModel.Founder = detail.Founder;
            dataDicDetailViewModel.FoundTime = detail.FoundTime;
            dataDicDetailViewModel.ModifyTime = detail.ModifyTime.ToString();
            dataDicDetailViewModel.ModifyPerson = detail.ModifyPerson;
            return dataDicDetailViewModel;
        }
        /// <summary>
        /// 新增单条字典详细信息
        /// </summary>
        /// <param name="detail">字典详细信息实体</param>
        /// <returns></returns>
        public int AddDicDetail(DataDicDetailViewModel detail)
        {
            BackgroundsDAL dal = new BackgroundsDAL();
            DataDicDetail details=new DataDicDetail();
            details.DeEnName = detail.DeEnName;
            details.DeChName = detail.DeChName;
            details.Description = detail.Description;
            details.Sort = detail.Sort;
            details.Enable = detail.Enable;
            details.Founder = detail.Founder;
            details.FoundTime = DateTime.Now;
            details.ModifyTime =DateTime.Now;
            details.ModifyPerson = detail.ModifyPerson;
            details.DId = detail.DId;
            return dal.AddDicDetail(details);
        }
        /// <summary>
        /// 更新单条详细字典信息
        /// </summary>
        /// <param name="detail">字典详细信息</param>
        /// <returns></returns>
        public int UpdateDicdetail(DataDicDetailViewModel detail)
        {
            BackgroundsDAL dal = new BackgroundsDAL();
            DataDicDetail details = new DataDicDetail();
            details.Id = detail.Id;
            details.DeChName = detail.DeChName;
            details.DeEnName = detail.DeEnName;
            details.Sort = detail.Sort;
            details.Enable = detail.Enable;
            details.ModifyTime = DateTime.Now;
            details.ModifyPerson = detail.ModifyPerson;
            details.Description = detail.Description;
            return dal.UpdateDicdetail(details);
        }
        /// <summary>
        /// 检查字典详细中文名是否重复
        /// </summary>
        /// <param name="deChName">字典详细中文名</param>
        /// <param name="id">字典详细Id</param>
        /// <returns></returns>
        public bool CheckDeChName(string deChName, int id)
        {
            BackgroundsDAL dal = new BackgroundsDAL();
            return dal.CheckDeChName(deChName, id);
        }
        /// <summary>
        /// 检查字典详细的英文名称是否重复
        /// </summary>
        /// <param name="deEnName">字典详细英文名称</param>
        /// <param name="id">字典详细Id</param>
        /// <returns></returns>
        public bool CheckDeEnName(string deEnName, int id)
        {
            BackgroundsDAL dal = new BackgroundsDAL();
            return dal.CheckDeEnName(deEnName, id);
        }

        #endregion
        #endregion

        #region 角色信息操作新

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="pageInfo">页面信息</param>
        /// <param name="total">数据条数</param>
        /// <param name="roleName">角色名</param>
        /// <returns></returns>
        public List<RoleViewModel> GetRoleList(PageInfo pageInfo, out int total, string roleName)
        {
            BackgroundsDAL dal=new BackgroundsDAL();
            var role = dal.GetRoleList(pageInfo, out total, roleName);
            List<RoleViewModel> roleViewModels = new List<RoleViewModel>();
            foreach (var item in role)
            {
                RoleViewModel roleViewModel = new RoleViewModel();
                roleViewModel.Id = item.Id;
                roleViewModel.RName = item.RName;
                roleViewModel.Description = item.Description;
                roleViewModel.Code = item.Code;
                roleViewModels.Add(roleViewModel);
            }
            return roleViewModels;
        }
        /// <summary>
        /// 获取单条角色信息
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <returns></returns>
        public RoleViewModel GetSigleRole(int id)
        {
            BackgroundsDAL dal = new BackgroundsDAL();
            var role = dal.GetSigleRole(id);
            RoleViewModel roleViewModel = new RoleViewModel();
            roleViewModel.Id = role.Id;
            roleViewModel.RName = role.RName;
            roleViewModel.Code = role.Code;
            roleViewModel.Description = role.Description;
            roleViewModel.Founder = role.Founder;
            roleViewModel.FoundTime = role.FoundTime;
            roleViewModel.ModifyPerson = role.ModifyPerson;
            roleViewModel.ModifyTime = role.ModifyTime;
            return roleViewModel;
        }
        /// <summary>
        /// 新增角色信息
        /// </summary>
        /// <param name="role">角色实体</param>
        /// <param name="listIdA">管理员角色Id集合</param>
        /// <param name="listIdM">模块角色Id集合</param>
        /// <returns></returns>
        public int AddRole(RoleViewModel role, int[] listIdA, int[] listIdM)
        {
            BackgroundsDAL dal = new BackgroundsDAL();
            Role roles=new Role();
            roles.RName = role.RName;
            roles.Code = role.Code;
            roles.Description = role.Description;
            roles.Founder = role.Founder;
            roles.FoundTime = DateTime.Now;
            roles.ModifyPerson = role.ModifyPerson;
            roles.ModifyTime = DateTime.Now;
            return dal.AddRole(roles, listIdA, listIdM);
        }
        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <param name="role">角色实体</param>
        /// <param name="listIdA">管理员角色Id集合</param>
        /// <param name="listIdM">模块角色Id集合</param>
        /// <returns></returns>
        public int UpdateRole(RoleViewModel role, int[] listIdA, int[] listIdM)
        {
            BackgroundsDAL dal = new BackgroundsDAL();
            Role roles = new Role();
            roles.Id = role.Id;
            roles.RName = role.RName;
            roles.Code = role.Code;
            roles.Description = role.Description;
            roles.ModifyPerson = role.ModifyPerson;
            roles.ModifyTime = DateTime.Now;
            return dal.UpdateRole(roles, listIdA, listIdM);
        }
        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <returns></returns>
        public int DeleteRole(int id)
        {
            BackgroundsDAL dal = new BackgroundsDAL();
            return dal.DeleteRole(id);
        }
        /// <summary>
        /// 检查角色名是否重复
        /// </summary>
        /// <param name="roleName">角色名</param>
        /// <param name="id">角色名Id</param>
        /// <returns></returns>
        public bool CheckRoleName(string roleName,int id)
        {
            BackgroundsDAL dal = new BackgroundsDAL();
            return dal.CheckRoleName(roleName, id);
        }

        #endregion

        #region 模块信息操作新

        /// <summary>
        /// 获得模块信息列表
        /// </summary>
        /// <param name="pageInfo">页面信息</param>
        /// <param name="total">数据条数</param>
        /// <param name="mId">模块Id</param>
        /// <returns></returns>
        public List<ModuleViewModel> GetModuleList(PageInfo pageInfo, out int total, int mId)
        {
            BackgroundsDAL dal=new BackgroundsDAL();
            var roleList = dal.GetModuleList(pageInfo, out total, mId);
            List<ModuleViewModel> moduleViewModels = new List<ModuleViewModel>();
            foreach (var item in roleList)
            {
                ModuleViewModel moduleViewModel = new ModuleViewModel();
                moduleViewModel.Id = item.Id;
                moduleViewModel.FId = item.FId;
                moduleViewModel.MChName = item.MChName;
                moduleViewModel.MEnName = item.MEnName;
                moduleViewModel.MSort = item.MSort;
                moduleViewModel.Display = item.Display;
                moduleViewModel.Icon = item.Icon;
                moduleViewModel.URL = item.URL;
                moduleViewModel.Code = item.Code;
                moduleViewModel.Description = item.Description;
                moduleViewModel.Enable = item.Enable;
                moduleViewModels.Add(moduleViewModel);
            }
            return moduleViewModels;
        }
        /// <summary>
        /// 获取单个模块信息
        /// </summary>
        /// <param name="id">模块Id</param>
        /// <returns></returns>
        public ModuleViewModel GetSingleModule(int id)
        {
            BackgroundsDAL dal = new BackgroundsDAL();
            var module = dal.GetSingleModule(id);
            ModuleViewModel moduleViewModel = new ModuleViewModel();
            moduleViewModel.Id = module.Id;
            moduleViewModel.FId = module.FId;
            moduleViewModel.MChName = module.MChName;
            moduleViewModel.MEnName = module.MEnName;
            moduleViewModel.MSort = module.MSort;
            moduleViewModel.Display = module.Display;
            moduleViewModel.Icon = module.Icon;
            moduleViewModel.URL = module.URL;
            moduleViewModel.Code = module.Code;
            moduleViewModel.Description = module.Description;
            moduleViewModel.Enable = module.Enable;
            moduleViewModel.Founder = module.Founder;
            moduleViewModel.FoundTime = module.FoundTime;
            moduleViewModel.ModifyPerson = module.ModifyPerson;
            moduleViewModel.ModifyTime = module.ModifyTime;
            return moduleViewModel;
        }

        /// <summary>
        /// 新增模块信息
        /// </summary>
        /// <param name="module">模块实体</param>
        /// <param name="listId">角色选择Id</param>
        /// <returns></returns>
        public int AddModule(ModuleViewModel module, int[] listId)
        {
            BackgroundsDAL dal = new BackgroundsDAL();
            Module modules=new Module();
            modules.FId = module.FId;
            modules.MChName = module.MChName;
            modules.MEnName = module.MEnName;
            modules.MSort = module.MSort;
            modules.Display = module.Display;
            modules.Icon = module.Icon;
            modules.URL = module.URL;
            modules.Code = module.Code;
            modules.Description = module.Description;
            modules.Enable = module.Enable;
            modules.Founder = module.Founder;
            modules.FoundTime = DateTime.Now;
            modules.ModifyPerson = module.ModifyPerson;
            modules.ModifyTime =DateTime.Now;
            return dal.AddModule(modules,listId);
        }
        /// <summary>
        /// 修改模块信息
        /// </summary>
        /// <param name="module">模块实体</param>
        /// <param name="listId">角色选择Id</param>
        /// <returns></returns>
        public int UpdateModule(ModuleViewModel module, int[] listId)
        {
            BackgroundsDAL dal = new BackgroundsDAL();
            Module modules=new Module();
            modules.Id = module.Id;
            modules.MChName = module.MChName;
            modules.MEnName = module.MEnName;
            modules.MSort = module.MSort;
            modules.Display = module.Display;
            modules.Icon = module.Icon;
            modules.URL = module.URL;
            modules.Code = module.Code;
            modules.Description = module.Description;
            modules.Enable = module.Enable;
            modules.ModifyPerson = module.ModifyPerson;
            modules.ModifyTime = DateTime.Now;
            return dal.UpdateModule(modules, listId);
        }
        /// <summary>
        /// 检查模块的中文名称是否重复
        /// </summary>
        /// <param name="mChName">模块的中文名称</param>
        /// <param name="id">模块的Id</param>
        /// <returns></returns>
        public bool CheckMChName(string mChName, int id)
        {
            BackgroundsDAL dal = new BackgroundsDAL();
            return dal.CheckMChName(mChName, id);
        }
        /// <summary>
        /// 检查模块的英文名称是否重复
        /// </summary>
        /// <param name="mEnName">模块的英文名称</param>
        /// <param name="id">模块的Id</param>
        /// <returns></returns>
        public bool CheckMEnName(string mEnName, int id)
        {
            BackgroundsDAL dal = new BackgroundsDAL();
            return dal.CheckMEnName(mEnName, id);
        }

        #endregion

    }

}

