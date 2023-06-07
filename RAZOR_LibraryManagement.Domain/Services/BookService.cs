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
                        Id = book.BookId,
                        Title = book.Title,
                        Author = book.Author,
                        IsLoanable= book.IsLoanable
                    };

                    bookIndexList.Add(vwBook);
                }
            }
            catch(Exception ex)
            {

            }
            return bookIndexList;        }

        public async Task<vmBookDetails> GetBookByIdService(int id)
        {
            var vmBook = new vmBookDetails();
            try
            {
                var book = await _bookRepository.GetBookById(id);
                if(book != null)
                {
                    vmBook.Title = book.Title;
                    vmBook.Author = book.Author;
                    vmBook.Description = book.Description;
                    vmBook.isLoanable = book.IsLoanable;
                    vmBook.Id = id;
                    vmBook.ImageUrl = book.ImageUrl;
                }
            }
            catch(Exception ex)
            {

            }
            return vmBook;
        }
    }
}
