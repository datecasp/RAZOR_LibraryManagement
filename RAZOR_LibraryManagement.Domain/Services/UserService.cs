using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Lang.Book;
using RAZOR_LibraryManagement.Models.Entities;
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
                var repo = _unitOfWork.GetRepository<Book>();
                usersList = repo.GetAllProfiled<UserModel>().Result.ToList();
            }
            catch (Exception ex)
            {

            }
            return usersList;
        }
        public async Task<UserModel> GetUserByIdService(int userId)
        {
            var user = new UserModel();
            try
            {
                user = (await _unitOfWork.UserRepository.GetAllUsers())
                    .FirstOrDefault(u => u.UserId == userId);
            }
            catch (Exception ex)
            {

            }
            return user;
        }

        public async Task<vmNotification> UpdateUserService(UserModel userModel)
        {
            var vmNotification = new vmNotification();
            try
            {
                var userResult = await _unitOfWork.UserRepository.UpdateUser(userModel);
                if(userResult != null)
                {
                    _unitOfWork.Save();
                    vmNotification.Type = Lang.Notification.NotificationType.Success;
                    vmNotification.Message = "User updated successfully";
                    return vmNotification;
                }
                else
                {
                    vmNotification.Type = Lang.Notification.NotificationType.Error;
                    vmNotification.Message = "Something went wrong updating user.";
                    return vmNotification;
                }
            }
            catch (Exception ex)
            {
                vmNotification.Type = Lang.Notification.NotificationType.Error;
                vmNotification.Message = "Exception thrown! " + ex.Message;
            }
            return vmNotification;
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
