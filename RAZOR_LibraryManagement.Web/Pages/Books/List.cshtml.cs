using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Web.Pages.Books
{
    [Authorize]
    public class ListModel : PageModel
    {
        private readonly IBookService _bookService;
        public List<vmBookIndex> vmBookIndexList;

        public ListModel(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task OnGet()
        {
            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<vmNotification>(notificationJson);
            }
            vmBookIndexList = (await _bookService.GetAllBooksService()).ToList();
        }
    }
}
