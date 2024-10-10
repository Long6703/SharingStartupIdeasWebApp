using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;

namespace SSI.Web.Client.Pages
{
    public class PaymentCallbackModel : PageModel
    {
        private readonly ILogger<PaymentCallbackModel> _logger;
        private readonly IVnPayService _vnPayService;

        public PaymentCallbackModel(ILogger<PaymentCallbackModel> logger, IVnPayService vnPayService)
        {
            _logger = logger;
            _vnPayService = vnPayService;
        }
        public string Result { get; set; } = string.Empty;

        public void OnGet()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            if (response == null || response.VnPayResponseCode != "00")
            {
                Result = $"Lỗi thanh toán VN Pay: {response.VnPayResponseCode}";
            }
            else
            {
                Result = $"Thanh toán VNPay thành công";
            }
        }
    }
}
