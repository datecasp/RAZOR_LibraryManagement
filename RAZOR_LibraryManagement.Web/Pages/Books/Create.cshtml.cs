using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Web.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly IBookService _bookService;
        private readonly IImageService _imageService;
        private readonly ICategoryService _categoryService;

        [BindProperty]
        public vmBookCreate vmBookCreate { get; set; }
        [BindProperty]
        public vmCategoryIndex vmCategory { get; set; }
        [BindProperty]
        public IEnumerable<vmCategoryIndex> vmCategoryIndexList { get; set; }
        [BindProperty]
        public IFormFile FeaturedImage { get; set; }

        public CreateModel(IBookService bookService, ICategoryService categoryService, IImageService imageService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _imageService = imageService;
        }
        public async void OnGet()
        {
            vmCategoryIndexList = (List<vmCategoryIndex>)_categoryService.GetActiveCategoriesService().Result;
        }

        public async Task<IActionResult> OnPost(int radioCategory)
        {
            vmBookCreate.CategoryId = radioCategory;
            var bookResult = await _bookService.CreateBookService(vmBookCreate);

            if (bookResult != null)
            {
                var notification = new vmNotification
                {
                    Type = Lang.Notification.NotificationType.Success,
                    Message = "Book created successfully"
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/books/list");
            }

            ViewData["Notification"] = new vmNotification
            {
                Type = Lang.Notification.NotificationType.Error,
                Message = "Something went wrong"
            };

            return Page();


        }

    }
}
