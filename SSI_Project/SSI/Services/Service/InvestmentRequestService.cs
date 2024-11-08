using SSI.Data.IRepository;
using SSI.Data.Repository;
using SSI.Models;
using SSI.Services.IService;

namespace SSI.Services.Service
{
    public class InvestmentRequestService : IInvestmentRequestService
    {
        private readonly IInvestmentRequestRepository _investmentRequestRepository;

        public InvestmentRequestService(IInvestmentRequestRepository investmentRequestRepository)
        {
            _investmentRequestRepository = investmentRequestRepository;
        }

        public async Task AddInvestmentRequestAsync(Models.InvestmentRequest investReq)
        {
            await _investmentRequestRepository.AddInvestmentRequestAsync(investReq);
        }
        public async Task<IEnumerable<Idea>> GetIdeasWithInvestmentRequestsByFounderAsync(int founderUserId, int pageNumber, int pageSize)
        {
            return await _investmentRequestRepository.GetIdeasWithInvestmentRequestsByFounderAsync(founderUserId, pageNumber, pageSize);
        }
    }
}
