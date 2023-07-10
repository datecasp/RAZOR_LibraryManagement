using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Lang.Book;
using RAZOR_LibraryManagement.Lang.Category;
using RAZOR_LibraryManagement.Models.Entities;
using RAZOR_LibraryManagement.Models.Models;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<vmNotification> CreateCategoryService(CategoryModel categoryModel)
        {
            var vmNotification = new vmNotification();
            try
            {
                var categoryResult = await _unitOfWork.CategoryRepository.CreateCategory(categoryModel);
                _unitOfWork.Save();
                if(categoryResult != null)
                {
                    vmNotification.Type = Lang.Notification.NotificationType.Success;
                    vmNotification.Message = "Category created successfully";
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

        public async Task<IEnumerable<CategoryModel>> GetAllCategoriesService()
        {
            var categoriesList = new List<CategoryModel>();
            try
            {
                var repo = _unitOfWork.GetRepository<Category>();
                categoriesList = repo.GetAllProfiled<CategoryModel>().Result.ToList();
            }
            catch (Exception ex)
            {

            }
            return categoriesList;
        }

        public async Task<IEnumerable<CategoryModel>> GetActiveCategoriesService()
        {
            var categoriesList = new List<CategoryModel>();
            try
            {
                categoriesList = (await _unitOfWork.CategoryRepository.GetAllCategories())
                    .Where(c => c.IsActive)
                    .ToList();
            }
            catch (Exception ex)
            {

            }
            return categoriesList;
        }

        public async Task<CategoryModel> GetCategoryByIdService(int id)
        {
            var result = new CategoryModel();
            try
            {
                var repo = _unitOfWork.GetRepository<Category>();
                result = repo.GetByIdProfiled<CategoryModel>(id).Result;
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}
