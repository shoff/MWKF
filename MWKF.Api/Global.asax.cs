namespace MWKF.Api
{
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Dispatcher;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using MWKF.Api.Services;

    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {

            ContainerConfig.RegisterComponents();
            GlobalConfiguration.Configure(WebApiConfig.Register);

            IHttpControllerActivator httpControllerActivator = Ioc.Instance.Resolve<IHttpControllerActivator>();
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), httpControllerActivator);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
