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

        public IEnumerable<User> GetFounders()
        {
            return _adminRepository.GetAllUsers().Where(u => u.Role == "startup");
        }

        public IEnumerable<User> GetInvestors()
        {
            return _adminRepository.GetAllUsers().Where(u => u.Role == "investor");
        }
    }
}
