using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Web.Pages.Admins
{
    [Authorize(Roles = "SuperAdmin")]
    public class CreateModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        [BindProperty]
        public vmAdminUserCreate vmAdminUser { get; set; }

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
                UserName = vmAdminUser.UserName,
                NormalizedUserName = vmAdminUser.UserName.ToUpper(),
                Email = vmAdminUser.Email,
                NormalizedEmail = vmAdminUser.Email.ToLower()
            };
                      
            var identityResult = await _userManager.CreateAsync(admin, vmAdminUser.Password);

            if (identityResult.Succeeded)
            {
                //Add Admin Role to just created admin
                var addRolesResult = await _userManager.AddToRoleAsync(admin, "Admin");

                if(addRolesResult.Succeeded)
                {
                    var notification = new vmNotification
                    {
                        Type = Lang.Notification.NotificationType.Success,
                        Message = "Admin created successfully"
                    };
                    TempData["Notification"] = JsonSerializer.Serialize(notification);
                    return RedirectToPage("/books/list");
                }
            }

            var errorNotification = new vmNotification
            {
                Type = Lang.Notification.NotificationType.Error,
                Message = "Something went wrong"
            };
            TempData["Notification"] = JsonSerializer.Serialize(errorNotification);
            return RedirectToPage("/admins/create");
        }
    }
}
