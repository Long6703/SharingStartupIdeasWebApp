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
        public (Idea, int, List<Comment>) GetIdeaById(int id)
        {
            // Retrieve the idea with basic relationships
            var idea = _context.Ideas
                .Include(i => i.Category)
                .Include(i => i.User)
                .FirstOrDefault(i => i.IdeaId == id);

            if (idea == null)
            {
                return (null, 0, new List<Comment>()); // Return default values if idea is not found
            }

            // Load additional related data for idea details and comments with users
            _context.Entry(idea)
                .Collection(i => i.Ideadetails)
                .Query()
                .Include(d => d.Images)
                .Include(d => d.Comments)
                    .ThenInclude(c => c.User) // Include the User for each Comment
                .Load();

            int commentCount = idea.Ideadetails.SelectMany(d => d.Comments).Count();
            var comments = idea.Ideadetails.SelectMany(d => d.Comments).ToList();

            return (idea, commentCount, comments);
        }
    }
}
