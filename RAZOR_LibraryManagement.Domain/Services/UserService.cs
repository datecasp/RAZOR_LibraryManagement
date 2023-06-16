using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.ViewModels;
using RAZOR_LibraryManagement.Models.Entities;

namespace RAZOR_LibraryManagement.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                var userResult = await _unitOfWork.UserRepository.CreateUser(createUser);
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
                var usersList = (await _unitOfWork.UserRepository.GetAllUsers()).ToList();
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
