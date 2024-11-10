using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;

namespace SSI.Pages
{
    [Authorize(Roles = "investor")]
    public class VietQr_ReturnModel : PageModel
    {
        private readonly ILogger<VietQr_ReturnModel> _logger;
        private readonly ITransactionService _transactionService;
        private readonly IInvestmentRequestService _investReqService;


        public string Result { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }

        public VietQr_ReturnModel(ILogger<VietQr_ReturnModel> logger, ITransactionService transactionService, IInvestmentRequestService investReqService)
        {
            _logger = logger;
            _transactionService = transactionService;
            _investReqService = investReqService;
        }

        public async Task<IActionResult> OnGetAsync(decimal amount, int requestId)
        {
            return await HandleCallbackAsync(amount, requestId);
        }

        public async Task<IActionResult> HandleCallbackAsync(decimal amount, int requestId)
        {          
                
                TransactionDate = DateTime.Now;

                var username = HttpContext.Session.GetString("user");

                var transaction = new Models.Transaction
                {
                    InvestmentRequestId = requestId,
                    Amount = amount,
                    Status = "completed",
                    CreatedAt = TransactionDate,
                    TransactionCode = requestId.ToString()+"-Receive",
                };
                Models.InvestmentRequest invesReq = await _investReqService.GetInvestmentRequestByIdAsync(requestId);
                invesReq.Status = "Investor thanh toan";
                await _investReqService.UpdateInvestmentRequestAsync(invesReq);
                await _transactionService.AddTransactionAsync(transaction);
            
            return Page();
        }
    }
}
