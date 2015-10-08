namespace MWKF.Api
{
    using System;
    using System.Data.Entity;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Dispatcher;
    using System.Web.Mvc;
    using System.Web.Routing;
    using MWKF.Api.Data;
    using MWKF.Api.Services;

    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            // remove X-AspNetMvc-Version, no need to tell the hackers our framework
            MvcHandler.DisableMvcResponseHeader = true;

            ContainerConfig.RegisterComponents();
            GlobalConfiguration.Configure(WebApiConfig.Register);

            IHttpControllerActivator httpControllerActivator = Ioc.Instance.Resolve<IHttpControllerActivator>();
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), httpControllerActivator);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Database.SetInitializer<DataContext>(new EntityContextInitializer());
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // remove Server header, no need to tell the hackers our server
            var application = sender as HttpApplication;
            application?.Context?.Response.Headers.Remove("Server");
        }
    }
}
