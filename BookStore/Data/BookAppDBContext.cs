using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public class BookAppDBContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public BookAppDBContext(DbContextOptions<BookAppDBContext> options) : base(options) {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Horror", Description = "This is so scary", DisplayOrder = 2 },
                new Category { Id = 2, Name = "Action", Description = "Fighting to ", DisplayOrder = 3 },
                new Category { Id = 3, Name = "Roman", Description = "Kissing under the sunset", DisplayOrder = 1 },
                new Category { Id = 4, Name = "Science", Description = "Research to save the earth", DisplayOrder = 4 }
                );
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "C# is the best",
                    Description = "Feeling it",
                    Author = "Microsoft",
                    Price = 10,
                    CategoryId = 1
                }
                ,
                 new Book
                 {
                     Id = 2,
                     Title = "Destroy world with Java",
                     Description = "Not gonna lie about this",
                     Author = "Oracle",
                     Price = 13,
                     CategoryId = 2
                 },
                 new Book
                 {
                     Id = 3,
                     Title = "Being pro at database",
                     Description = "Database is important",
                     Author = "Hell",
                     Price = 12,
                     CategoryId = 1
                 },
                 new Book
                 {
                     Id = 4,
                     Title = "Story about OOP",
                     Description = "You gotta read it~",
                     Author = "Unknow366",
                     Price = 12,
                     CategoryId = 3
                 },
                 new Book
                 {
                     Id = 5,
                     Title = "Don't mess with Data structure",
                     Description = "Not easy to learn (fact)",
                     Author = "Greenwich",
                     Price = 15,
                     CategoryId = 4
                 }
                );

        }
    }
}
