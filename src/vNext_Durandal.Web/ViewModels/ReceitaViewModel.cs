using System;
using Newtonsoft.Json;

namespace vNext_Durandal.Web.ViewModels
{
    public class ReceitaViewModel
    {
        public long Id { get; set; }

        public double Valor { get; set; }

        public string Observacao { get; set; }

        public DateTime Data { get; set; }
    }
}
