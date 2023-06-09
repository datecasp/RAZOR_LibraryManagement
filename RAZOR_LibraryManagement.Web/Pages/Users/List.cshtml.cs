using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Domain.ViewModels;

namespace RAZOR_LibraryManagement.Web.Pages.Users
{
    public class ListModel : PageModel
    {
        private readonly IUserService _userService;
        public List<vmUserIndex> vmUserIndexList;


        public ListModel(IUserService userService)
        {
            _userService = userService;
        }

        public async Task OnGet()
        {
            
           
            vmUserIndexList = (await _userService.GetAllUsersService()).ToList();
        }
    }
}
