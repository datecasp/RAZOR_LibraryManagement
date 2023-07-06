using RAZOR_LibraryManagement.Models.Models;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Domain.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetAllUsersService();
        Task<UserModel> GetUserByIdService(int userId);
        Task<vmNotification> CreateUserService(UserModel userModel);
        Task<vmNotification> UpdateUserService(UserModel userModel);
    }
}
