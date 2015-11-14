using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting.Startup;
using Microsoft.AspNet.Routing;

namespace vNext_Durandal.Web.Configuration
{
    public static class MvcRouteConfig
    {
        public static void Configure(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute(
                name: "Default",
                template: "{controller}/{action}/{id?}",
                defaults: new { controller = "Home", action = "Index" });

            routeBuilder.MapRoute("Views_Durandal",
                "views/{controller}/{action}.html");
        }
    }
}
