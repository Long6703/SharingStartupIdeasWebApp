using SSI.Data.IRepository;
using SSI.Models;

namespace SSI.Data.Repository
{
    public class InvestmentRequestRepository : RepositoryBase<Models.InvestmentRequest>, IInvestmentRequestRepository
    {
        public InvestmentRequestRepository(SSIV3Context context) : base(context)
        {
        }
        public async Task AddInvestmentRequestAsync(Models.InvestmentRequest investReq)
        {
            await AddAsync(investReq);
            SaveChanges();
        }
    }
}
