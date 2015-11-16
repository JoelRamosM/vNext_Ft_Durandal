using System;
using System.Linq.Expressions;
using vNextDurandal.Business.Intefaces.Repository;
using vNextDurandal.Business.Models;
using vNextDurandal.Commom.Interfaces.UoW;
using vNextDurandal.Data.Repositories.Commom;
using vNext_Durandal.Business.Enums;

namespace vNextDurandal.Data.Repositories
{
    public class ReceitaRepository : Repository<Movimentacao>, IReceitaRepository
    {
        public override Expression<Func<Movimentacao, bool>> DefaultFilter => (r) => r.Tipo == TipoMovimentacao.Receita;

        public ReceitaRepository(IUnitOfWork uow) : base(uow) { }

        public override void SetDefaultValues(Movimentacao entity)
        {
            entity.Tipo = TipoMovimentacao.Receita;
            base.SetDefaultValues(entity);
        }
    }
}
