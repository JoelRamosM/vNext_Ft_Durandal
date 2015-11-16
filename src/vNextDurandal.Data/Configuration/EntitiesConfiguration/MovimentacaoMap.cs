using Microsoft.Data.Entity;
using vNextDurandal.Business.Models;
using vNextDurandal.Data.Configuration.EntitiesConfiguration.Commom;

namespace vNextDurandal.Data.Configuration.EntitiesConfiguration
{
    public class MovimentacaoMap : IEntityTypeConfiguration
    {
        public void Configure(ModelBuilder modelBuilder) => EntityBaseMap<Movimentacao>.Configure(modelBuilder);
    }
}
