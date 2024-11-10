using Microsoft.EntityFrameworkCore;
using SSI.Data.IRepository;
using SSI.Models;

namespace SSI.Data.Repository
{
    public class TransactionRepository : RepositoryBase<Models.Transaction>, ITransactionRepository
    {
        public TransactionRepository(SSIV2Context context) : base(context)
        {
        }
        public async Task AddTransactionAsync(Models.Transaction transaction)
        {
            await AddAsync(transaction);

            SaveChanges();
        }
        public async Task<IQueryable<Models.Transaction>> GetAllTransactionAsync()
        {
            return await Task.FromResult(GetAll());
        }
        public async Task<Models.Transaction> GetTransactionByIdAsync(int id)
        {
            return await GetByIdAsync(id);
        }
        public async Task<IQueryable<Models.Transaction>> GetTransactionByUserIdAsync(int userId)
        {
            return await Task.FromResult(_dbset.Where(t => t.TransactionId == userId));
        }
    }
}
