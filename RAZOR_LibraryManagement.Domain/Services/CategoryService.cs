using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.ViewModels;
using RAZOR_LibraryManagement.Models.Entities;

namespace RAZOR_LibraryManagement.Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork
            ;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<vmNotification> CreateCategoryService(vmCategoryIndex vmCategoryIndex)
        {
            var vmNotification = new vmNotification();
            var category = new Category
            { 
                Name = vmCategoryIndex.Name,
                IsActive = (bool)vmCategoryIndex.IsActive
            };
            try
            {
                var categoryResult = await _unitOfWork.CategoryRepository.CreateCategory(category);
                _unitOfWork.Save();
                if(categoryResult != null)
                {
                    vmNotification.Type = Lang.Notification.NotificationType.Success;
                    vmNotification.Message = "User created successfully";
                    return vmNotification;
                }
              }
            catch (Exception ex)
            {
                vmNotification.Type = Lang.Notification.NotificationType.Error;
                vmNotification.Message = "Exception thrown! " + ex.Message;
                return vmNotification;
            }
            vmNotification.Type = Lang.Notification.NotificationType.Error;
            vmNotification.Message = "Hmmm something went wrong here....";
            return vmNotification;
        }

        public async Task<IEnumerable<vmCategoryIndex>> GetAllCategoriesService()
        {
            var categorysList = new List<Category>();
            var categoryIndexList = new List<vmCategoryIndex>();
            try
            {
                categorysList = (await _unitOfWork.CategoryRepository.GetAllCategories()).ToList();
                
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
                categorysList = (await _unitOfWork.CategoryRepository.GetAllCategories())
                    .Where(c => c.IsActive)
                    .ToList();
                foreach (var category in categorysList)
                {
                    var vwCategory = new vmCategoryIndex
                    {
                        CatId = category.CategoryId,
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
