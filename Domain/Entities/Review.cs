using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Review
    {
        [Key]
        public long Id { get; set; }
        public string Comment { get; set; }
        public long BookId { get; set; }
        public Book Book { get; set; }
    }
}
