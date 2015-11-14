using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Ninject;
using NUnit.Framework;
using vNext_Durandal.Business.Enums;
using vNext_Durandal.Business.Models;
using vNext_Durandal.Data.Repositories;

namespace vNext_Durandal.Data.Test
{
    [TestFixture]
    public class DadoUmReceitaRepository : TestDataBase
    {
        private ReceitaRepository _sut;

        [SetUp]
        public void Setup()
        {
            _kernel.Reset();
            SetUpBase();
            _sut = _kernel.Get<ReceitaRepository>();
        }

        [Test]
        public void AoIncluirMovimentacaoIncluiComTipoReceita()
        {
            var movimentacao = new Movimentacao { Data = DateTime.Now, Observacao = "Teste", Valor = 10.00 };
            _sut.Include(movimentacao);
            Assert.AreEqual(TipoMovimentacao.Receita, movimentacao.Tipo);
        }

        [Test]
        public void PossoDeletarMovimentacao()
        {
            var movimentacao = new Movimentacao { Data = DateTime.Now, Observacao = "Teste", Valor = 10.00 };
            _sut.Include(movimentacao);
            Assert.IsTrue(_uow.Context.Set<Movimentacao>().ToList().Count == 1);
            _sut.Delete(movimentacao.Id);
            Assert.IsTrue(_uow.Context.Set<Movimentacao>().ToList().Count == 0);
        }

        [Test]
        public void AoRecuperarListRetornaSomenteMovimentacoesDeReceita()
        {
            var receita = new Movimentacao { Data = DateTime.Now, Observacao = "Receita", Valor = 10.00, Tipo = TipoMovimentacao.Receita };
            var despesa = new Movimentacao { Data = DateTime.Now, Observacao = "Despesa", Valor = 10.00, Tipo = TipoMovimentacao.Despesa };
            _uow.Context.Set<Movimentacao>().AddOrUpdate(receita);
            _uow.Context.Set<Movimentacao>().AddOrUpdate(despesa);
            _uow.SaveChanges();
            var receitas = _sut.List();
            Assert.AreEqual(1, receitas.Count());
        }

        [Test]
        public void PossoAlterarUmaDadaReceita()
        {
            var expectedObservacao = "alterado";
            var movimentacao = new Movimentacao { Data = DateTime.Now, Observacao = "Teste", Valor = 10.00 };
            movimentacao = _sut.Include(movimentacao);
            movimentacao.Observacao = expectedObservacao;
            _sut.Update(movimentacao);
            var assertMovimentacoes = _uow.Context.Set<Movimentacao>().Find(movimentacao.Id);
            Assert.AreEqual(expectedObservacao, assertMovimentacoes.Observacao);
            Assert.IsTrue(_uow.Context.Set<Movimentacao>().ToList().Count == 1);
        }
    }
}
