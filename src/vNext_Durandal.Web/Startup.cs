using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using vNext_Durandal.Web.Configuration;

namespace vNext_Durandal.Web
{
    public class Startup
    {
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseMvc(MvcRouteConfig.Configure);

        }
    }
}
