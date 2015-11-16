using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Data.Entity.Metadata.Internal;
using vNextDurandal.Commom.Abstract;

namespace vNextDurandal.Data.Configuration.EntitiesConfiguration.Commom
{
    public static class EntityBaseMap<TEntity> where TEntity : EntityBase
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TEntity>().HasKey(e => e.Id);
        }
    }
}
