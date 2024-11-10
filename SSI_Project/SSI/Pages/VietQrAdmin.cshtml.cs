using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;

namespace SSI.Pages
{
    public class VietQrAdminModel : PageModel
    {
        private readonly ILogger<VietQrAdminModel> _logger;
        private readonly IInvestmentRequestService _investReqService;


        [BindProperty(SupportsGet = true)]
        public double Amount { get; set; }
        [BindProperty(SupportsGet = true)]
        public int RequestId { get; set; }
        public string PaymentUrl { get; set; }
        public string Info { get; set; }
        public VietQrAdminModel(ILogger<VietQrAdminModel> logger, IInvestmentRequestService investReqService)
        {
            _logger = logger;
            _investReqService = investReqService;

        }

        public async Task<IActionResult> OnGetAsync()
        {
            return await HandlePaymentAsync();
        }
        public async Task<IActionResult> HandlePaymentAsync()
        {
            var investReq = await _investReqService.GetInvestmentRequestByIdAsync(RequestId);
            String url = "https://img.vietqr.io/image/";
            String bankName = investReq.Idea.User.BankName;
            String stk = investReq.Idea.User.BankAccountNumber;
            String amount = Amount.ToString();
            String addInfo = " Thanh toan den founder " + RequestId;
            Info = addInfo;
            String accountName = investReq.Idea.User.Displayname;
            PaymentUrl = url + bankName+ "-"+ stk+ "-compact2.png?"+ "amount=" + amount + "&addInfo=" + addInfo + "&accountName=" + accountName;
            return Page();
        }
    }
}
