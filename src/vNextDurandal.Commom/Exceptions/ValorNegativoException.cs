using System;
using vNextDurandal.Commom.Model;

namespace vNextDurandal.Commom.Exceptions
{
    public class ValorNegativoException : VNextDurandalException
    {
        public ValorNegativoException(ErrorMessage msg) : base(msg) { }
    }
}