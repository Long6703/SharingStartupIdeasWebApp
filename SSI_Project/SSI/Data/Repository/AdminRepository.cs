using Microsoft.EntityFrameworkCore;
using SSI.Data.IRepository;
using SSI.Models;
using SSI.Ultils.ViewModel;
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

        public decimal SumAmountIncome()
        {
            var startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
  
            var total = (from t in _context.Transactions
                         where t.Status == "completed" && t.CreatedAt >= startOfMonth
                         select t.Amount)
                         .Sum();

            return total * 0.1m;
        }

        public int CountTransaction()
        {
            var startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
   
            return _context.Transactions
                .Where(t => t.Status == "completed" && t.CreatedAt >= startOfMonth )
                .Count();
        }

        public int CountIdeas()
        {
            var startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
         
            return _context.Ideas
                .Where(i => i.CreatedAt >= startOfMonth )
                .Count();
        }

        public IEnumerable<MonthlyStats> GetMonthlyRevenueAndIdeas()
        {
            return _context.Transactions
                 .GroupBy(t => new { Year = t.CreatedAt.Value.Year, Month = t.CreatedAt.Value.Month })
            .Select(g => new MonthlyStats
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                TotalRevenue = g.Where(t => t.Status == "completed").Sum(t => t.Amount),
                TotalIdeas = g.Count()
            })
            .OrderBy(ms => ms.Year)
            .ThenBy(ms => ms.Month)
            .ToList();
        }

        public IEnumerable<UserMonthly> GetMonthlyInvestAndFounder()
        {
            return _context.Users
                .GroupBy(u => new { Year = u.CreatedAt.Value.Year, Month = u.CreatedAt.Value.Month })
                .Select(g => new UserMonthly 
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalInvestors = g.Count(u => u.Role == "investor"),
                    TotalFounder = g.Count(u => u.Role == "startup")
                })
                .OrderBy(r => r.Year)
                .ThenBy(r => r.Month)
                .ToList();
        }
    }
}
