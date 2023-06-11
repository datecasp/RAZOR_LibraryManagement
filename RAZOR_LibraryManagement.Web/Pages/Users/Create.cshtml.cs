using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Domain.ViewModels;

namespace RAZOR_LibraryManagement.Web.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly IUserService _userService;

        [BindProperty]
        public vmCreateUser vmCreateUser { get; set; }

        public CreateModel(IUserService userService)
        {
            _userService = userService;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(vmCreateUser vmCreateUser)
        {
            var userResult = await _userService.CreateUserService(vmCreateUser);
           
            if (userResult != null)
            {
                ViewData["Notification"] = new vmNotification
                {
                    Type = Lang.Notification.NotificationType.Success,
                    Message = "User created successfully"
                };

                return Page();
            }

            ViewData["Notification"] = new vmNotification
            {
                Type = Lang.Notification.NotificationType.Error,
                Message = "Something went wrong"
            };

            return Page();
        }
    }
}
