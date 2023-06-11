using RAZOR_LibraryManagement.Domain.Models;

namespace RAZOR_LibraryManagement.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> CreateUser(User user);
    }
}
