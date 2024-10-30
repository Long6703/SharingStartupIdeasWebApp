using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;

namespace SSI.Pages
{
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
        //public Models.Idea Idea { get; set; }
        public List<Models.Transaction> Transaction { get; set; }
        public async Task<IActionResult> OnGetAsync(int RequestId)
        {
            InvestmentRequest = await _investmentRequestService.GetInvestmentRequestByIdAsync(RequestId);
            //Idea = get idea by ideaId;
            //get transaction by requestid
            Transaction = InvestmentRequest.Transactions.ToList();
            if (InvestmentRequest == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
