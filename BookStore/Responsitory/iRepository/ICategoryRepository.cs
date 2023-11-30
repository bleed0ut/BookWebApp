using BookStore.Models;

namespace BookStore.Responsitory.iRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        public void Update (Category category);
    }
}
