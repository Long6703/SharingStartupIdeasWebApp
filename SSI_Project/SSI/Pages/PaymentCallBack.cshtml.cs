using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;

namespace SSI.Pages
{
    public class PaymentCallBackModel : PageModel
    {
        private readonly ILogger<PaymentCallBackModel> _logger;
        private readonly IVnPayService _vnPayService;
        private readonly ITransactionService _transactionService;
        private readonly IInvestmentRequestService _investReqService;

        
        public string Result { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; }

        public PaymentCallBackModel(ILogger<PaymentCallBackModel> logger, IVnPayService vnPayService, ITransactionService transactionService, IInvestmentRequestService investReqService)
        {
            _logger = logger;
            _vnPayService = vnPayService;
            _transactionService = transactionService;
            _investReqService = investReqService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            return await HandleCallbackAsync();
        }

        public async Task<IActionResult> HandleCallbackAsync()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            if (response == null || response.VnPayResponseCode != "00")
            {
                Result = $"Lỗi thanh toán VN Pay: {response.VnPayResponseCode}";
            }
            else
            {
                Result = $"Thanh toán VNPay thành công";

                Amount = Convert.ToDecimal(response.Amount);
                PaymentMethod = response.PaymentMethod;
                TransactionDate = DateTime.Now;

                var username = HttpContext.Session.GetString("user");

                var transaction = new Models.Transaction
                {
                    InvestmentRequestId = response.OrderId,
                    Amount = Amount,             
                    Status = "completed",
                    CreatedAt = TransactionDate,
                    TransactionCode = response.VnPayResponseCode
                };
                Models.InvestmentRequest invesReq = await _investReqService.GetInvestmentRequestByIdAsync(response.OrderId);
                invesReq.Status = "completed";
                await _investReqService.UpdateInvestmentRequestAsync(invesReq);
                await _transactionService.AddTransactionAsync(transaction);
            }
            return Page();
        }
    }
}
