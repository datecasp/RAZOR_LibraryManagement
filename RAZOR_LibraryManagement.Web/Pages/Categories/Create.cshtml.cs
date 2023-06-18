using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.Models;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Web.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        [BindProperty]
        public vmCategoryIndex vmCategoryIndex { get; set; }

        public CreateModel(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        public void OnGet()
        {
            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<vmNotification>(notificationJson);
            }
        }

        public async Task<IActionResult> OnPost(vmCategoryIndex vmCategoryIndex)
        {
            var categoryModel = _mapper.Map<CategoryModel>(vmCategoryIndex);
            var notification = await _categoryService.CreateCategoryService(categoryModel);
            TempData["Notification"] = JsonSerializer.Serialize(notification);

            if (notification.Type == Lang.Notification.NotificationType.Success)
            {
                return RedirectToPage("/categories/list");
            }

            return RedirectToPage("/categories/create");
        }
    }
}
