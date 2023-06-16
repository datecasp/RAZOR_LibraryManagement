using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Domain.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<vmUserIndex>> GetAllUsersService();
        Task<vmUserCreate> CreateUserService(vmUserCreate vmCreateUser);
    }
}
