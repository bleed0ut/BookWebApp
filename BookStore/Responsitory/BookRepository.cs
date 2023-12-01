using BookStore.Data;
using BookStore.Models;
using BookStore.Responsitory.iRepository;

namespace BookStore.Responsitory
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly BookAppDBContext _dbContext;
        public BookRepository(BookAppDBContext dbContext) : base (dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(Book book)
        {
            _dbContext.Update(book);
        }
    }
}
