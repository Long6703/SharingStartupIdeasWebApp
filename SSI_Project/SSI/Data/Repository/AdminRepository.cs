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

        public Models.User GetUser(int id) 
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            return user;
        }

        public Category GetCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
            return category;
        }

        public decimal GetTotalAmount(int id)
        {
            return _context.InvestmentRequests.Where(i => i.UserId == id && i.Status == "approved").Sum(i => i.Amount);
        }

        public int GetTotalIdeasByUserId(int userId)
        {
            var totalIdeas = _context.InvestmentRequests
            .Where(ir => ir.UserId == userId && ir.Status == "approved")
            .Select(ir => ir.IdeaId) 
            .Distinct() 
            .Count();
            return totalIdeas;
        }
    }
}
