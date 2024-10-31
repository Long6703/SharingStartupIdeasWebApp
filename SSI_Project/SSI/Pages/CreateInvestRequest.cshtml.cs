using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;

namespace SSI.Pages
{
    public class CreateInvestRequestModel : PageModel
    {
        private readonly ILogger<CreateInvestRequestModel> _logger;
        private readonly IInvestmentRequestService _investReqService;

        [BindProperty]
        public decimal Amount { get; set; }
        public int IdeaId { get; set; }
        public decimal EquityPercentage { get; set; }
        public string InvestmentPeriod { get; set; }
        public string? Description { get; set; }

        public CreateInvestRequestModel(ILogger<CreateInvestRequestModel> logger, IInvestmentRequestService investReqService)
        {
            _logger = logger;
            _investReqService = investReqService;
        }

        // GET method to show payment details and total amount
        public void OnGet(decimal amount, int ideaId)
        {
            Amount = amount;
            IdeaId = ideaId;
        }

        // POST method to create the payment URL
        public async Task<IActionResult> OnPostAsync()
        {
            var investReq = new Models.InvestmentRequest
            {
                IdeaId = IdeaId,
                UserId = 1,
                Amount = Amount,
                Status = "pending",
                CreatedAt = DateTime.Now,
                EquityPercentage = EquityPercentage,
                InvestmentPeriod = InvestmentPeriod,
                Description = Description
            };
            await _investReqService.AddInvestmentRequestAsync(investReq);
            return RedirectToPage("/Index");
        }
    }
}
