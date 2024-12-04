using Domain.Abstraction.PaginationFilter;

namespace Application.ServiceInterface
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}
