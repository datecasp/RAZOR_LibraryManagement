using RAZOR_LibraryManagement.Domain.Models;

namespace RAZOR_LibraryManagement.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> CreateCategory(Category category);
        Task<Category> GetCategoryById(int id);
    }
}
