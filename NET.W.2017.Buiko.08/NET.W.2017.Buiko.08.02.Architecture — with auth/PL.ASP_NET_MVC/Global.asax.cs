using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject;
using Ninject.Web.Mvc;
using PL.ASP_NET_MVC.Util;

namespace PL.ASP_NET_MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var registrations = new NinjectRegistrations();
            var kernel = new StandardKernel(registrations);
            kernel.Unbind<ModelValidatorProvider>();
            System.Web.Mvc.DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));         
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            if (exception.GetType() == typeof(HttpException))
            {
                if (exception.Message.Contains("not found"))
                {
                    Server.Transfer("~/404.html");
                }
            }
            else
            {
                Server.Transfer("~/GenericError.html");
            }

            Server.ClearError();
        }
    }
}
