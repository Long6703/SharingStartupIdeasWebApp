﻿using SSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.Data.Repository
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
    }
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SSIV3Context _context;

        public CategoryRepository(SSIV3Context context)
        {
            _context = context;
        }

        public List<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }
    }
}
