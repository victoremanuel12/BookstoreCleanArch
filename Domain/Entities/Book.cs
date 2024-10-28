namespace Domain.Entities
{
    public class Book
    {
        public long Id { get; set; }
        public string Title { get; set; } = "";

        public long PublisherId { get; set; }

        public Publisher Publisher { get; set; }

        public Review Review { get; set; }
        public ICollection<Author> Authors { get; set; } = new List<Author>();
    }
}
