using Microsoft.EntityFrameworkCore;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Domain.Models;
using RAZOR_LibraryManagement.Infra.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAZOR_LibraryManagement.Infra.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly LM_DbContext _lM_DbContext;

        public CategoryRepository(LM_DbContext lM_DbContext)
        {
            _lM_DbContext = lM_DbContext;
        }

        public async Task<Category> CreateCategory(Category category)
        {
            try
            {
                _lM_DbContext.Categories.Add(category);
                await _lM_DbContext.SaveChangesAsync();
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
                result = await _lM_DbContext.Categories.ToListAsync();
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
