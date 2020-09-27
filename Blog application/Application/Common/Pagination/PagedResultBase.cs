using System;

namespace Application.Common.Pagination
{
    public class PagedResultBase
    {
        public PagedResultBaseDetails Pager { get; set; }
    }

    public class PagedResultBaseDetails
    {
        public int CurrentPage { get; set; } 
        public int PageSize { get; set; } 
        public int RowCount { get; set; }
        
        public int PageCount
        {
            get { return (int)Math.Ceiling((double)RowCount / PageSize); }
        } 
        
        public int FirstRowOnPage
        {
            get { return (CurrentPage - 1) * PageSize + 1; }
        }
 
        public int LastRowOnPage
        {
            get { return Math.Min(CurrentPage * PageSize, RowCount); }
        }
    }
}