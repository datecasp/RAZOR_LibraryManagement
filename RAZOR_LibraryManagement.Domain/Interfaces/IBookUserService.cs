using RAZOR_LibraryManagement.Models.Models;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Domain.Interfaces
{
    public interface IBookUserService
    {
        Task<BookUserModel> GetBookUserByIdService(int id);
        Task<bool> UserHasBooks(int userId);
        Task<IEnumerable<BookUserModel>> GetBookUserListService();
        Task<IEnumerable<string>> GetBooksOfUser(int userId, bool? actualUser = null);
        Task<vmUserDetails> GetVmUserDetails(int userId);
        Task<vmNotification> AddBookToUserService(int userId, int bookId);
        Task<AllBooksAndUsersModel> GetBorrowableBooksAndUsersService();
        Task<vmNotification> ToggleBookUserIsActualUser(BookUserModel bu);
    }
}
