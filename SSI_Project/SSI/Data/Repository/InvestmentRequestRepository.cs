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
            await SaveChangesAsync();
        }
        public async Task<IQueryable<Models.InvestmentRequest>> GetAllInvestmentRequestAsync()
        {
            return await Task.FromResult(GetAll());
        }
        public async Task<Models.InvestmentRequest> GetInvestmentRequestByIdAsync(int id)
        {
            return await GetByIdAsync(id);
        }
        public async Task<IQueryable<Models.InvestmentRequest>> GetInvestmentRequestByInvestorIdAsync(int investorId)
        {
            return await Task.FromResult(_dbset.Where(t => t.UserId == investorId));
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
