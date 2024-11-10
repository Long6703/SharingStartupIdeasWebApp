using SSI.Models;

namespace SSI.Services.IService
{
    public interface IInvestmentRequestService
    {
        Task AddInvestmentRequestAsync(Models.InvestmentRequest investReq);
        Task<IEnumerable<Models.InvestmentRequest>> GetIdeasWithInvestmentRequestsByFounderAsync(int founderUserId);
        Task<IQueryable<Models.InvestmentRequest>> GetAllInvestmentRequestAsync();
        Task<Models.InvestmentRequest> GetInvestmentRequestByIdAsync(int id);
        Task<IQueryable<Models.InvestmentRequest>> GetInvestmentRequestByInvestorIdAsync(int investorId);
        Task DeleteInvestmentRequestAsync(int requestId);
        Task UpdateInvestmentRequestAsync(Models.InvestmentRequest investReq);
    }
}
