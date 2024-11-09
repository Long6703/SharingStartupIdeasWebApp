using Microsoft.EntityFrameworkCore;
using SSI.Data.IRepository;
using SSI.Models;
using System.ComponentModel.DataAnnotations;

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

        public int CountNoSuccesInvest(int userId)
        {
            var count = (from i in _context.InvestmentRequests 
                         join t in _context.Transactions on i.RequestId equals t.InvestmentRequestId
                        where i.UserId == userId && i.Status == "approved" && t.Status == "completed"
                         select i).Count();
            return count;               
        }

        public int CountNoRejecrInvest(int userId)
        {
            var count = (from i in _context.InvestmentRequests
                         where i.UserId == userId && i.Status == "rejected"
                         select i).Count();
            return count;
        }

        public decimal SumAmountInvest(int investorId)
        {
            var total = (from iv in _context.InvestmentRequests
                         join t in _context.Transactions on iv.RequestId equals t.InvestmentRequestId
                         where iv.UserId == investorId
                            && iv.Status == "approved"
                            && t.Status == "completed"
                         select t.Amount
                         ).Sum();
            return total;
        }
    }
}
