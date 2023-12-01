using BookStore.Models;

namespace BookStore.Responsitory.iRepository
{
    public interface IBookRepository : IRepository<Book>
    {
        public void Update(Book book);
    }
}
