namespace RAZOR_LibraryManagement.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public ICategoryRepository CategoryRepository { get; }
        void Save();
        void Dispose();

    }
}
