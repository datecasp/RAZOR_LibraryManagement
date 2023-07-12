using System.Text.Json;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public List<vmBookIndex> vmBookIndexList;

        public ListModel(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
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
