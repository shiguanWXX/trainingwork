using NewsManage.Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace NewsManage
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // 当模型改变与数据库中的表不一致时它会自动更新数据库中的表与模型保持一致（缺点是会丢失之前数据库中的数据）
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<NewsDAL>());
        }

    }
}
