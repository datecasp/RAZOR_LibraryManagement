using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Domain.ViewModels;

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
            vmBookIndexList = (await _bookService.GetAllBooksService()).ToList();
        }
    }
}
