using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Book
    {
        [Key]
        public long Id { get; set; }
        public string Title { get; set; } = "";

        public long PublisherId { get; set; }

        public Publisher Publisher { get; set; }

        public long ReviewId { get; set; }
        public Review Review { get; set; }
        public ICollection<Author> Authors { get; set; }
    }
}
