namespace Domain.Abstraction
{
    public class PagedResult<T> : Result<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Uri FirstPage { get; set; }
        public Uri LastPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }

        private PagedResult(T value, int pageNumber, int pageSize, Uri firstPage, Uri lastPage, int totalPages, int totalRecords, Uri nextPage, Uri previousPage)
            : base(value)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            FirstPage = firstPage;
            LastPage = lastPage;
            TotalPages = totalPages;
            TotalRecords = totalRecords;
            NextPage = nextPage;
            PreviousPage = previousPage;
        }

        private PagedResult(Error error) : base(error)
        {
        }

        public static PagedResult<T> Success(T value, int pageNumber, int pageSize, Uri firstPage, Uri lastPage, int totalPages, int totalRecords, Uri nextPage, Uri previousPage)
        {
            return new PagedResult<T>(value, pageNumber, pageSize, firstPage, lastPage, totalPages, totalRecords, nextPage, previousPage);
        }

        public static new PagedResult<T> Failure(Error error)
        {
            return new PagedResult<T>(error);
        }
    }
}
