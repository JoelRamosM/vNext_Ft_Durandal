using Ninject;
using NUnit.Framework;
using vNext_Durandal.Data.Configuration;
using VNext_Durandal.Test.Commom;

namespace vNext_Durandal.Data.Test
{
    public class DadoUmUoW : TestBase
    {
        private UnitOfWork _sut;

        [SetUp]
        public void Setup()
        {
            _kernel.Reset();
            _sut = _kernel.Get<UnitOfWork>();
        }

        [Test]
        public void TransactionEhDeUoWEhMesmaQueDoContexto()
        {
            var transaction = _sut.Context.Database.CurrentTransaction;
            Assert.AreEqual(transaction, _sut.Transaction);
        }
        [Test]
        public void AoCommitarTransactionCurrentTransactionDeContextoFicaNUll()
        {
            Assert.IsNotNull(_sut.Context.Database.CurrentTransaction);
            _sut.Save();
            Assert.IsNull(_sut.Context.Database.CurrentTransaction);
        }
        [Test]
        public void AposRealizarRollBackTransactionDoContextoEhFechadaECancelada()
        {
            Assert.IsNotNull(_sut.Context.Database.CurrentTransaction);
            _sut.Rollback();
            Assert.IsNull(_sut.Context.Database.CurrentTransaction);
        }

        [Test]
        public void PossoChamaSaveChanges() => _sut.SaveChanges();
    }
}
