using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Infra.DataContext;
using RAZOR_LibraryManagement.Models.Entities;
using RAZOR_LibraryManagement.Models.Models;

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
        public async Task<IEnumerable<BookModel>> GetAllBooks()
        {
            var result = new List<BookModel>();
            try
            {
                var bookList = await _lM_DbContext.Books.ToListAsync();
                result = _mapper.Map<List<BookModel>>(bookList);
            }
            catch (Exception ex)
            {

            }
            return result;

        }

        public async Task<BookModel> GetBookById(int id)
        {
            var result = new BookModel();
            try
            {
                //var book = _lM_DbContext.Books.Include(b => b.Category).Where(b => b.BookId == id).FirstOrDefault();
                var book = _lM_DbContext.Books.Where(b => b.BookId == id).FirstOrDefault();
                result = _mapper.Map<BookModel>(book);
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public async Task<BookModel> CreateBook(BookModel bookModel)
        {
            var book = _mapper.Map<Book>(bookModel);
            try
            {
                _lM_DbContext.Books.Add(book);
                return _mapper.Map<BookModel>(book);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
