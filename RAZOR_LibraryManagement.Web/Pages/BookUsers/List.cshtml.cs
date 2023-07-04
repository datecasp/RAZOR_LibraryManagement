using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.Models;
using RAZOR_LibraryManagement.Models.ViewModels;
using System.Text.Json;

namespace RAZOR_LibraryManagement.Web.Pages.BookUsers
{
    public class ListModel : PageModel
    {
        private readonly IBookUserService _bookUserService;
        private readonly IAppSettingsService _appSettingsService;

        public List<BookUserModel> buList { get; set; }
        public List<AppSettingsModel> appSettingsParamsList { get; set; }
        public int DaysToWarnOriginal;
        public int DaysToReturnOriginal;

        public ListModel(IBookUserService bookUserService, IAppSettingsService appSettingsService)
        {
            _bookUserService = bookUserService;
            _appSettingsService = appSettingsService;
        }

        public async Task OnGet()
        {
            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<vmNotification>(notificationJson);
            }
            appSettingsParamsList = _appSettingsService.GetAllSettings().Result.ToList();
            DaysToWarnOriginal = appSettingsParamsList[1].Value;
            DaysToReturnOriginal = appSettingsParamsList[2].Value;
            buList = (await _bookUserService.GetBookUserListService()).ToList();
        }
    }
}
