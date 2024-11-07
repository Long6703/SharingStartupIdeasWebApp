namespace SSI.Services.IService
{
    public interface IAdminTransactionService
    {
        IEnumerable<Models.Transaction> GetTransactions();
    }
}
