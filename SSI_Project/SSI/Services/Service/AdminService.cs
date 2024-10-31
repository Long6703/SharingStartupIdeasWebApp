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
        
        public List<User> GetAllUsers()
        {
            return _adminRepository.GetAllUsers();
        }
    }
}
