namespace RAZOR_LibraryManagement.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public ICategoryRepository CategoryRepository { get; }
        public IBookRepository BookRepository { get; }
        public IUserRepository UserRepository { get; set; }
        void Save();
        void Dispose();

    }
}
