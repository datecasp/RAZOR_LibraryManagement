using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Infra.DataContext;
using RAZOR_LibraryManagement.Models.Entities;

namespace RAZOR_LibraryManagement.Infra.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly LM_DbContext _lM_DbContext;
        private readonly IMapper _mapper;

        public CategoryRepository(LM_DbContext lM_DbContext, IMapper mapper) : base(lM_DbContext, mapper)
        {
            _lM_DbContext = lM_DbContext;
            _mapper = mapper;
        }

        public async Task<Category> CreateCategory(Category category)
        {
            try
            {
                _lM_DbContext.Categories.Add(category);
                return category;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            var result = new List<Category>();
            try
            {
                result = await _lM_DbContext.Categories.Distinct().ToListAsync();
            }
            catch (Exception ex)
            {

            }
            return result;

        }

        public async Task<Category> GetCategoryById(int id)
        {
            var result = new Category();
            try
            {
                result = await _lM_DbContext.FindAsync<Category>(id);
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}
