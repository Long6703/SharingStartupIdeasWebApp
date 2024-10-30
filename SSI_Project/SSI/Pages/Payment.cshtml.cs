using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;
using SSI.ViewModel;

namespace SSI.Pages
{
    public class PaymentModel : PageModel
    {
        private readonly ILogger<PaymentModel> _logger;
        private readonly IVnPayService _vnPayService;

        [BindProperty(SupportsGet = true)]
        public double Amount { get; set; }
        [BindProperty(SupportsGet = true)]
        public int RequestId { get; set; }
        public PaymentModel(ILogger<PaymentModel> logger, IVnPayService vnPayService)
        {
            _logger = logger;
            _vnPayService = vnPayService;
        }

        // GET method to show payment details and total amount
        public async Task<IActionResult> OnGetAsync()
        {
            return await HandlePaymentAsync();
        }
        public async Task<IActionResult> HandlePaymentAsync()
        {
            var username = HttpContext.Session.GetString("user");
            // get user by username
            var vnPayModel = new VnPaymentRequestModel
            {
                Amount = Amount,
                CreatedDate = DateTime.Now,
                Description = "Thanh toan investment request thanh cong!",
                FullName = "Chu Tran Duc Tung - ductung@gmail.com",// user name + email
                OrderId = RequestId
            };
            return Redirect(_vnPayService.CreatePaymentUrl(HttpContext, vnPayModel));
        }
    }
}
