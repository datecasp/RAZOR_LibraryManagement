using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.Models;
using RAZOR_LibraryManagement.Models.ViewModels;
using System.Text.Json;

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
        [BindProperty]
        public AppSettingsModel MaxNumOfBooks { get; set; }
        private static int DaysToWarnOriginal;
        private static int DaysToReturnOriginal;
        private static int MaxNumOfBooksOriginal;


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
            MaxNumOfBooks = appSettingsParamsList[3];
            DaysToWarnOriginal = DaysToWarn.Value;
            DaysToReturnOriginal = DaysToReturn.Value;
            MaxNumOfBooksOriginal = MaxNumOfBooks.Value;
        }

        public async Task<IActionResult> OnPost()
        {
            var result = new vmNotification();
            if (DaysToReturnOriginal != DaysToReturn.Value)
            {
                var returnSetting = new AppSettingsModel
                {
                    Id = 3,
                    SettingParam = "DaysToReturnDate",
                    Value = DaysToReturn.Value
                };
                result = await _appSettingsService.UpdateSettingService(returnSetting);
              
            }
            if (DaysToWarnOriginal != DaysToWarn.Value)
            {
                var warnSetting = new AppSettingsModel
                {
                    Id = 2,
                    SettingParam = "DaysToWarningDate",
                    Value = DaysToWarn.Value
                };
                result = await _appSettingsService.UpdateSettingService(warnSetting);
                
            }
            if (MaxNumOfBooksOriginal != MaxNumOfBooks.Value)
            {
                var numBooksSetting = new AppSettingsModel
                {
                    Id = 4,
                    SettingParam = "MaxNumberOfBooks",
                    Value = MaxNumOfBooks.Value
                };
                result = await _appSettingsService.UpdateSettingService(numBooksSetting);
               
            }

            TempData["Notification"] = JsonSerializer.Serialize(result);
            return RedirectToPage("/admins/configurations");
        }
    }
}
