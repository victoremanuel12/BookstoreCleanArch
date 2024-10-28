namespace Domain.Entities
{
    public class Publisher
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();

    }
}
