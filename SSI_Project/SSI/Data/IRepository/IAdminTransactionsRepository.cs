namespace SSI.Data.IRepository
{
    public interface IAdminTransactionsRepository
    {
        IEnumerable<Models.Transaction> GetTransactions();
    }
}
