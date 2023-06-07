using RAZOR_LibraryManagement.Domain.ViewModels;

namespace RAZOR_LibraryManagement.Domain.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<vmUserIndex>> GetAllUsersService();
    }
}
