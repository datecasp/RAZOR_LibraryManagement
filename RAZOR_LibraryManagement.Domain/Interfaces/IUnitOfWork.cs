namespace RAZOR_LibraryManagement.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public ICategoryRepository CategoryRepository { get; }
        public IBookRepository BookRepository { get; }
        public IUserRepository UserRepository { get; set; }
        public IAppSettingsRepository AppSettingsRepository { get; }
        void Save();
        void Dispose();

    }
}
