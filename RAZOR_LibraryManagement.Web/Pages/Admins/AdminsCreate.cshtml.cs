using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.Models;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Web.Pages.Admins
{
    [Authorize(Roles = "SuperAdmin")]
    public class AdminsCreateModel : PageModel
    {
        private readonly IAdminService _adminService;

        [BindProperty]
        public vmAdminUserCreate vmAdminUser { get; set; }

        public AdminsCreateModel(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public void OnGet()
        {
            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<vmNotification>(notificationJson);
            }
        }

        public async Task<IActionResult> OnPost()
        {
            var notificationJson = await _adminService.CreateAdminService(vmAdminUser);

            TempData["Notification"] = JsonSerializer.Serialize(notificationJson);

            if (notificationJson.Type == Lang.Notification.NotificationType.Success)
            {

                return RedirectToPage("/admins/adminslist");
            }

            return RedirectToPage("/admins/adminscreate");
        }
    }
}
