using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.Models;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Web.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        [BindProperty]
        public vmUserCreate vmCreateUser { get; set; }

        public CreateModel(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        public void OnGet()
        {
            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<vmNotification>(notificationJson);
            }
        }

        public async Task<IActionResult> OnPost(vmUserCreate userCreate)
        {
            var userModel = _mapper.Map<UserModel>(userCreate);
            var notificationJson = await _userService.CreateUserService(userModel);

            TempData["Notification"] = JsonSerializer.Serialize(notificationJson);

            if (notificationJson.Type == Lang.Notification.NotificationType.Success)
            {

                return RedirectToPage("/users/list");
            }
          
            return RedirectToPage("/users/create");
        }
    }
}
