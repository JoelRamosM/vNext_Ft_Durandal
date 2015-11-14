using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace vNext_Durandal.Data.Configuration
{
    public class VNextContext : DbContext
    {
        public VNextContext() : base("VNextConn") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(typeof(VNextContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
