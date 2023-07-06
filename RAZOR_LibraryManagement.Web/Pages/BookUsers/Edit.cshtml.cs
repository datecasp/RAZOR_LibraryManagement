using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.Models;
using RAZOR_LibraryManagement.Models.ViewModels;
using System.Text.Json;

namespace RAZOR_LibraryManagement.Web.Pages.BookUsers
{
    public class EditModel : PageModel
    {
        private readonly IBookUserService _bookUserService;
        private readonly IMapper _mapper;

        [BindProperty]
        public vmBookUser vmBookUser { get; set; }

        public EditModel(IBookUserService bookUserService, IMapper mapper)
        {
            _bookUserService = bookUserService;
            _mapper = mapper;
        }
        public async Task OnGet(int id)
        {
            var bu = await _bookUserService.GetBookUserByIdService(id);
            vmBookUser = _mapper.Map<vmBookUser>(bu);
        }

        public async Task<IActionResult> OnPost(int id)
        {
            var buMap = _mapper.Map<BookUserModel>(vmBookUser);
            buMap.IsActualUser = !buMap.IsActualUser;

            var notification = await _bookUserService.ToggleBookUserIsActualUser(buMap);
            
            TempData["Notification"] = JsonSerializer.Serialize(notification);

            if(notification.Type == Lang.Notification.NotificationType.Success)
            {
                return RedirectToPage("/bookusers/list");
            }

            return RedirectToPage($"/bookusers/edit/{id}");
        }
    }
}
