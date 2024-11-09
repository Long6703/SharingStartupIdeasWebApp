using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;

namespace SSI.Pages
{
    public class VietQrAdmin_ReturnModel : PageModel
    {
        private readonly ILogger<VietQrAdmin_ReturnModel> _logger;
        private readonly ITransactionService _transactionService;
        private readonly IInvestmentRequestService _investReqService;


        public string Result { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }

        public VietQrAdmin_ReturnModel(ILogger<VietQrAdmin_ReturnModel> logger, ITransactionService transactionService, IInvestmentRequestService investReqService)
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
                TransactionCode = requestId.ToString()+"-Send",
            };
            Models.InvestmentRequest invesReq = await _investReqService.GetInvestmentRequestByIdAsync(requestId);
            invesReq.Status = "completed";
            await _investReqService.UpdateInvestmentRequestAsync(invesReq);
            await _transactionService.AddTransactionAsync(transaction);

            return Page();
        }
    }
}
