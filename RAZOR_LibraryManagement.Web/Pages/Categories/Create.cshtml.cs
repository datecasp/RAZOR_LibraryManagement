using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Web.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        [BindProperty]
        public vmCategoryIndex vmCategoryIndex { get; set; }

        public CreateModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(vmCategoryIndex vmCategoryIndex)
        {
            var categoryResult = await _categoryService.CreateCategoryService(vmCategoryIndex);

            if (categoryResult != null)
            {
                var notification = new vmNotification
                {
                    Type = Lang.Notification.NotificationType.Success,
                    Message = "Category created successfully"
                };
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/categories/list");
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
