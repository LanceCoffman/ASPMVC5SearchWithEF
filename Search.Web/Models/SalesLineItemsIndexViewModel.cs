using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Search.Entities;

namespace Search.Web.Models
{
    public class SalesLineItemsIndexViewModel
    {
        public string Option { get; set; }
        public string Search { get; set; }
        public IEnumerable<SalesLineItem> Items { get; set; }
        public Pager Pager { get; set; }
    }

    public class Pager
    {
        public Pager(string option, string search, int totalItems, int? page, int pageSize = 10)
        {
            // calculate total, start and end pages
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var currentPage = page != null ? (int)page : 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;

            Option = option;
            Search = search;
        }

        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }

        public string Option { get; set; }
        public string Search { get; set; }
    }
}