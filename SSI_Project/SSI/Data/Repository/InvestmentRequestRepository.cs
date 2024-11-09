using Microsoft.EntityFrameworkCore;
using SSI.Data.IRepository;
using SSI.Models;

namespace SSI.Data.Repository
{
    public class InvestmentRequestRepository : RepositoryBase<Models.InvestmentRequest>, IInvestmentRequestRepository
    {
        private readonly SSIV2Context _context;
        public InvestmentRequestRepository(SSIV2Context context) : base(context)
        {
            _context = context;
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
        public async Task<IQueryable<Models.InvestmentRequest>> GetAllInvestmentRequestAsync()
        {
            return await Task.FromResult(_dbset.Include(t => t.Idea).Include(t => t.Transactions));
        }
        public async Task<Models.InvestmentRequest?> GetInvestmentRequestByIdAsync(int id)
        {
            return await _context.InvestmentRequests.
                Include(i => i.Idea).
                    ThenInclude(c=>c.User).
                Include(i=>i.Idea).
                    ThenInclude(c=>c.Category).
                Include(i => i.Transactions).FirstOrDefaultAsync(i => i.RequestId == id);
        }
        public async Task<IQueryable<Models.InvestmentRequest>> GetInvestmentRequestByInvestorIdAsync(int investorId)
        {
            return await Task.FromResult(_dbset.Include(t=>t.Idea).Include(t => t.Transactions).Where(t => t.UserId == investorId));
        }
        public async Task DeleteInvestmentRequestAsync(int requestId)
        {
            // Find the request by its ID
            var investReq = await _context.InvestmentRequests.FindAsync(requestId);

            if (investReq != null)
            {
                // Remove the request from the database
                _context.InvestmentRequests.Remove(investReq);
                await _context.SaveChangesAsync(); ;
            }
        }
        public async Task UpdateInvestmentRequestAsync(Models.InvestmentRequest investReq)
        {
            var existingRequest = await _context.InvestmentRequests.FindAsync(investReq.RequestId);
            if (existingRequest != null)
            {
                existingRequest.Amount = investReq.Amount;
                existingRequest.EquityPercentage = investReq.EquityPercentage;
                existingRequest.InvestmentPeriod = investReq.InvestmentPeriod;
                existingRequest.Description = investReq.Description;
                existingRequest.Status = investReq.Status;
                existingRequest.CreatedAt = DateTime.Now;

                await _context.SaveChangesAsync();
            }
        }
    }
}
