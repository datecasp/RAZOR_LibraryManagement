using RAZOR_LibraryManagement.Models.Models;

namespace RAZOR_LibraryManagement.Domain.Interfaces
{
    public interface IAppSettingsRepository
    {
        Task<IEnumerable<AppSettingsModel>> GetAllSettings();
        Task<AppSettingsModel> UpdateSetting(AppSettingsModel setting);
    }
}
