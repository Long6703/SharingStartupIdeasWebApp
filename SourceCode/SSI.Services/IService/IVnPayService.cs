using Microsoft.AspNetCore.Http;
using SSI.Share.ViewModel;

namespace SSI.Services.IService
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model);
        VnPaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
