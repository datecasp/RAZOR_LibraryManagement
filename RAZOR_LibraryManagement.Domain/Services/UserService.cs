using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.ViewModels;
using RAZOR_LibraryManagement.Models.Entities;
using RAZOR_LibraryManagement.Models.Models;

namespace RAZOR_LibraryManagement.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<vmNotification> CreateUserService(vmUserCreate vmCreateUser)
        {
            var vmNotification = new vmNotification();
            var createUser = new UserModel
            {
                UserName = vmCreateUser.UserName,
                Email = vmCreateUser.Email,
                PhoneNumber = vmCreateUser.PhoneNumber,
                IsActive = true
            };

            try
            {
                if (!CheckIfEmailExists(vmCreateUser.Email).Result)
                {
                    var userResult = await _unitOfWork.UserRepository.CreateUser(createUser);
                    _unitOfWork.Save();
                    vmNotification.Type = Lang.Notification.NotificationType.Success;
                    vmNotification.Message = "User created successfully";
                    return vmNotification;
                }
                vmNotification.Type = Lang.Notification.NotificationType.Error;
                vmNotification.Message = "Email exits in database";
                return vmNotification;
            }
            catch (Exception ex)
            {
                vmNotification.Type = Lang.Notification.NotificationType.Error;
                vmNotification.Message = "Exception thrown! " + ex.Message;
            }
            return vmNotification;
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

        private async Task<bool> CheckIfEmailExists(string email)
        {
            var userIfExists = _unitOfWork.UserRepository.GetUserByEmail(email).Result;
            if (userIfExists == null)
            {
                return false;
            }
            return true;
        }
    }
}
