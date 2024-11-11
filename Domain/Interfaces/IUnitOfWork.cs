namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IAuthorRepository AuthorRepository { get; }
        IReviewRepository ReviewRepository { get; }
        Task CommitAsync();
    }
}
