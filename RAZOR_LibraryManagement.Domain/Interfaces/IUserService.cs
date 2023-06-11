using RAZOR_LibraryManagement.Domain.Models;
using RAZOR_LibraryManagement.Domain.ViewModels;

namespace RAZOR_LibraryManagement.Domain.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<vmUserIndex>> GetAllUsersService();
        Task<vmCreateUser> CreateUserService(vmCreateUser vmCreateUser);
    }
}
