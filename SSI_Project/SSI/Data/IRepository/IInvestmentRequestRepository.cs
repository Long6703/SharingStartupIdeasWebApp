namespace SSI.Data.IRepository
{
    public interface IInvestmentRequestRepository
    {
        Task AddInvestmentRequestAsync(Models.InvestmentRequest investReq);
        Task<IQueryable<Models.InvestmentRequest>> GetAllInvestmentRequestAsync();
        Task<Models.InvestmentRequest> GetInvestmentRequestByIdAsync(int id);
        Task<IQueryable<Models.InvestmentRequest>> GetInvestmentRequestByInvestorIdAsync(int investorId);
        Task DeleteInvestmentRequestAsync(int requestId);
        Task UpdateInvestmentRequestAsync(Models.InvestmentRequest investReq);
    }
}
