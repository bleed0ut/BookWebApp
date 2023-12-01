using BookStore.Data;
using BookStore.Responsitory.iRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookStore.Responsitory
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly BookAppDBContext _dbContext;
        public DbSet<T> DbSet { get; set; }

        public Repository(BookAppDBContext dbContext)
        {
            _dbContext = dbContext;
            DbSet = _dbContext.Set<T>();
        }
        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperty = null)
        {
            IQueryable<T> query = DbSet;
            query = query.Where(filter);
            if (!String.IsNullOrEmpty(includeProperty))
            {
                query.Include(includeProperty).ToList();
            }
                
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProperty = null)
        {
            IQueryable<T> query = DbSet;
            if (!String.IsNullOrEmpty(includeProperty))
            {
                query.Include(includeProperty).ToList();
            }
            return query.ToList();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
        
    }
}
