using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Domain.Services;
using RAZOR_LibraryManagement.Models.Models;
using RAZOR_LibraryManagement.Models.ViewModels;
using System.Text.Json;

namespace RAZOR_LibraryManagement.Web.Pages.Books
{
    public class EditModel : PageModel
    {
        private readonly IBookUserService _bookUserService;
        private readonly IBookService _bookService;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        [BindProperty]
        public vmBookEdit vmBookEdit { get; set; }
        [BindProperty]
        public vmCategoryIndex vmCategory { get; set; }
        [BindProperty]
        public IEnumerable<vmCategoryIndex> vmCategoryIndexList { get; set; }
        [BindProperty]
        public IFormFile FeaturedImage { get; set; }
        public bool isBorrowed { get; set; }

        public EditModel(IBookUserService bookUserService, IBookService bookService, ICategoryService categoryService, IImageService imageService, IMapper mapper)
        {
            _bookUserService = bookUserService;
            _bookService = bookService;
            _categoryService = categoryService;
            _imageService = imageService;
            _mapper = mapper;
        }
        public async void OnGet(int id)
        {
            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<vmNotification>(notificationJson);
            }
            isBorrowed = _bookUserService.BookIsBorrowed(id).Result;
            var book = await _bookService.GetBookByIdService(id);
            var catList = (List<CategoryModel>)_categoryService.GetActiveCategoriesService().Result;
            vmCategoryIndexList = _mapper.Map<List<vmCategoryIndex>>(catList);
            vmBookEdit = _mapper.Map<vmBookEdit>(book);
        }

        public async Task<IActionResult> OnPost(int radioCategory)
        {
            vmBookEdit.CategoryId = radioCategory;
            var bookModel = _mapper.Map<BookModel>(vmBookEdit);
            var notification = await _bookService.CreateUpdateBookService(bookModel, true);
            TempData["Notification"] = JsonSerializer.Serialize(notification);

            if (notification.Type == Lang.Notification.NotificationType.Success)
            {
                return RedirectToPage("/books/list");
            }
            return RedirectToPage("/books/create");
        }
    }
}
