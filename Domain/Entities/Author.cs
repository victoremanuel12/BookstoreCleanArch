namespace Domain.Entities
{
    public class Author : BaseEntity
    {

        public string Name { get; set; } = "";
        public bool IsActive { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
        protected Author(){}
        public Author(string name, bool isActive)
        {
            Name = name;
            IsActive = isActive;
        }
    }
}
