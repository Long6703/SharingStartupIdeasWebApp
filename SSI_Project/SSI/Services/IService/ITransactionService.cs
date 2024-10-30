namespace SSI.Services.IService
{
    public interface ITransactionService
    {
        Task AddTransactionAsync(Models.Transaction transaction);
        Task<IQueryable<Models.Transaction>> GetAllTransactionAsync();
        Task<Models.Transaction> GetTransactionByIdAsync(int id);
        Task<IQueryable<Models.Transaction>> GetAllTransactionByUserIdAsync(int userId);
    }
}
