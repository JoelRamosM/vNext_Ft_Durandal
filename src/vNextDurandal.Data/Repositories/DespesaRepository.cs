using System;
using System.Linq.Expressions;
using vNextDurandal.Business.Intefaces.Repository;
using vNextDurandal.Business.Models;
using vNextDurandal.Commom.Interfaces.UoW;
using vNextDurandal.Data.Repositories.Commom;
using vNext_Durandal.Business.Enums;

namespace vNextDurandal.Data.Repositories
{
    public class DespesaRepository : Repository<Movimentacao>, IDespesaRepository
    {
        public override Expression<Func<Movimentacao, bool>> DefaultFilter => (d) => d.Tipo == TipoMovimentacao.Despesa;

        public DespesaRepository(IUnitOfWork uow) : base(uow) { }

        public override void SetDefaultValues(Movimentacao entity)
        {
            entity.Tipo = TipoMovimentacao.Despesa;
            base.SetDefaultValues(entity);
        }
    }
}
