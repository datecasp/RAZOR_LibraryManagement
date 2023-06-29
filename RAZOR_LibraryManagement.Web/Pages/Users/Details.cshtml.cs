using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Web.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly IBookUserService _bookUserService;
        [BindProperty]
        public vmUserDetails vmUserDetails { get; set; }
        public DetailsModel(IBookUserService bookUserService)
        {
            _bookUserService = bookUserService;
        }

        public async Task OnGet(int id)
        {
            var user = await _bookUserService.GetVmUserDetails(id);
            vmUserDetails = user;
        }
    }
}
