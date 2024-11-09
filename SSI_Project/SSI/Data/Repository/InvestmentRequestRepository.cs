using Microsoft.EntityFrameworkCore;
using SSI.Data.IRepository;
using SSI.Models;

namespace SSI.Data.Repository
{
    public class InvestmentRequestRepository : RepositoryBase<Models.InvestmentRequest>, IInvestmentRequestRepository
    {
        public InvestmentRequestRepository(SSIV2Context context) : base(context)
        {
        }
        public async Task AddInvestmentRequestAsync(Models.InvestmentRequest investReq)
        {
            await AddAsync(investReq);
            SaveChanges();
        }
        public async Task<IEnumerable<Idea>> GetIdeasWithInvestmentRequestsByFounderAsync(int founderUserId, int pageNumber, int pageSize)
        {
            return await _context.Ideas
                .Where(idea => idea.UserId == founderUserId && idea.InvestmentRequests.Any())
                .Include(idea => idea.InvestmentRequests)
                    .ThenInclude(request => request.User)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
