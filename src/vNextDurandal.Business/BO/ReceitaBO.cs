using vNextDurandal.Business.BO.Commom;
using vNextDurandal.Business.Intefaces.BO;
using vNextDurandal.Business.Intefaces.Repository;

namespace vNextDurandal.Business.BO
{
    public class ReceitaBO : MovimentacaoBO, IReceitaBO
    {
        public ReceitaBO(IReceitaRepository receitaRepository) : base(receitaRepository) { }
    }
}
