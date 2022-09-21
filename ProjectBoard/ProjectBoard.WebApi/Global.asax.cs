using Autofac;
using Autofac.Integration.WebApi;
using ProjectBoard.Model;
using ProjectBoard.Repository;
using ProjectBoard.Service;
using ProjectBoard.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ProjectBoard.WebApi
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(ContainerConfig.Configure());
        }
    }
    public class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<DiModel>();
            builder.RegisterModule<DiService>();
            builder.RegisterModule<DiRepository>();
            builder.RegisterModule<DiAutoMapper>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            return builder.Build();
        }
    }
}
