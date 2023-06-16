using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Web.Pages.Categories
{
    [Authorize]
    public class ListModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        public List<vmCategoryIndex> vmCategoryIndexList;

        public ListModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task OnGet()
        {
            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<vmNotification>(notificationJson);
            }
            vmCategoryIndexList = (await _categoryService.GetAllCategoriesService()).ToList();
        }
    }
}
