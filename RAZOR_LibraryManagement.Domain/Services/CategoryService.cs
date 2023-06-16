using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.ViewModels;
using RAZOR_LibraryManagement.Models.Entities;

namespace RAZOR_LibraryManagement.Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork
            ;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<vmCategoryIndex> CreateCategoryService(vmCategoryIndex vmCategoryIndex)
        {
            var vmCategory = new vmCategoryIndex();
            var category = new Category
            { 
                Name = vmCategoryIndex.Name,
                IsActive = (bool)vmCategoryIndex.IsActive
            };
            try
            {
                var categoryResult = await _categoryRepository.CreateCategory(category);
                vmCategory.Name = categoryResult.Name;
                vmCategory.IsActive = (bool)categoryResult.IsActive;
            }
            catch (Exception ex)
            {

            }
            return vmCategory;
        }

        public async Task<IEnumerable<vmCategoryIndex>> GetAllCategoriesService()
        {
            var categorysList = new List<Category>();
            var categoryIndexList = new List<vmCategoryIndex>();
            try
            {
                categorysList = (await _categoryRepository.GetAllCategories()).ToList();
                
                foreach (var category in categorysList)
                {
                    var vwCategory = new vmCategoryIndex
                    {
                        Name = category.Name,
                        IsActive = category.IsActive
                    };

                    categoryIndexList.Add(vwCategory);
                }
            }
            catch (Exception ex)
            {

            }
            return categoryIndexList;
        }

        public async Task<IEnumerable<vmCategoryIndex>> GetActiveCategoriesService()
        {
            var categorysList = new List<Category>();
            var categoryIndexList = new List<vmCategoryIndex>();
            try
            {
                categorysList = (await _categoryRepository.GetAllCategories())
                    .Where(c => c.IsActive)
                    .ToList();
                foreach (var category in categorysList)
                {
                    var vwCategory = new vmCategoryIndex
                    {
                        Name = category.Name,
                        IsActive = category.IsActive
                    };

                    categoryIndexList.Add(vwCategory);
                }
            }
            catch (Exception ex)
            {

            }
            return categoryIndexList;
        }
    }
}
