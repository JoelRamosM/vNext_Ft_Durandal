using System;
using vNextDurandal.Commom.Abstract;
using vNext_Durandal.Business.Enums;

namespace vNextDurandal.Business.Models
{
    public class Movimentacao : EntityBase
    {
        public TipoMovimentacao Tipo { get; set; }
        public double Valor { get; set; }
        public DateTime Data { get; set; }
        public string Observacao { get; set; }
        public TipoCategoria Categoria { get; set; }
    }
}
