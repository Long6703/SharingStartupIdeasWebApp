using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;
using SSI.Ultils.ViewModel;
using SSI.ViewModel;
using System.Text.Json;

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

        
        public async Task<IActionResult> OnGetAsync()
        {
            return await HandlePaymentAsync();
        }
        public async Task<IActionResult> HandlePaymentAsync()
        {
            byte[] userBytes;
            var name = "";
            var email = "";
            if (HttpContext.Session.TryGetValue("UserSession", out userBytes))
            {
                var userJson = System.Text.Encoding.UTF8.GetString(userBytes);
                var userViewModel = JsonSerializer.Deserialize<UserViewModel>(userJson);
                name = userViewModel.Displayname;
                email = userViewModel.Email;
            }
            var vnPayModel = new VnPaymentRequestModel
            {
                Amount = Amount,
                CreatedDate = DateTime.Now,
                Description = "Thanh toan investment request thanh cong!",
                FullName = name+" - "+email,
                OrderId = RequestId
            };
            return Redirect(_vnPayService.CreatePaymentUrl(HttpContext, vnPayModel));
        }
    }
}
