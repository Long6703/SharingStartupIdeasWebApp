namespace SSI.Data.IRepository
{
    public interface IInvestmentRequestRepository
    {
        Task AddInvestmentRequestAsync(Models.InvestmentRequest investReq);
    }
}
