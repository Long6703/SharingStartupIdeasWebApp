using SSI.Data.IRepository;
using SSI.Models;

namespace SSI.Data.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly SSIV3Context _context;
        public AdminRepository(SSIV3Context context)
        {
            _context = context;
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

    }
}
