using Microsoft.EntityFrameworkCore;
using SSI.Data.IRepository;
using SSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.Data.Repository
{
    public class IdeaRepository : IIdeaRepository
    {
        private readonly SSIV2Context _context;
        public IdeaRepository(SSIV2Context context)
        {
            _context = context;
        }
        public List<Idea> SearchIdeas(string searchTerm, int? categoryId)
        {
            var query = _context.Ideas
                .Include(i => i.Category)
                .Include(i => i.Ideadetails)
                .ThenInclude(d=>d.Images)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(i => i.Title.Contains(searchTerm));
            }

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(i => i.CategoryId == categoryId.Value);
            }

            return query.ToList();
        }
        public (Idea, int) GetIdeaById(int id)
        {
            var idea = _context.Ideas
        .Include(i => i.Category)
        .Include(i => i.Ideadetails)
            .ThenInclude(d => d.Images)
        .Include(i => i.Ideadetails)
            .ThenInclude(d => d.Comments)
        .Include(u=>u.User)
        .FirstOrDefault(i => i.IdeaId == id);
            int commentCount = idea?.Ideadetails.SelectMany(d => d.Comments).Count() ?? 0;

            return (idea, commentCount);
        }
    }
}
