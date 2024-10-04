namespace Domain.Entities
{
    public class Review
    {
        public long Id { get; set; }
        public string Comment { get; set; }
        public Book Book { get; set; }
    }
}
