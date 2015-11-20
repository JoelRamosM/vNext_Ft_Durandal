using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using vNextDurandal.Business.Intefaces.BO;
using vNextDurandal.Business.Models;
using vNextDurandal.Commom.Exceptions;
using vNextDurandal.Commom.Interfaces.Repository;
using vNextDurandal.Commom.Model;

namespace vNextDurandal.Business.BO.Commom
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

        public void Delete(List<long> ids) => _repository.DeleteRange(ids);


        public virtual IQueryable<Movimentacao> Data(Expression<Func<Movimentacao, bool>> filter = null)
            => filter == null ? _repository.List() : _repository.Filter(filter);

        public virtual void Delete(long id) => _repository.Delete(id);

        public virtual void Valida(Movimentacao movimentacao) => _validacoes.ForEach(v => v(movimentacao));

        private static void ValidaValorNegativo(Movimentacao movimentacao)
        {
            if (movimentacao.Valor < 0)
                throw new ValorNegativoException(new ErrorMessage(nameof(movimentacao.Valor), "Movimentação aceita somente valores positivos. Para diferenciar as naturezas insira uma receita ou despesa."));
        }
    }
}
