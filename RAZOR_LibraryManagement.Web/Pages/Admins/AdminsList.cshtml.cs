using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        [BindProperty]
        public List<IdentityUser> vmAdminUserList { get; set; }

        public AdminsListModel(UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task OnGet()
        {
            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<vmNotification>(notificationJson);
            }
            var adminsList = (await _userManager.GetUsersInRoleAsync("admin")).ToList();
            vmAdminUserList = adminsList; // _mapper.Map<List<vmAdminUserCreate>>(adminsList);
        }
    }
}
