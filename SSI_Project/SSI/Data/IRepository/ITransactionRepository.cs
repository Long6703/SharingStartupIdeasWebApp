namespace SSI.Data.IRepository
{
    public interface ITransactionRepository
    {
        Task AddTransactionAsync(Models.Transaction transaction);
        Task<IQueryable<Models.Transaction>> GetAllTransactionAsync();
        Task<Models.Transaction> GetTransactionByIdAsync(int id);
        Task<IQueryable<Models.Transaction>> GetTransactionByUserIdAsync(int userId);
    }
}
