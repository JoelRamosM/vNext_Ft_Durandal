using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using vNext_Durandal.Business.Intefaces.BO;
using vNext_Durandal.Business.Models;
using vNext_Durandal.Commom.Exceptions;
using vNext_Durandal.Commom.Interfaces.Repository;

namespace vNext_Durandal.Business.BO.Commom
{
    public abstract class MovimentacaoBO : IMovimentacaoBO
    {
        private readonly IRepository<Movimentacao> _repository;

        protected List<Action<Movimentacao>> _validacoes = new List<Action<Movimentacao>>();

        protected MovimentacaoBO(IRepository<Movimentacao> repository)
        {
            _repository = repository;
            _validacoes.Add(ValidaValorNegativo);
        }

        public virtual Movimentacao New(Movimentacao movimentacao)
        {
            Valida(movimentacao);
            return _repository.Include(movimentacao);
        }

        public virtual Movimentacao Update(Movimentacao movimentacao)
        {
            Valida(movimentacao);
            return _repository.Update(movimentacao);
        }

        public virtual Movimentacao Find(long id) => _repository.Find(id);

        public virtual IQueryable<Movimentacao> Data(Expression<Func<Movimentacao, bool>> filter = null)
            => filter == null ? _repository.List() : _repository.Filter(filter);

        public virtual void Delete(long id) => _repository.Delete(id);

        public virtual void Valida(Movimentacao movimentacao) => _validacoes.ForEach(v => v(movimentacao));

        private static void ValidaValorNegativo(Movimentacao movimentacao)
        {
            if (movimentacao.Valor < 0)
                throw new ValorNegativoException("Movimentação aceita somente valores positivos. Para diferenciar as naturezas insira uma receita ou despesa.");
        }
    }
}
