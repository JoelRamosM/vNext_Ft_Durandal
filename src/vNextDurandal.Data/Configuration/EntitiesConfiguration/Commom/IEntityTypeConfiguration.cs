using Microsoft.Data.Entity;

namespace vNextDurandal.Data.Configuration.EntitiesConfiguration.Commom
{
    public interface IEntityTypeConfiguration
    {
        void Configure(ModelBuilder modelBuilder);
    }
}
