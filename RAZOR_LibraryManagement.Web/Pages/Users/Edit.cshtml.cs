using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Web.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        [BindProperty]
        public vmUserCreate vmCreateUser { get; set; }

        public EditModel(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        public async Task OnGet(int userId)
        {
            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<vmNotification>(notificationJson);
            }

            var user = (await _userService.GetAllUsersService())
                .Where(u => u.UserId == userId)
                .FirstOrDefault();
            vmCreateUser = _mapper.Map<vmUserCreate>(user);

        }
    }
}
