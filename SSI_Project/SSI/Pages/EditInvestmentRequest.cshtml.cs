using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Models;
using SSI.Services.IService;

namespace SSI.Pages
{
    public class EditInvestmentRequestModel : PageModel
    {
        private readonly ILogger<EditInvestmentRequestModel> _logger;
        private readonly IInvestmentRequestService _investReqService;
        public EditInvestmentRequestModel(ILogger<EditInvestmentRequestModel> logger, IInvestmentRequestService investReqService)
        {
            _logger = logger;
            _investReqService = investReqService;
        }
        [BindProperty]
        public int RequestId { get; set; }
        [BindProperty]
        public decimal Amount { get; set; }

        [BindProperty]
        public int IdeaId { get; set; }

        [BindProperty]
        public decimal EquityPercentage { get; set; }

        [BindProperty]
        public string InvestmentPeriod { get; set; }

        [BindProperty]
        public string? Description { get; set; }
        [BindProperty]
        public Models.InvestmentRequest InvestmentRequests { get; set; }
        public async Task OnGetAsync(int requestId)
        {
            RequestId = requestId;
            var investRequests = await _investReqService.GetInvestmentRequestByIdAsync(RequestId);
            InvestmentRequests = investRequests;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            // Ensure model is valid before proceeding
            

            // Create a new InvestmentRequest model
            var investReq = new Models.InvestmentRequest
            {
                RequestId = RequestId,
                IdeaId = IdeaId,
                UserId = 2,  // Static value for now, replace with logged-in user ID in the future
                Amount = Amount,
                Status = "pending",
                CreatedAt = DateTime.Now,
                EquityPercentage = EquityPercentage,
                InvestmentPeriod = InvestmentPeriod,  // Value from the form
                Description = Description
            };

            // Call the service to save the investment request
            await _investReqService.UpdateInvestmentRequestAsync(investReq);

            // Redirect to Index page after successful submission
            return RedirectToPage("/ManageRequestInvestor");
        }
    }
}
