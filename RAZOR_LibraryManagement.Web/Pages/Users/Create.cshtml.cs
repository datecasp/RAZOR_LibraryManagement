using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Web.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly IUserService _userService;

        [BindProperty]
        public vmUserCreate vmCreateUser { get; set; }

        public CreateModel(IUserService userService)
        {
            _userService = userService;
        }
        public void OnGet()
        {
            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<vmNotification>(notificationJson);
            }
        }

        public async Task<IActionResult> OnPost(vmUserCreate vmCreateUser)
        {
            
            var notification = await _userService.CreateUserService(vmCreateUser);

            TempData["Notification"] = JsonSerializer.Serialize(notification);

            if (notification.Type == Lang.Notification.NotificationType.Success)
            {

                return RedirectToPage("/users/list");
            }
          
            return RedirectToPage("/users/create");
        }
    }
}
