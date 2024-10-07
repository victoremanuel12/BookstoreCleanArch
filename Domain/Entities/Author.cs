using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Author
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; } = "";
        public ICollection<Book> Books { get; set; } 
    }
}
