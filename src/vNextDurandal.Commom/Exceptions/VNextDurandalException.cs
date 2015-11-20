using System;
using System.Collections.Generic;
using vNextDurandal.Commom.Model;

namespace vNextDurandal.Commom.Exceptions
{
    public class VNextDurandalException : Exception
    {
        public VNextDurandalException(string message)
        {
            Errors = new List<ErrorMessage> { new ErrorMessage("Error", message) };
        }

        public VNextDurandalException(ErrorMessage errorMessage)
        {
            Errors = new List<ErrorMessage> { errorMessage };
        }

        public VNextDurandalException(params ErrorMessage[] errorMessages)
        {
            Errors = new List<ErrorMessage>(errorMessages);
        }
        public List<ErrorMessage> Errors { get; set; }
    }
}
