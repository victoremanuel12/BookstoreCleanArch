﻿namespace Domain.Entities
{
    public class Review : BaseEntity
    {

        public string Comment { get; set; }
        public long BookId { get; set; }
        public Book Book { get; set; }
        protected Review(){}
        public Review(string comment, long bookId)
        {
            Comment = comment;
            BookId = bookId;
        }
    }
}
