using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Web.Pages.Books
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public vmBookDetails vmBookDetails;

        public DetailsModel(IBookService bookService, ICategoryService categoryService, IMapper mapper)
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _mapper = mapper;
        }
        public async Task OnGet(int id)
        {
            var bookModel = await _bookService.GetBookByIdService(id);
            vmBookDetails = _mapper.Map<vmBookDetails>(bookModel);
            vmBookDetails.Category = _categoryService.GetCategoryByIdService(bookModel.CategoryId).Result.Name;
        }
    }
}
