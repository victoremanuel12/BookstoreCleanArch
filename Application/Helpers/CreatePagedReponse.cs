using Application.ServiceInterface;
using Domain.Abstraction;
using Domain.Abstraction.PaginationFilter;

namespace Application.Helpers
{
    public static class PagedResponseHelper
    {
        public static Result<PagedResult<IEnumerable<T>>> CreatePagedResponse<T>(
            List<T> pagedData,
            PaginationFilter filter,
            int totalRecords,
            IUriService uriService,
            string route
        )
        {
            var totalPages = ((double)totalRecords / (double)filter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

            Uri? nextPageUri = filter.PageNumber >= 1 && filter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(filter.PageNumber + 1, filter.PageSize), route)
                : null;

            Uri? previousPageUri = filter.PageNumber - 1 >= 1 && filter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(filter.PageNumber - 1, filter.PageSize), route)
                : null;

            Uri firstPageUri = uriService.GetPageUri(new PaginationFilter(1, filter.PageSize), route);
            Uri lastPageUri = uriService.GetPageUri(new PaginationFilter(roundedTotalPages, filter.PageSize), route);

            var pagedResult = PagedResult<IEnumerable<T>>.Success(
                value: pagedData,
                pageNumber: filter.PageNumber,
                pageSize: filter.PageSize,
                firstPage: firstPageUri,
                lastPage: lastPageUri,
                totalPages: roundedTotalPages,
                totalRecords: totalRecords,
                nextPage: nextPageUri,
                previousPage: previousPageUri
            );

            return Result<PagedResult<IEnumerable<T>>>.Success(pagedResult);
        }
    }
}
