using SSI.Data.IRepository;
using SSI.Models;

namespace SSI.Data.Repository
{
    public class AdminTransactionsRepository : IAdminTransactionsRepository
    {
        private readonly SSIV2Context _context;
        public AdminTransactionsRepository(SSIV2Context context) 
        {
            _context = context;
        }
        public IEnumerable<Transaction> GetTransactions()
        {
            return _context.Transactions.ToList();
        }
    }
}
