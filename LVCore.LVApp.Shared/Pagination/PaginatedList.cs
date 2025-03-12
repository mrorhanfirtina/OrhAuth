using System;
using System.Collections.Generic;

namespace LVCore.LVApp.Shared.Pagination
{
    public class PaginatedList<T>
    {
        public IEnumerable<T> Items { get; private set; }
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasPrevious => PageIndex > 1;
        public bool HasNext => PageIndex < TotalPages;

        public PaginatedList() { }

        public PaginatedList(IEnumerable<T> items, int count, int pageIndex, int pageSize)
        {
            Items = items;
            TotalCount = count;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public static PaginatedList<T> Create(IEnumerable<T> items, int count, int pageIndex, int pageSize)
        {
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
