using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;
using System.Threading.Tasks;

namespace SSI.Pages
{
    public class CreateInvestRequestModel : PageModel
    {
        private readonly ILogger<CreateInvestRequestModel> _logger;
        private readonly IInvestmentRequestService _investReqService;

        // BindProperty ensures data is received from the form for these properties
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

        public CreateInvestRequestModel(ILogger<CreateInvestRequestModel> logger, IInvestmentRequestService investReqService)
        {
            _logger = logger;
            _investReqService = investReqService;
        }

        // GET method to show investment details
        public void OnGet(int ideaId)
        {
            IdeaId = ideaId; // Fetch ideaId from the query string or parameter
        }

        // POST method to submit the investment request
        public async Task<IActionResult> OnPostAsync()
        {
            // Ensure model is valid before proceeding
            if (!ModelState.IsValid)
            {
                // Return the page with validation messages
                return Page();
            }

            // Create a new InvestmentRequest model
            var investReq = new Models.InvestmentRequest
            {
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
            await _investReqService.AddInvestmentRequestAsync(investReq);

            // Redirect to Index page after successful submission
            return RedirectToPage("/ManageRequestInvestor");
        }
    }
}
