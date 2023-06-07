using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Domain.Models;

namespace RAZOR_LibraryManagement.Domain.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
           _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> GetAllBooksService()
        {
            var result =new List<Book>();
            try
            {
                result = (await _bookRepository.GetAllBooks()).ToList();
            }
            catch(Exception ex)
            {

            }
            return result;        }
    }
}
