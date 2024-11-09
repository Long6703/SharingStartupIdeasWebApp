using SSI.Data.IRepository;
using SSI.Models;
using SSI.Services.IService;

namespace SSI.Services.Service
{
    public class AdminTransactionService : IAdminTransactionService
    {
        private readonly IAdminTransactionsRepository _repository;
        public AdminTransactionService(IAdminTransactionsRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            return _repository.GetTransactions();
        }
    }
}
