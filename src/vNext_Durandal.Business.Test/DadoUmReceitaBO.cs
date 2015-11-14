using System;
using System.Linq.Expressions;
using Ninject;
using Ninject.MockingKernel.NSubstitute;
using NSubstitute;
using NUnit.Framework;
using vNext_Durandal.Business.BO;
using vNext_Durandal.Business.Enums;
using vNext_Durandal.Business.Intefaces.Repository;
using vNext_Durandal.Business.Models;
using vNext_Durandal.Commom.Exceptions;
using VNext_Durandal.Test.Commom;

namespace vNext_Durandal.Business.Test
{
    [TestFixture]
    public class DadoUmReceitaBO : TestBase
    {
        private ReceitaBO _sut;
        private IReceitaRepository _repository;

        [SetUp]
        public void Setup()
        {
            _kernel = new NSubstituteMockingKernel();
            _repository = _kernel.Get<IReceitaRepository>();
            _sut = _kernel.Get<ReceitaBO>();
        }

        [Test]
        public void AoChamarNewMetodoIncludeDeRepositoryEhChamado()
        {
            _sut.New(new Movimentacao());
            _repository.Received().Include(Arg.Any<Movimentacao>());
        }

        [TestCase(-1)]
        [TestCase(-1.39)]
        [TestCase(-0.09)]
        public void NaoPossoIncluirNovaReceitaComValorNegativo(double valor)
            => Assert.Throws<ValorNegativoException>(() => _sut.New(new Movimentacao { Valor = valor }));

        [Test]
        public void AoChamarUpdateMetodoUpdateDeRepositoryEhChamado()
        {
            _sut.Update(new Movimentacao());
            _repository.Received().Update(Arg.Any<Movimentacao>());
        }

        [TestCase(-1)]
        [TestCase(-1.39)]
        [TestCase(-0.09)]
        public void NaoPossoAlterarUmaReceitaComValorNegativo(double valor)
            => Assert.Throws<ValorNegativoException>(() => _sut.Update(new Movimentacao { Valor = valor }));

        [Test]
        public void AoChamarFindFindDeRepositoryEhChamado()
        {
            _sut.Find(1);
            _repository.Received().Find(1);
        }

        [Test]
        public void AoChamarDeleteMetodoDeleteDeRepositoryEhChamado()
        {
            _sut.Delete(1);
            _repository.Received().Delete(1);
        }

        [Test]
        public void AoChamarDataSemParametroFiltroMetodoListDoRepositorioEhChamado()
        {
            _sut.Data();
            _repository.Received().List();
        }
        [Test]
        public void AoChamarDataComPArametroFiltroMetodoFilterDoRepositorioEhChamado()
        {
            _sut.Data(m => m.Tipo == TipoMovimentacao.Receita);
            _repository.Received().Filter(Arg.Any<Expression<Func<Movimentacao, bool>>>());
        }

    }
}
