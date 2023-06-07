using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Domain.Models;
using RAZOR_LibraryManagement.Domain.ViewModels;

namespace RAZOR_LibraryManagement.Domain.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
           _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<vmBookIndex>> GetAllBooksService()
        {
            var booksList =new List<Book>();
            var bookIndexList = new List<vmBookIndex>();
            try
            {
                booksList = (await _bookRepository.GetAllBooks()).ToList();
                foreach (var book in booksList)
                {
                    var vwBook = new vmBookIndex
                    {
                        Title = book.Title,
                        Author = book.Author
                    };

                    bookIndexList.Add(vwBook);
                }
            }
            catch(Exception ex)
            {

            }
            return bookIndexList;        }
    }
}
