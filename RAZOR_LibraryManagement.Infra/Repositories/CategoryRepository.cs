﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Infra.DataContext;
using RAZOR_LibraryManagement.Models.Entities;
using RAZOR_LibraryManagement.Models.Models;

namespace RAZOR_LibraryManagement.Infra.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly LM_DbContext _lM_DbContext;
        private readonly IMapper _mapper;

        public CategoryRepository(LM_DbContext lM_DbContext, IMapper mapper)
        {
            _lM_DbContext = lM_DbContext;
            _mapper = mapper;
        }

        public async Task<CategoryModel> CreateCategory(CategoryModel categoryModel)
        {
            var category = _mapper.Map<Category>(categoryModel);
            try
            {
                var result = _lM_DbContext.Categories.Add(category);
                if (result != null)
                {
                    return _mapper.Map<CategoryModel>(result.Entity);
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public async Task<IEnumerable<CategoryModel>> GetAllCategories()
        {

            var result = new List<CategoryModel>();
            try
            {
                var categoryList = await _lM_DbContext.Categories.Distinct().ToListAsync();
                result = _mapper.Map<List<CategoryModel>>(categoryList);
            }
            catch (Exception ex)
            {

            }
            return result;

        }

        public async Task<CategoryModel> GetCategoryById(int id)
        {
            var result = new CategoryModel();
            try
            {
                var category = await _lM_DbContext.FindAsync<Category>(id);
                result = _mapper.Map<CategoryModel>(category);
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}
