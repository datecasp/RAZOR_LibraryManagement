using AutoMapper;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Infra.DataContext;
using RAZOR_LibraryManagement.Infra.Repositories;

namespace RAZOR_LibraryManagement.Infra.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LM_DbContext _context;
        private readonly IMapper _mapper;

        public ICategoryRepository CategoryRepository { get; set; }
        public IBookRepository BookRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        public IBookUserRepository BookUserRepository { get; set; }
        public IAppSettingsRepository AppSettingsRepository { get; set; }

        public UnitOfWork(LM_DbContext lM_DbContext, IMapper mapper)
        {
            _context = lM_DbContext;
            _mapper = mapper;
            CategoryRepository = new CategoryRepository(_context, _mapper);
            BookRepository = new BookRepository(_context, _mapper);
            UserRepository = new UserRepository(_context, _mapper);
            BookUserRepository = new BookUserRepository(_context, _mapper);
            AppSettingsRepository= new AppSettingsRepository(_context, _mapper);
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