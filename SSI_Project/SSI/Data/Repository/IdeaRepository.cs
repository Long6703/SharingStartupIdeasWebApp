using Microsoft.EntityFrameworkCore;
using SSI.Data.IRepository;
using SSI.Models;

namespace SSI.Data.Repository
{
    public class IdeaRepository : IIdeaRepository
    {
        private readonly SSIV2Context _context;

        public IdeaRepository(SSIV2Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Idea>> GetIdeasByUserIdAndRoleAsync(int userId, string role, int pageNumber, int pageSize)
        {
            return await _context.Ideas
                .Include(i => i.User)
                .Include(i => i.Category)
                .Where(i => i.UserId == userId && i.User.Role == role)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalIdeasCountByUserIdAndRoleAsync(int userId, string role)
        {
            return await _context.Ideas
                .Include(i => i.User)
                .CountAsync(i => i.UserId == userId && i.User.Role == role);
        }
        public async Task<Idea?> GetIdeaWithDetailsAsync(int ideaId)
        {
            return await _context.Ideas
                .Include(i => i.Ideadetails)
                    .ThenInclude(d => d.Images)
                .Include(i => i.Ideadetails)
                    .ThenInclude(d => d.Comments)
                        .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(i => i.IdeaId == ideaId);
        }
        public async Task<Ideadetail?> GetMilestoneDetailByIdAsync(int ideaDetailId)
        {
            return await _context.Ideadetails
                .Include(d => d.Images)
                .Include(d => d.Comments)
                    .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(d => d.IdeaDetailId == ideaDetailId);
        }
    }
}
