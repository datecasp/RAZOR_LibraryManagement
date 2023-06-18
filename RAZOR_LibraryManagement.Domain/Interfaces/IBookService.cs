using RAZOR_LibraryManagement.Models.Models;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Domain.Interfaces
{
    public interface IBookService
    {
        Task<vmNotification> CreateBookService(BookModel bookModel);
        Task<IEnumerable<BookModel>> GetAllBooksService();
        Task<BookModel> GetBookByIdService(int id);
    }
}
