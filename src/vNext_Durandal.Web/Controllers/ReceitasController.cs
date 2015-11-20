using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using vNextDurandal.Business.Intefaces.BO;
using vNextDurandal.Business.Models;
using vNextDurandal.Commom.Exceptions;
using vNext_Durandal.Web.ViewModels;


namespace vNext_Durandal.Web.Controllers
{
    public class ReceitasController : Controller
    {
        private readonly IReceitaBO receitaBO;

        public ReceitasController(IReceitaBO receitaBO)
        {
            this.receitaBO = receitaBO;
            Mapper.CreateMap<ReceitaViewModel, Movimentacao>();
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
        public IActionResult Get(long id)
        {
            var movimentacao = receitaBO.Find(id);
            if (movimentacao == null)
                return new HttpNotFoundResult();
            return new JsonResult(movimentacao);
        }

        [HttpDelete("api/receitas")]
        public IActionResult Delete([FromForm]List<long> ids)
        {
            try
            {
                receitaBO.Delete(ids);
                return new HttpOkResult();
            }
            catch (Exception)
            {
                return new BadRequestObjectResult(new VNextDurandalException("Ocorreu um erro ao deletar receita."));
            }
        }
        [HttpPost("api/receitas")]
        public IActionResult Post([FromBody]ReceitaViewModel receita)
        {
            try
            {
                return new JsonResult(receitaBO.New(Mapper.Map<Movimentacao>(receita)));
            }
            catch (VNextDurandalException e)
            {
                return new BadRequestObjectResult(e.Errors);
            }
        }

        [HttpPut("api/receitas")]
        public IActionResult Put([FromBody]ReceitaViewModel receita)
        {
            try
            {
                return new JsonResult(receitaBO.Update(Mapper.Map<Movimentacao>(receita)));
            }
            catch (VNextDurandalException e)
            {
                return new BadRequestObjectResult(e.Errors);
            }
        }


    }
}
