using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.Models;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Web.Pages.Admins
{
    public class ConfigurationsModel : PageModel
    {
        private readonly IAppSettingsService _appSettingsService;
        [BindProperty]
        public List<AppSettingsModel> appSettingsParamsList { get; set; }
        [BindProperty]
        public AppSettingsModel DaysToWarn { get; set; }
        [BindProperty]
        public AppSettingsModel DaysToReturn { get; set; }
        private static int DaysToWarnOriginal;
        public static int DaysToReturnOriginal;

        public ConfigurationsModel(IAppSettingsService appSettingsService)
        {
            _appSettingsService = appSettingsService;
        }
        public async void OnGet()
        {
            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<vmNotification>(notificationJson);
            }
            appSettingsParamsList = _appSettingsService.GetAllSettings().Result.ToList();
            DaysToWarn = appSettingsParamsList[1];
            DaysToReturn = appSettingsParamsList[2];
            DaysToWarnOriginal = DaysToWarn.Value;
            DaysToReturnOriginal = DaysToReturn.Value;
        }

        public async Task<IActionResult> OnPost()
        {
            if (DaysToWarnOriginal != DaysToWarn.Value && DaysToReturnOriginal != DaysToReturn.Value)
            {
                var warnSetting = new AppSettingsModel
                {
                    Id = 2,
                    SettingParam = "DaysToWarningDate",
                    Value = DaysToWarn.Value
                };
                var returnSetting = new AppSettingsModel
                {
                    Id = 3,
                    SettingParam = "DaysToReturnDate",
                    Value = DaysToReturn.Value
                };
                var result = await _appSettingsService.UpdateSettingService(warnSetting, returnSetting);
                TempData["Notification"] = JsonSerializer.Serialize(result);
                return RedirectToPage("/admins/configurations");
            }else if (DaysToReturnOriginal != DaysToReturn.Value)
            {
                var returnSetting = new AppSettingsModel
                {
                    Id = 3,
                    SettingParam = "DaysToReturnDate",
                    Value = DaysToReturn.Value
                };
                var result = await _appSettingsService.UpdateSettingService(returnSetting);
                TempData["Notification"] = JsonSerializer.Serialize(result);
                return RedirectToPage("/admins/configurations");
            }
            else if (DaysToWarnOriginal != DaysToWarn.Value)
            {
                var warnSetting = new AppSettingsModel
                {
                    Id = 2,
                    SettingParam = "DaysToWarningDate",
                    Value = DaysToWarn.Value
                };
                var result = await _appSettingsService.UpdateSettingService(warnSetting);
                TempData["Notification"] = JsonSerializer.Serialize(result);
                return RedirectToPage("/admins/configurations");
            }

            var eNotifications = new vmNotification
            {
                Type = Lang.Notification.NotificationType.Error,
                Message = "Values are equal"
            };
            TempData["Notification"] = JsonSerializer.Serialize(eNotifications);
            return RedirectToPage("/admins/configurations");
        }
    }
}
