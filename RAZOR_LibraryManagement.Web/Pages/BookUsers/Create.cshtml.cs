using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.Models;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Web.Pages.BookUsers
{
    public class CreateModel : PageModel
    {
        private readonly IBookUserService _bookUserService;
        private readonly IBookService _bookService;
        private readonly IUserService _userService;
        public List<BookModel> Books;
        public List<UserModel> Users;
        public AllBooksAndUsersModel AllBooksAndUsers = new AllBooksAndUsersModel();
        public BookUserModel BookUser;


        public CreateModel(IBookUserService bookUserService,
            IBookService bookService,
            IUserService userService)
        {
            _bookUserService = bookUserService;
            _bookService = bookService;
            _userService = userService;
        }
        public async Task OnGet()
        {
            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<vmNotification>(notificationJson);
            }
            AllBooksAndUsers = await _bookUserService.GetAllBooksAndUsersService();
        }

        public async Task<IActionResult> OnPost(int bookId, int userId)
        {
            var notification = await _bookUserService.AddBookToUserService(userId, bookId);
            TempData["Notification"] = JsonSerializer.Serialize(notification);
            if (notification.Type == Lang.Notification.NotificationType.Success)
            {
                return RedirectToPage("/books/list");
            }
            return RedirectToPage("/bookusers/create");
        }

    }
}
