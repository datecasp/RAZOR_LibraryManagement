using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Infra.DataContext;
using RAZOR_LibraryManagement.Models.Entities;

namespace RAZOR_LibraryManagement.Infra.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LM_DbContext _lM_DbContext;
        private readonly IMapper _mapper;

        public BookRepository(LM_DbContext lM_DbContext, IMapper mapper)
        {
            _lM_DbContext = lM_DbContext;
            _mapper = mapper;
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
                //var book = _lM_DbContext.Books.Include(b => b.Category).Where(b => b.BookId == id).FirstOrDefault();
                var book = _lM_DbContext.Books.Where(b => b.BookId == id).FirstOrDefault();
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
                return book;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
