using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LibraryManagement.Domain.ViewModels;

namespace RAZOR_LibraryManagement.Web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signManager;

        [BindProperty]
        public vmLogin vmLogin { get; set; }

        public LoginModel(SignInManager<IdentityUser> signManager)
        {
            _signManager = signManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var result = await _signManager.PasswordSignInAsync(vmLogin.UserName, vmLogin.Password, false, false);
            if(result.Succeeded)
            {
                ViewData["Notification"] = new vmNotification
                {
                    Message = "Login successfully",
                    Type = Lang.Notification.NotificationType.Success
                };
                return RedirectToPage("Index");
            }
            else
            {
                ViewData["Notification"] = new vmNotification
                {
                    Message = "Wrong credentials",
                    Type = Lang.Notification.NotificationType.Error
                };
                return Page();
            }
        }
    }
}
