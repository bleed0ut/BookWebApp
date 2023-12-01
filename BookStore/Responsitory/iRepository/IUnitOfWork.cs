namespace BookStore.Responsitory.iRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IBookRepository BookRepository { get; }

        void Save();
    }
}
