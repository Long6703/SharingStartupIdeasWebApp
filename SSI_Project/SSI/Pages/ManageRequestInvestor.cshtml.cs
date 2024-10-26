using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Models;
using SSI.Services.IService;

namespace SSI.Pages
{
    public class ManageRequestInvestorModel : PageModel
    {
        private readonly ILogger<ManageRequestInvestorModel> _logger;
        private readonly IInvestmentRequestService _investReqService;
        public ManageRequestInvestorModel(ILogger<ManageRequestInvestorModel> logger, IInvestmentRequestService investReqService)
        {
            _logger = logger;
            _investReqService = investReqService;
        }
        public List<InvestmentRequest> InvestmentRequests { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var username = HttpContext.Session.GetString("user");
            var investorId = 2; 
            var investRequests = await _investReqService.GetInvestmentRequestByInvestorIdAsync(investorId);
            InvestmentRequests = investRequests.ToList();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _investReqService.DeleteInvestmentRequestAsync(id);
            return RedirectToPage("/Index");
        }
    }
}
