using Domain.Entities;
using Domain.Interfaces;

namespace Infra.Data.Repositories
{
    public sealed class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(AppDbContext context) : base(context)
        {
        }
    }
}
