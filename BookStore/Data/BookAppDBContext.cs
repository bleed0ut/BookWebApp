using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public class BookAppDBContext : DbContext
    {
        public BookAppDBContext(DbContextOptions<BookAppDBContext> options) : base(options) {
        
        }
    }
}
