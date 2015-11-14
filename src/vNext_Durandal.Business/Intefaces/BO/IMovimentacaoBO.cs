using System;
using System.Linq;
using System.Linq.Expressions;
using vNext_Durandal.Business.Models;

namespace vNext_Durandal.Business.Intefaces.BO
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