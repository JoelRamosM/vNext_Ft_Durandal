using Ninject.MockingKernel.NSubstitute;

namespace VNext_Durandal.Test.Commom
{
    public abstract class TestBase
    {
        protected NSubstituteMockingKernel _kernel;

        protected TestBase()
        {
            _kernel = new NSubstituteMockingKernel();
        }
    }
}
