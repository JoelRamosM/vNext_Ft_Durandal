﻿using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using vNextDurandal.Business.Intefaces.BO;
using vNextDurandal.Business.Models;
using vNextDurandal.Commom.Exceptions;
using vNext_Durandal.Web.OutputModels;
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

        public IActionResult Receitas() => View();

        public IActionResult Receita() => View();

        [HttpGet("api/receitas")]
        public JsonResult Get([FromQuery]GridRequest request) => new JsonResult(request.ToResult(receitaBO.Data()));

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
                return new BadRequestObjectResult(new ErrorResult("Ocorreu um erro ao deletar receita."));
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
                return new BadRequestObjectResult(new ErrorResult(e));
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
                return new BadRequestObjectResult(new ErrorResult(e));
            }
        }


    }
}
