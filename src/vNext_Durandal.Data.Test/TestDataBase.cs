using NUnit.Framework;
using vNext_Durandal.Commom.Interfaces.UoW;
using vNext_Durandal.Data.Configuration;
using VNext_Durandal.Test.Commom;

namespace vNext_Durandal.Data.Test
{
    public class TestDataBase : TestBase
    {
        protected UnitOfWork _uow;

        [SetUp]
        public void SetUpBase()
        {
            _uow = new UnitOfWork();
            _kernel.Unbind<IUnitOfWork>();
            _kernel.Bind<IUnitOfWork>().ToConstant(_uow);
        }

        [TearDown]
        public void CleanUp()
        {
            if (_uow.Transaction != null)
                _uow.Rollback();
            _uow.Dispose();
        }
    }
}