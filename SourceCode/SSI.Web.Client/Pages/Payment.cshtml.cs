using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;
using SSI.Services.Service;
using SSI.Share.ViewModel;

namespace SSI.Web.Client.Pages
{
    public class PaymentModel : PageModel
    {
        private readonly ILogger<PaymentModel> _logger;
        private readonly IVnPayService _vnPayService;

        public PaymentModel(ILogger<PaymentModel> logger, IVnPayService vnPayService)
        {
            _logger = logger;
            _vnPayService = vnPayService;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var vnPayModel = new VnPaymentRequestModel
            {
                Amount = 100000,
                CreatedDate = DateTime.Now,
                Description = "Chu Tran Duc Tung - 0328015134",
                FullName = "Chu Tran Duc Tung",
                OrderId = new Random().Next(1000, 100000)
            };
            return Redirect(_vnPayService.CreatePaymentUrl(HttpContext, vnPayModel));
        }
    }
}
