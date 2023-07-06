using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.ViewModels;
using System.Text.Json;

namespace RAZOR_LibraryManagement.Web.Pages.Admins
{
    [Authorize]
    public class AdminsListModel : PageModel
    {
        private readonly IAdminService _adminService;

        [BindProperty]
        public List<vmAdminUserList> vmAdminUserList { get; set; }

        public AdminsListModel(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task OnGet()
        {
            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<vmNotification>(notificationJson);
            }
            vmAdminUserList = await _adminService.GetAdminsListService(); // _mapper.Map<List<vmAdminUserCreate>>(adminsList);
        }
    }
}
