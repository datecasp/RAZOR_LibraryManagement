using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.Models;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Web.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly IBookService _bookService;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        [BindProperty]
        public vmBookCreate vmBookCreate { get; set; }
        [BindProperty]
        public vmCategoryIndex vmCategory { get; set; }
        [BindProperty]
        public IEnumerable<vmCategoryIndex> vmCategoryIndexList { get; set; }
        [BindProperty]
        public IFormFile FeaturedImage { get; set; }

        public CreateModel(IBookService bookService, ICategoryService categoryService, IImageService imageService, IMapper mapper)
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _imageService = imageService;
            _mapper = mapper;
        }
        public async void OnGet()
        {
            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<vmNotification>(notificationJson);
            }

            vmCategoryIndexList = (List<vmCategoryIndex>)_categoryService.GetActiveCategoriesService().Result;
        }

        public async Task<IActionResult> OnPost(int radioCategory)
        {
            vmBookCreate.CategoryId = radioCategory;
            var bookModel = _mapper.Map<BookModel>(vmBookCreate);
            var notification = await _bookService.CreateBookService(bookModel);
            TempData["Notification"] = JsonSerializer.Serialize(notification);

            if (notification.Type == Lang.Notification.NotificationType.Success)
            {
                return RedirectToPage("/categories/list");
            }
            return RedirectToPage("/categories/create");
        }

    }
}
