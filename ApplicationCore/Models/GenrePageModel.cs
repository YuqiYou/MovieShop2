﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class GenrePageModel<TEntity> where TEntity : class
    {
        public string Name { get; }
        public int PageIndex { get; }
        public int PageSize { get; }
        public int TotalPages { get; }
        public int TotalRowCount { get; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
        public IEnumerable<TEntity> Data { get; set; }

        public GenrePageModel(string genreName, IEnumerable<TEntity> data, int pageIndex, int pageSize, int totalRowcount)
        {
            Name = genreName;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalRowCount = totalRowcount;
            Data = data;
            TotalPages = (int)Math.Ceiling(TotalRowCount / (double)pageSize);

        }
    }
}