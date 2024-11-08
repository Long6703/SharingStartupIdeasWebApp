using SSI.Models;

namespace SSI.Data.IRepository
{
    public interface IInvestmentRequestRepository
    {
        Task AddInvestmentRequestAsync(Models.InvestmentRequest investReq);
        Task<IEnumerable<Idea>> GetIdeasWithInvestmentRequestsByFounderAsync(int founderUserId, int pageNumber, int pageSize);
    }
}
