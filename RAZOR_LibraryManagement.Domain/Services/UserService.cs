using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.Models;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<vmNotification> CreateUserService(UserModel userModel)
        {
            var vmNotification = new vmNotification();
            try
            {
                if (!CheckIfEmailExists(userModel.Email).Result)
                {
                    var userResult = await _unitOfWork.UserRepository.CreateUser(userModel);
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

        public async Task<IEnumerable<UserModel>> GetAllUsersService()
        {
            var usersList = new List<UserModel>();
            try
            {
                usersList = (await _unitOfWork.UserRepository.GetAllUsers()).ToList();
            }
            catch (Exception ex)
            {

            }
            return usersList;
        }

        #region private methods
        private async Task<bool> CheckIfEmailExists(string email)
        {
            var userIfExists = _unitOfWork.UserRepository.GetUserByEmail(email).Result;
            if (userIfExists == null)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
