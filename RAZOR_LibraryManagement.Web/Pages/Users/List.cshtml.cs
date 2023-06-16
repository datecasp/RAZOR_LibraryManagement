using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Web.Pages.Users
{
    [Authorize]
    public class ListModel : PageModel
    {
        private readonly IUserService _userService;
        public List<vmUserIndex> vmUserIndexList;
        public vmNotification vmNotification { get; set; }

        public ListModel(IUserService userService)
        {
            _userService = userService;
        }

        public async Task OnGet()
        {
            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<vmNotification>(notificationJson); 
            }
           
            vmUserIndexList = (await _userService.GetAllUsersService()).ToList();
        }
    }
}
