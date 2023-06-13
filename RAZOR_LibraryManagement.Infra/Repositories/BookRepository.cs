using Microsoft.EntityFrameworkCore;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Domain.Models;
using RAZOR_LibraryManagement.Infra.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAZOR_LibraryManagement.Infra.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LM_DbContext _lM_DbContext;

        public BookRepository(LM_DbContext lM_DbContext)
        {
            _lM_DbContext = lM_DbContext;
        }
        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            var result = new List<Book>();
            try
            {
                result = await _lM_DbContext.Books.ToListAsync();
            }
            catch (Exception ex)
            {

            }
            return result;

        }

        public async Task<Book> GetBookById(int id)
        {
            var result = new Book();
            try
            {
                var book = _lM_DbContext.Books.Include(b => b.Category).Where(b => b.BookId == id).FirstOrDefault();
                result = book;
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public async Task<Book> CreateBook(Book book)
        {
            try
            {
                _lM_DbContext.Books.Add(book);
                await _lM_DbContext.SaveChangesAsync();
                return book;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
