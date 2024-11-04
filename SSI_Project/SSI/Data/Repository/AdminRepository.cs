using Microsoft.EntityFrameworkCore;
using SSI.Data.IRepository;
using SSI.Models;

namespace SSI.Data.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly SSIV2Context _context;
        public AdminRepository(SSIV2Context context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public void LockAccount(int id)
        {
            var acc = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (acc != null)
            {
                acc.Status = "inactive";
                _context.Users.Update(acc);
            }
            _context.SaveChanges();
        }

        public void UnlockAccount(int id)
        {
            var acc = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (acc != null)
            {
                acc.Status = "active";
                _context.Users.Update(acc);
            }
            _context.SaveChanges();
        }
    }
}
