using Microsoft.AspNet.Mvc;
using vNextDurandal.Business.Intefaces.BO;

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
        public JsonResult Get()
        {
            return new JsonResult(new { });
        }

        [HttpGet("api/receitas/{id}")]
        public JsonResult Get(long id)
        {
            return new JsonResult(new { });
        }

        [HttpPost("api/receitas/novo")]
        public JsonResult Post()
        {
            return new JsonResult(new { });
        }

        [HttpPut("api/receitas/alterar")]
        public JsonResult Put()
        {
            return new JsonResult(new { });
        }


    }
}
