using SSI.Data.IRepository;
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
    }
}
