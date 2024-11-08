using SSI.Models;

namespace SSI.Services.IService
{
    public interface IInvestmentRequestService
    {
        Task AddInvestmentRequestAsync(Models.InvestmentRequest investReq);
        Task<IEnumerable<Idea>> GetIdeasWithInvestmentRequestsByFounderAsync(int founderUserId, int pageNumber, int pageSize);
    }
}
