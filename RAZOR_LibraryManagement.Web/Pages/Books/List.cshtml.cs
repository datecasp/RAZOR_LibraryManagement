using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Domain.Services;
using RAZOR_LibraryManagement.Domain.ViewModels;
using RAZOR_LibraryManagement.Infra.DataContext;

namespace RAZOR_LibraryManagement.Web.Pages.Books
{
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
            var booksList = await _bookService.GetAllBooksService();
            var bookIndexList = new List<vmBookIndex>();
            foreach (var book in booksList)
            {
                var vwBook = new vmBookIndex
                {
                    Title = book.Title,
                    Author = book.Author
                };

                bookIndexList.Add(vwBook);
            }
            vmBookIndexList = bookIndexList.ToList();
        }
    }
}
