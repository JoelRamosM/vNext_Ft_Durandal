using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using vNextDurandal.Business.Intefaces.BO;
using vNextDurandal.Business.Models;
using vNext_Durandal.Business.Enums;
using vNext_Durandal.Web.ViewModels;

namespace vNext_Durandal.Web.Controllers
{
    public class ReceitasController : Controller
    {
        private readonly IReceitaBO receitaBO;

        public ReceitasController(IReceitaBO receitaBO)
        {
            this.receitaBO = receitaBO;
        }

        public IActionResult Receitas()
        {
            return View();
        }

        public IActionResult Receita()
        {
            return View();
        }

        [HttpGet("api/receitas")]
        public JsonResult Get([FromQuery]GridRequest request)
        {
            //var x = Enumerable.Range(0, 100).Select(i => new Movimentacao
            //{
            //    Tipo = (TipoMovimentacao)1,
            //    Observacao = i.ToString(),
            //    Valor = i,
            //    Id = i
            //}).AsQueryable();

            return new JsonResult(request.ToResult(receitaBO.Data()));
        }

        [HttpGet("api/receitas/{id}")]
        public JsonResult Get(long id)
        {
            return new JsonResult(new { });
        }

        [HttpDelete("api/receitas")]
        public JsonResult Delete([FromForm]List<long> ids)
        {
            ids.ForEach(id => receitaBO.Delete(id));
            return new JsonResult(new { });
        }
        [HttpPost("api/receitas")]
        public JsonResult Post()
        {
            return new JsonResult(new { });
        }

        [HttpPut("api/receitas")]
        public JsonResult Put()
        {
            return new JsonResult(new { });
        }


    }
}
