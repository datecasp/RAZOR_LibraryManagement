using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.ViewModels;
using RAZOR_LibraryManagement.Models.Entities;

namespace RAZOR_LibraryManagement.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<vmUserCreate> CreateUserService(vmUserCreate vmCreateUser)
        {
            var vmUserResult = new vmUserCreate();
            var createUser = new User
            {
                UserName = vmCreateUser.UserName,
                Email = vmCreateUser.Email,
                PhoneNumber= vmCreateUser.PhoneNumber,
                IsActive = true
            };
            try
            {
                var userResult = await _userRepository.CreateUser(createUser);
                vmUserResult.UserName = userResult.UserName;
                vmUserResult.Email = userResult.Email;
                vmUserResult.PhoneNumber = userResult.PhoneNumber;
            }
            catch (Exception ex)
            {

            }
            return vmUserResult;
        }

        public async Task<IEnumerable<vmUserIndex>> GetAllUsersService()
        {
            var bookIndexList = new List<vmUserIndex>();
            try
            {
                var usersList = (await _userRepository.GetAllUsers()).ToList();
                foreach (var user in usersList)
                {
                    var vwUser = new vmUserIndex
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        IsActive = user.IsActive,
                    };

                    bookIndexList.Add(vwUser);
                }
            }
            catch (Exception ex)
            {

            }
            return bookIndexList;
        }
    }
}
