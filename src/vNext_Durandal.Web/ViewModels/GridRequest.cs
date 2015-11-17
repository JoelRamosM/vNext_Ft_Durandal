﻿using System.Collections.Generic;
using System.Linq;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace vNext_Durandal.Web.ViewModels
{
    public class GridRequest
    {
        public string Query { get; set; }

        public int PageLength { get; set; }

        public int CurrentPage { get; set; }

        public string GroupBy { get; set; }

        public int TotalPages { get; set; }

        public int SkipCount() => CurrentPage == 1 ? 0 : PageLength * (CurrentPage-1);


        public List<object> Data { get; set; }


        public GridRequest ToResult<T>(IQueryable<T> data)
        {
            TotalPages = ((data.Count() + PageLength - 1 )/ PageLength);
            Data = data.ToList().Skip(SkipCount()).Take(PageLength).Select(t => t as object).ToList();
            return this;
        }
    }
}