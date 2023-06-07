using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Domain.ViewModels;

namespace RAZOR_LibraryManagement.Web.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private readonly IBookService _bookService;
        public vmBookDetails vmBookDetails;

        public DetailsModel(IBookService bookService)
        {
            _bookService = bookService;
        }
        public async Task OnGet(int id)
        {
            vmBookDetails = await _bookService.GetBookByIdService(id);
        }
    }
}
