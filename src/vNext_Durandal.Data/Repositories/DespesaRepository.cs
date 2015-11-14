using System;
using System.Linq.Expressions;
using vNext_Durandal.Business.Enums;
using vNext_Durandal.Business.Intefaces.Repository;
using vNext_Durandal.Business.Models;
using vNext_Durandal.Commom.Interfaces.UoW;
using vNext_Durandal.Data.Repositories.Commom;

namespace vNext_Durandal.Data.Repositories
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
