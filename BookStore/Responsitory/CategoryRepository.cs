using BookStore.Data;
using BookStore.Models;
using BookStore.Responsitory.iRepository;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Responsitory
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly BookAppDBContext _dbContext;

        public CategoryRepository(BookAppDBContext dbContext) : base(dbContext)
        { 
            _dbContext = dbContext;
        }

        public void Update(Category category)
        {
           _dbContext.Update(category);
        }
    }
}
