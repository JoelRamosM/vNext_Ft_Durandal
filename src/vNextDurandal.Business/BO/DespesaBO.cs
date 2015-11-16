using vNextDurandal.Business.BO.Commom;
using vNextDurandal.Business.Intefaces.BO;
using vNextDurandal.Business.Intefaces.Repository;

namespace vNextDurandal.Business.BO
{
    public class DespesaBO : MovimentacaoBO, IDespesaBO
    {
        public DespesaBO(IDespesaRepository repository) : base(repository) { }
    }
}
