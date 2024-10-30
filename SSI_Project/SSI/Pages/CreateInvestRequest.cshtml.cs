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
        public void OnGet(int ideaId)
        {
            IdeaId = ideaId;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var username = HttpContext.Session.GetString("user");
            //get userid by username
            var investReq = new Models.InvestmentRequest
            {
                IdeaId = IdeaId,
                UserId = 2,
                Amount = Amount,
                Status = "pending",
                CreatedAt = DateTime.Now,
                EquityPercentage = EquityPercentage,
                InvestmentPeriod = InvestmentPeriod,
                Description = Description
            };
            await _investReqService.AddInvestmentRequestAsync(investReq);
            return RedirectToPage("/ManageRequestInvestor");
        }
    }
}
