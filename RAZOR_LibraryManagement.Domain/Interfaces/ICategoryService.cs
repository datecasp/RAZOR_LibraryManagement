using RAZOR_LibraryManagement.Models.Models;
using RAZOR_LibraryManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAZOR_LibraryManagement.Domain.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryModel>> GetAllCategoriesService();
        Task<IEnumerable<CategoryModel>> GetActiveCategoriesService();
        Task<CategoryModel> GetCategoryByIdService(int id);
        Task<vmNotification> CreateCategoryService(CategoryModel categoryModel);

    }
}
