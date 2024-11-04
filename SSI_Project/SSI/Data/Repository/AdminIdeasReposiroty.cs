using SSI.Data.IRepository;
using SSI.Models;

namespace SSI.Data.Repository
{
    public class AdminIdeasReposiroty : IAdminIdeasRepository
    {
        private readonly SSIV2Context _context;
        public AdminIdeasReposiroty(SSIV2Context context) 
        {
            _context = context;
        }
        public IEnumerable<Idea> GetIdeas()
        {
            return _context.Ideas.ToList();
        }
    }
}
