using RAZOR_LibraryManagement.Models.Models;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Domain.Interfaces
{
    public interface IAppSettingsService
    {
        Task<IEnumerable<AppSettingsModel>> GetAllSettings();
        Task<vmNotification> UpdateSettingService(AppSettingsModel setting);
        Task<vmNotification> UpdateSettingService(AppSettingsModel warningSetting, AppSettingsModel returnSetting);
    }
}
