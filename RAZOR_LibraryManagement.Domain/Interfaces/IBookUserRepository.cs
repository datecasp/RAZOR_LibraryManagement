using RAZOR_LibraryManagement.Models.Models;

namespace RAZOR_LibraryManagement.Domain.Interfaces
{
    public interface IBookUserRepository
    {
        Task<BookUserModel> AddBookToUser(BookUserModel bookUserModel, int maxBooks);
    }
}
