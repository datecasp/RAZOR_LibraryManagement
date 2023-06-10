using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LibraryManagement.Domain.ViewModels;

namespace RAZOR_LibraryManagement.Web.Pages.Admins
{
    [Authorize(Roles = "SuperAdmin")]
    public class CreateModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        [BindProperty]
        public vmCreateAdmin vmCreateAdmin { get; set; }

        public CreateModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            //Seed admin
            var admin = new IdentityUser
            {
                UserName = vmCreateAdmin.UserName,
                NormalizedUserName = vmCreateAdmin.UserName.ToUpper(),
                Email = vmCreateAdmin.Email,
                NormalizedEmail = vmCreateAdmin.Email.ToLower()
            };
                      
            var identityResult = await _userManager.CreateAsync(admin, vmCreateAdmin.Password);

            if (identityResult.Succeeded)
            {
                //Add Admin Role to just created admin
                var addRolesResult = await _userManager.AddToRoleAsync(admin, "Admin");

                if(addRolesResult.Succeeded)
                {
                    ViewData["Notification"] = new vmNotification
                    {
                        Type = Lang.Notification.NotificationType.Success,
                        Message = "Admin created successfully"
                    };

                    return Page();
                }
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
