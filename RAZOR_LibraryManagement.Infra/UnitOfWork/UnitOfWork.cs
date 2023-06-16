using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.Entities;
using RAZOR_LibraryManagement.Infra.DataContext;
using RAZOR_LibraryManagement.Infra.Repositories;

namespace RAZOR_LibraryManagement.Infra.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
        {
            private LM_DbContext _context;
            private GenericRepository<Category> categoryRepository;

            public GenericRepository<Category> CategoryRepository
            {
                get
                {

                    if (this.categoryRepository == null)
                    {
                        this.categoryRepository = new GenericRepository<Category>(_context);
                    }
                    return categoryRepository;
                }
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