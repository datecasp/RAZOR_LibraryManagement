namespace RAZOR_LibraryManagement.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        void Dispose();

    }
}
