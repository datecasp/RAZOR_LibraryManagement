using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Domain.Interfaces
{
    public interface IBookService
    {
        Task<vmNotification> CreateBookService(vmBookCreate vmCreateBook);
        Task<IEnumerable<vmBookIndex>> GetAllBooksService();
        Task<vmBookDetails> GetBookByIdService(int id);
    }
}
