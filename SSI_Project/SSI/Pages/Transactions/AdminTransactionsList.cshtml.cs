using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;

namespace SSI.Pages.Transactions
{
    [Authorize(Roles = "admin")]
    public class AdminTransactionsListModel : PageModel
    {
        private readonly IAdminTransactionService service;
        private readonly IAdminService adminService;
        private readonly IAdminIdeasService ideasService;
        public AdminTransactionsListModel(IAdminTransactionService service, IAdminService adminService, IAdminIdeasService adminIdeas)
        {
            this.service = service;
            this.adminService = adminService;
            this.ideasService = adminIdeas;
        }
        public Models.User User { get; set; } = new Models.User();
        public Models.Idea Idea { get; set; } = new Models.Idea();
        public Models.InvestmentRequest InvestmentRequest { get; set; } = new Models.InvestmentRequest();
        public IEnumerable<Models.Transaction> Transactions { get; set; } = new List<Models.Transaction>();
        public void OnGet()
        {
            Transactions = service.GetTransactions();
            foreach (var i in Transactions) 
            {
                InvestmentRequest = ideasService.GetInvestmentRequestById(i.InvestmentRequestId);
                i.InvestmentRequest = InvestmentRequest;
                User = adminService.GetUser(InvestmentRequest.UserId);
                Idea = ideasService.GetIdeaById(InvestmentRequest.IdeaId);
                i.InvestmentRequest.Idea = Idea;
                i.InvestmentRequest.User = User;
            }
        }
    }
}
