using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;

namespace SSI.Pages
{
    [Authorize(Roles = "investor")]
    public class InvestmentRequestDetailModel : PageModel
    {
        private readonly ILogger<InvestmentRequestDetailModel> _logger;
        private readonly ITransactionService _transactionService;
        private readonly IInvestmentRequestService _investmentRequestService;
        public InvestmentRequestDetailModel(ILogger<InvestmentRequestDetailModel> logger, ITransactionService transactionService, IInvestmentRequestService investmentRequestService)
        {
            _logger = logger;
            _transactionService = transactionService;
            _investmentRequestService = investmentRequestService;
        }

        public Models.InvestmentRequest InvestmentRequest { get; set; }
        
        public async Task<IActionResult> OnGetAsync(int RequestId)
        {
            InvestmentRequest = await _investmentRequestService.GetInvestmentRequestByIdAsync(RequestId);
            
            if (InvestmentRequest == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
