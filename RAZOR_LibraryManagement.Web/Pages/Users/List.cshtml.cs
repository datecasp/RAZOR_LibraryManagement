using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Web.Pages.Users
{
    [Authorize]
    public class ListModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public List<vmUserIndex> vmUserIndexList;
        public vmNotification vmNotification { get; set; }

        public ListModel(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task OnGet()
        {
            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<vmNotification>(notificationJson); 
            }
           var usersList = (await _userService.GetAllUsersService()).ToList();
            vmUserIndexList = _mapper.Map<List<vmUserIndex>>(usersList);
        }
    }
}
