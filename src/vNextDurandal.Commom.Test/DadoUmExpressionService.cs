using System.Linq;
using System.Linq.Expressions;
using vNextDurandal.Commom.Services;
using Xunit;

namespace vNextDurandal.Commom.Test
{
    public class DadoUmExpressionService
    {


        [Fact]
        public void AOChamarFilterDeExpressionRetornaExpressionBodyComTodosAsPropriedadesDoTipo()
        {
            var x2 = Enumerable.Range(0, 10).Select(i => new Teste { S1 = i.ToString() });
            var teste = x2.Where(ExpressionService.Filter<Teste>("1").Compile());
            Assert.Equal(1, teste.Count());
        }
    }

    public class Teste
    {
        public string S { get; set; }
        public string S1 { get; set; }
    }
}
