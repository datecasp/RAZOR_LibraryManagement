using RAZOR_LibraryManagement.Models.Models;

namespace RAZOR_LibraryManagement.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserModel>> GetAllUsers();
        Task<UserModel> GetUserByEmail(string email);
        Task<UserModel> CreateUser(UserModel userModel);
        Task<UserModel> UpdateUser(UserModel userModel);
    }
}
