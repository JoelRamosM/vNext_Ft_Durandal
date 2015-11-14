using System.Data.Entity.ModelConfiguration;
using vNext_Durandal.Commom.Abstract;

namespace vNext_Durandal.Data.Configuration.EntitiesConfiguration.Commom
{
    public abstract class EntityBaseMap<TEntity> : EntityTypeConfiguration<TEntity> where TEntity : EntityBase
    {
        protected EntityBaseMap()
        {
            HasKey(e => e.Id);
        }
    }
}
