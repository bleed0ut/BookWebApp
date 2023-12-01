using BookStore.Data;
using BookStore.Responsitory.iRepository;

namespace BookStore.Responsitory
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookAppDBContext _dbContext;
        public ICategoryRepository CategoryRepository { get; private set; }
        public IBookRepository BookRepository { get; private set; }

        public UnitOfWork(BookAppDBContext dbContext)
        {
            _dbContext = dbContext;
            CategoryRepository = new CategoryRepository(dbContext);
            BookRepository = new BookRepository(dbContext);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
