using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;
using SSI.Ultils.ViewModel;
using System.Text.Json;
namespace SSI.Pages
{
    [Authorize(Roles = "founder")]
    public class FounderRequestDetailsModel : PageModel
    {
        private readonly ILogger<FounderRequestDetailsModel> _logger;
        private readonly IInvestmentRequestService _investmentRequestService;
        public FounderRequestDetailsModel(ILogger<FounderRequestDetailsModel> logger, IInvestmentRequestService investmentRequestService)
        {
            _logger = logger;
            _investmentRequestService = investmentRequestService;
        }

        public Models.InvestmentRequest InvestmentRequest { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }
        public async Task<IActionResult> OnGetAsync(int RequestId)
        {
            InvestmentRequest = await _investmentRequestService.GetInvestmentRequestByIdAsync(RequestId);

            if (InvestmentRequest == null)
            {
                return NotFound();
            }

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int RequestId, string action)
        {
            byte[] userBytes;
            if (HttpContext.Session.TryGetValue("UserSession", out userBytes))
            {
                var userJson = System.Text.Encoding.UTF8.GetString(userBytes);
                var userViewModel = JsonSerializer.Deserialize<UserViewModel>(userJson);
                if (userViewModel.BankAccountNumber == null)
                {
                    ErrorMessage = "Bank account number is missing. Please update your profile.";
                    return RedirectToPage("FounderRequestDetails", new { RequestId });
                }
            }
            InvestmentRequest = await _investmentRequestService.GetInvestmentRequestByIdAsync(RequestId);

            if (InvestmentRequest == null)
            {
                return NotFound();
            }

            if (action == "accept")
            {
                InvestmentRequest.Status = "Approved";
            }
            else if (action == "reject")
            {
                InvestmentRequest.Status = "Rejected";
            }

            await _investmentRequestService.UpdateInvestmentRequestAsync(InvestmentRequest);

            return RedirectToPage("ManageRequestFounder");
        }
    }
}
