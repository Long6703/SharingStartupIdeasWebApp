namespace SSI.Services.IService
{
    public interface IInvestmentRequestService
    {
        Task AddInvestmentRequestAsync(Models.InvestmentRequest investReq);
    }
}
