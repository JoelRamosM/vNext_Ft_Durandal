using System;

namespace vNext_Durandal.Commom.Exceptions
{
    public class ValorNegativoException : Exception
    {
        public ValorNegativoException(string msg) : base(msg) { }
    }
}