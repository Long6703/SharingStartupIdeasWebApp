using SSI.Data.IRepository;
using SSI.Services.IService;

namespace SSI.Services.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task AddTransactionAsync(Models.Transaction transaction)
        {
            await _transactionRepository.AddTransactionAsync(transaction);
        }
        public async Task<IQueryable<Models.Transaction>> GetAllTransactionAsync()
        {
            return await _transactionRepository.GetAllTransactionAsync();
        }
        public async Task<Models.Transaction> GetTransactionByIdAsync(int id)
        {
            return await _transactionRepository.GetTransactionByIdAsync(id);
        }
        public async Task<IQueryable<Models.Transaction>> GetAllTransactionByUserIdAsync(int userId)
        {
            return await _transactionRepository.GetTransactionByUserIdAsync(userId);
        }
    }
}
