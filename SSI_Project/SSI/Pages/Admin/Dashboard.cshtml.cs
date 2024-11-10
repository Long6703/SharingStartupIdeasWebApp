using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Models;
using SSI.Services.IService;
using SSI.Ultils.ViewModel;

namespace SSI.Pages.Admin
{
    [Authorize(Roles ="admin")]
    public class DashboardModel : PageModel
    {
        private readonly IAdminService adminService;
        private readonly IAdminIdeasService adminIdeasService;

        public IEnumerable<User> Users { get; set; } = new List<User>();
        public DashboardModel(IAdminService adminService, IAdminIdeasService adminIdeasService)
        {
            this.adminService = adminService;
            this.adminIdeasService = adminIdeasService;
        }
        public int NoOfUsers {  get; set; }

        public double AmountIncome {  get; set; }

        public int CountTransaction {  get; set; }

        public int CountIdeas {  get; set; }

        public IEnumerable<MonthlyStats> MonthlyStatistics { get; set; }
        public IEnumerable<UserMonthly> GetUserMonthlies {  get; set; }
        public void OnGet()
        {
            Users = adminService.GetAllUsers();
            NoOfUsers = Users.Count();
            AmountIncome =(double) adminService.SumAmountIncome();
            CountTransaction = adminService.CountTransaction();
            CountIdeas = adminService.CountIdeas();

            MonthlyStatistics = adminService.GetMonthlyRevenueAndIdeas();
            
            GetUserMonthlies = adminService.GetMonthlyInvestAndFounder();
        }
    }
}
