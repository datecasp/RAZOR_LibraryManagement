using RAZOR_LibraryManagement.Models.Models;

namespace RAZOR_LibraryManagement.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryModel>> GetAllCategories();
        Task<CategoryModel> CreateCategory(CategoryModel categoryModel);
        Task<CategoryModel> GetCategoryById(int id);
    }
}
