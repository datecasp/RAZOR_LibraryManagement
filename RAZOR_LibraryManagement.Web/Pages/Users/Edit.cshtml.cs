using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Domain.Services;
using RAZOR_LibraryManagement.Models.Models;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Web.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        [BindProperty]
        public vmUserEdit vmUserEdit { get; set; }

        public EditModel(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        public async Task OnGet(int id)
        {
            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<vmNotification>(notificationJson);
            }

            var user = await _userService.GetUserByIdService(id);
            vmUserEdit = _mapper.Map<vmUserEdit>(user);
        }

        public async Task<IActionResult> OnPost(int id)
        {
            var user = _mapper.Map<UserModel>(vmUserEdit);
            user.UserId = id;
            if (user != null)
            {
                var notification = await _userService.UpdateUserService(user);
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage($"/users/list");
            }
            var notificationError = new vmNotification
            {
                Type = Lang.Notification.NotificationType.Error,
                Message = "User not found"
            };
            TempData["Notification"] = JsonSerializer.Serialize(notificationError);
            return RedirectToPage($"/users/list");

        }
    }
}
