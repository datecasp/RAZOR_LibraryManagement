using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Infra.DataContext;
using RAZOR_LibraryManagement.Infra.Repositories;

namespace RAZOR_LibraryManagement.Infra.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LM_DbContext _context;
        public ICategoryRepository CategoryRepository { get; set; }
        public IBookRepository BookRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        public IAppSettingsRepository AppSettingsRepository { get; set; }

        public UnitOfWork(LM_DbContext lM_DbContext)
        {
            _context = lM_DbContext;
            CategoryRepository = new CategoryRepository(_context);
            BookRepository = new BookRepository(_context);
            UserRepository = new UserRepository(_context);
            AppSettingsRepository= new AppSettingsRepository(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && _context != null)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}