using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SSI.Pages
{
    public class VietQrModel : PageModel
    {

        private readonly ILogger<VietQrModel> _logger;


        [BindProperty(SupportsGet = true)]
        public double Amount { get; set; }
        [BindProperty(SupportsGet = true)]
        public int RequestId { get; set; }
        public string PaymentUrl { get; set; }
        public string Info { get; set; }
        public VietQrModel(ILogger<VietQrModel> logger)
        {
            _logger = logger;

        }

        public async Task<IActionResult> OnGetAsync()
        {
            return await HandlePaymentAsync();
        }
        public async Task<IActionResult> HandlePaymentAsync()
        {
            String url = "https://img.vietqr.io/image/BIDV-4271031836-compact2.png?";
            String amount = Amount.ToString();
            String addInfo = " Thanh toan dau tu " + RequestId;
            Info = addInfo;
            String accountName = "SSI Project";
            PaymentUrl = url + "amount=" + amount + "&addInfo=" + addInfo + "&accountName=" + accountName;
            return Page();
        }
    }
}



