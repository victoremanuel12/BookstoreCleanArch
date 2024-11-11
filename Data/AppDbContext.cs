using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Book
            modelBuilder.Entity<Book>(builder =>
            {
                builder.ToTable("book");

                builder.HasKey(b => b.Id);
                builder.Property(p => p.Id)
                   .HasColumnName("id")
                   .IsRequired();

                builder.Property(b => b.Title)
                    .HasColumnName("title")
                    .HasColumnType("varchar(255)")
                    .IsRequired();

                builder.HasOne(b => b.Review)
                       .WithOne(r => r.Book)
                       .HasForeignKey<Review>(r => r.BookId)
                       .HasConstraintName("book_id")
                       .OnDelete(DeleteBehavior.Cascade);

                builder.Property(b => b.PublisherId)
                .HasColumnName("publisher_id");

                builder.HasOne(b => b.Publisher)
                  .WithMany(p => p.Books)
                  .HasForeignKey(b => b.PublisherId)
                  .HasConstraintName("publisher_id")
                  .OnDelete(DeleteBehavior.Cascade);
            });

            //Book_Author
            modelBuilder.Entity<Book>()
            .HasMany(b => b.Authors)
            .WithMany(a => a.Books)
            .UsingEntity<Dictionary<string, object>>(
                "book_author",
                j => j
                    .HasOne<Author>()
                    .WithMany()
                    .HasForeignKey("author_id")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<Book>()
                    .WithMany()
                    .HasForeignKey("book_id")
                    .OnDelete(DeleteBehavior.Cascade)
            );

            // Author
            modelBuilder.Entity<Author>(builder =>
            {
                builder.ToTable("author");
                builder.HasKey(a => a.Id);
                builder.Property(p => p.Id)
                  .HasColumnName("id")
                  .IsRequired();
                builder.Property(p => p.Id)
                    .HasColumnName("id")
                    .IsRequired();

                builder.Property(p => p.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)")
                    .IsRequired();
                builder.Property(p => p.IsActive)
                    .HasColumnName("active")
                    .HasColumnType("boolean")
                    .IsRequired();
            });
            // Publisher
            modelBuilder.Entity<Publisher>(builder =>
            {
                builder.ToTable("publisher");
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Id)
                  .HasColumnName("id")
                  .IsRequired();
                builder.Property(p => p.Name)
                   .HasColumnName("name")
                   .HasColumnType("varchar(255)")
                   .IsRequired();
            });


            // Review
            modelBuilder.Entity<Review>(builder =>
            {
                builder.ToTable("review");
                builder.HasKey(r => r.Id);
                builder.Property(p => p.Id)
                  .HasColumnName("id")
                  .IsRequired();
                builder.Property(p => p.Comment)
                 .HasColumnName("comment")
                 .HasColumnType("varchar(255)")
                 .IsRequired();

                builder.Property(p => p.BookId)
                .HasColumnName("book_id")
                .IsRequired();

                builder.HasOne(r => r.Book)  
                .WithOne(b => b.Review)   
                .HasForeignKey<Review>(r => r.BookId)  
                .OnDelete(DeleteBehavior.Cascade); 
            });
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
