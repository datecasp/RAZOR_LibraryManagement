using RAZOR_LibraryManagement.Models.Models;

namespace RAZOR_LibraryManagement.Domain.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookModel>> GetAllBooks();
        Task<BookModel> GetBookById(int id);
        Task<BookModel> CreateBook(BookModel bookModel);
        Task<BookModel> UpdateBook(BookModel bookModel);
    }
}
