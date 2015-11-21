using System.Collections.Generic;
using vNextDurandal.Commom.Exceptions;
using vNextDurandal.Commom.Model;

namespace vNext_Durandal.Web.OutputModels
{
    public class ErrorResult
    {
        public ErrorResult(string error, string title)
        {
            Errors = new List<ErrorMessage> { new ErrorMessage("Erro", error) };
            Title = title;
        }
        public ErrorResult(VNextDurandalException ex, string title = "Erro")
        {
            Errors = ex.Errors;
            Title = title;
        }

        public ErrorResult(List<ErrorMessage> errors, string title = "Erro")
        {
            Errors = errors;
            Title = title;
        }

        public List<ErrorMessage> Errors { get; }

        public string Title { get; set; }
    }
}
