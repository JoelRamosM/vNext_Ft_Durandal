using System;

namespace vNextDurandal.Commom.Exceptions
{
    public class ValorNegativoException : Exception
    {
        public ValorNegativoException(string msg) : base(msg) { }
    }
}