using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Data.Entity;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using vNextDurandal.Business.BO;
using vNextDurandal.Business.Intefaces.BO;
using vNextDurandal.Business.Intefaces.Repository;
using vNextDurandal.Commom.Interfaces.UoW;
using vNextDurandal.Data.Configuration;
using vNextDurandal.Data.Repositories;
using vNext_Durandal.Web.Configuration;

namespace vNext_Durandal.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
            _configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<VNextContext>(option =>
                {
                    var connectionString = _configuration["ConnectionString"];
                    option.UseSqlServer(connectionString);
                });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IReceitaRepository, ReceitaRepository>();
            services.AddScoped<IDespesaRepository, DespesaRepository>();
            services.AddScoped<IReceitaBO, ReceitaBO>();
            services.AddScoped<IDespesaBO, DespesaBO>();

        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseMvc(MvcRouteConfig.Configure);
        }
    }
}
