using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Book
    {
        [Key]
        public long Id { get; set; }
        public string Title { get; set; } = "";
        public ICollection<Publisher> Publishers { get; set; }
    }
}
