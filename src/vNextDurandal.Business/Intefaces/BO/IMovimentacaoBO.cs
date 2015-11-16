using System;
using System.Linq;
using System.Linq.Expressions;
using vNextDurandal.Business.Models;

namespace vNextDurandal.Business.Intefaces.BO
{
    public interface IMovimentacaoBO
    {
        Movimentacao New(Movimentacao movimentacao);

        Movimentacao Update(Movimentacao movimentacao);

        Movimentacao Find(long id);

        void Delete(long id);

        IQueryable<Movimentacao> Data(Expression<Func<Movimentacao, bool>> filter = null);
    }
}