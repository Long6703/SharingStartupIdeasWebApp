using SSI.Data.IRepository;
using SSI.Models;
using SSI.Services.IService;
using SSI.Ultils.ViewModel;

namespace SSI.Services.Service
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        public AdminService(IAdminRepository adminRepository) 
        { 
            _adminRepository = adminRepository;
        }

        public int CountIdeas()
        {
            return _adminRepository.CountIdeas();
        }

        public int CountNoRejecrInvest(int investorId)
        {
            return _adminRepository.CountNoRejecrInvest(investorId);
        }

        public int CountNoSuccesInvest(int investorId)
        {
            return _adminRepository.CountNoSuccesInvest(investorId);
        }

        public int CountTransaction()
        {
            return _adminRepository.CountTransaction();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _adminRepository.GetAllUsers();
        }

        public Category GetCategory(int id)
        {
            return _adminRepository.GetCategory(id);
        }

        public IEnumerable<User> GetFounders()
        {
            return _adminRepository.GetAllUsers().Where(u => u.Role == "startup");
        }

        public IEnumerable<User> GetInvestors()
        {
            return _adminRepository.GetAllUsers().Where(u => u.Role == "investor");
        }

        public IEnumerable<UserMonthly> GetMonthlyInvestAndFounder()
        {
            return _adminRepository.GetMonthlyInvestAndFounder();
        }

        public IEnumerable<MonthlyStats> GetMonthlyRevenueAndIdeas()
        {
            return _adminRepository.GetMonthlyRevenueAndIdeas();
        }

        public User GetUser(int id)
        {
            return _adminRepository.GetUser(id);
        }

        public void LockAccount(int id)
        {
            _adminRepository.LockAccount(id);
        }

        public decimal SumAmountIncome()
        {
            return _adminRepository.SumAmountIncome();
        }

        public decimal SumAmountInvest(int investorId)
        {
            return _adminRepository.SumAmountInvest(investorId);
        }

        public void UnlockAccount(int id)
        {
            _adminRepository.UnlockAccount(id);
        }
    }
}
