using System.Collections.Generic;
using vNextDurandal.Commom.Exceptions;
using vNextDurandal.Commom.Model;

namespace vNext_Durandal.Web.OutputModels
{
    public class ErrorResult
    {
        public ErrorResult(string error)
        {
            Errors = new List<ErrorMessage> { new ErrorMessage("Erro", error) };
        }
        public ErrorResult(VNextDurandalException ex)
        {
            Errors = ex.Errors;
        }
        public ErrorResult(List<ErrorMessage> errors)
        {
            Errors = errors;
        }

        public List<ErrorMessage> Errors { get; }
    }
}
