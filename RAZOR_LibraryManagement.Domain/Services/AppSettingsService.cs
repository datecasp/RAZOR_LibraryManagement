using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.Models;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Domain.Services
{
    public class AppSettingsService : IAppSettingsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppSettingsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppSettingsModel>> GetAllSettings()
        {
            return await _unitOfWork.AppSettingsRepository.GetAllSettings();
        }

        public async Task<vmNotification> UpdateSettingService(AppSettingsModel setting)
        {
            var result = await _unitOfWork.AppSettingsRepository.UpdateSetting(setting);
            _unitOfWork.Save();
            return _NotificationFactory(result, null);
        }

        public async Task<vmNotification> UpdateSettingService(AppSettingsModel warningSetting, AppSettingsModel returnSetting)
        {
            var resultWarn = await _unitOfWork.AppSettingsRepository.UpdateSetting(warningSetting);
            var resultReturn = await _unitOfWork.AppSettingsRepository.UpdateSetting(returnSetting);
            _unitOfWork.Save();
            return _NotificationFactory(resultWarn, resultReturn);
        }

        #region Private mehthods

        private vmNotification _NotificationFactory(AppSettingsModel result, AppSettingsModel resultTwo)
        {
            if (result != null && resultTwo != null)
            {
                var sNotification = new vmNotification
                {
                    Type = Lang.Notification.NotificationType.Success,
                    Message = "Parameters updated successfully"
                };
                return sNotification;
            }
            else if (result != null || resultTwo != null)
            {
                var sNotification = new vmNotification
                {
                    Type = Lang.Notification.NotificationType.Success,
                    Message = "Parameter updated successfully"
                };
                return sNotification;
            }
            else
            {
                var sNotification = new vmNotification
                {
                    Type = Lang.Notification.NotificationType.Success,
                    Message = "Something went wrong..."
                };
                return sNotification;
            }
        }
    }

    #endregion
}

