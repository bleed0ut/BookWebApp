using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public class BookAppDBContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public BookAppDBContext(DbContextOptions<BookAppDBContext> options) : base(options) {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id=1, Name="Horror", Description="This is so scary", DisplayOrder=2},
                new Category { Id=2, Name="Action", Description="Fighting to ", DisplayOrder=3},
                new Category { Id=3, Name="Roman", Description="Kissing under the sunset", DisplayOrder=1},
                new Category { Id=4, Name = "Science", Description = "Research to save the earth", DisplayOrder =4}
                );

        }
    }
}
