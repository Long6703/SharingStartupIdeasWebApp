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
        public async Task<IEnumerable<Models.InvestmentRequest>> GetIdeasWithInvestmentRequestsByFounderAsync(int founderUserId)
        {
            return await _investmentRequestRepository.GetIdeasWithInvestmentRequestsByFounderAsync(founderUserId);
        }
        public async Task<IQueryable<Models.InvestmentRequest>> GetAllInvestmentRequestAsync()
        {
            return await _investmentRequestRepository.GetAllInvestmentRequestAsync();
        }
        public async Task<Models.InvestmentRequest> GetInvestmentRequestByIdAsync(int id)
        {
            return await _investmentRequestRepository.GetInvestmentRequestByIdAsync(id);
        }
        public async Task<IQueryable<Models.InvestmentRequest>> GetInvestmentRequestByInvestorIdAsync(int investorId)
        {
            return await _investmentRequestRepository.GetInvestmentRequestByInvestorIdAsync(investorId);
        }
        public async Task DeleteInvestmentRequestAsync(int requestId)
        {
            await _investmentRequestRepository.DeleteInvestmentRequestAsync(requestId);
        }
        public async Task UpdateInvestmentRequestAsync(Models.InvestmentRequest investReq)
        {
            await _investmentRequestRepository.UpdateInvestmentRequestAsync(investReq);
        }
    }
}
