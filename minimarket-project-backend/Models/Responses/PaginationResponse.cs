﻿namespace minimarket_project_backend.Models.Responses
{
    public class PaginationResponse<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public T? Data { get; set; }
    }
}
