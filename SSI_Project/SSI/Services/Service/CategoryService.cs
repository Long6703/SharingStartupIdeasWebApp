﻿using SSI.Data.Repository;
using SSI.Models;

namespace SSI.Services.Service
{
    public interface ICategoryService
    {
        List<Category> GetAllCategories();
    }
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public List<Category> GetAllCategories()
        {
            return _categoryRepository.GetAllCategories();
        }
    }
}
