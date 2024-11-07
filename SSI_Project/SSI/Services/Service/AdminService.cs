using SSI.Data.IRepository;
using SSI.Models;
using SSI.Services.IService;

namespace SSI.Services.Service
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        public AdminService(IAdminRepository adminRepository) 
        { 
            _adminRepository = adminRepository;
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

        public decimal GetTotalAmountByUserId(int userId)
        {
            return _adminRepository.GetTotalAmount(userId);
        }

        public int GetTotalIdeasByUserId(int userId)
        {
            return _adminRepository.GetTotalIdeasByUserId(userId);
        }

        public User GetUser(int id)
        {
            return _adminRepository.GetUser(id);
        }

        public void LockAccount(int id)
        {
            _adminRepository.LockAccount(id);
        }

        public void UnlockAccount(int id)
        {
            _adminRepository.UnlockAccount(id);
        }
    }
}
