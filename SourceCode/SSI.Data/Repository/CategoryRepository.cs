using SSI.Share.Domain;
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
        private readonly SSIContext _context;

        public CategoryRepository(SSIContext context)
        {
            _context = context;
        }

        public List<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }
    }
}
