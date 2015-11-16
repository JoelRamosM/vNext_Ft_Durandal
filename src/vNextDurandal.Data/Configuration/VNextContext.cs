using System.Collections.Generic;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Data.Entity;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using vNextDurandal.Data.Configuration.EntitiesConfiguration;
using vNextDurandal.Data.Configuration.EntitiesConfiguration.Commom;

namespace vNextDurandal.Data.Configuration
{
    public class VNextContext : DbContext
    {
        private readonly string connStr;
        private readonly List<IEntityTypeConfiguration> entityConfigurations = new List<IEntityTypeConfiguration>()
        {
            new MovimentacaoMap()
        };
        public VNextContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            entityConfigurations.ForEach(c => c.Configure(modelBuilder));
            base.OnModelCreating(modelBuilder);
        }
    }
    //MIGUE Para Criação de migrations
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(appEnv.ApplicationBasePath)
                .AddJsonFile("../vNext_Durandal.Web/config.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<VNextContext>(options =>
                {
                    options.UseSqlServer(Configuration["ConnectionString"]);
                });
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
        }
    }
}
