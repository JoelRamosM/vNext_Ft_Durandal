using vNext_Durandal.Business.BO.Commom;
using vNext_Durandal.Business.Intefaces.BO;
using vNext_Durandal.Business.Intefaces.Repository;

namespace vNext_Durandal.Business.BO
{
    public class ReceitaBO : MovimentacaoBO, IReceitaBO
    {
        public ReceitaBO(IReceitaRepository receitaRepository) : base(receitaRepository) { }
    }
}
