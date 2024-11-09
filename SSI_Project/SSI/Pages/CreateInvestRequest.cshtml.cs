using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;
using SSI.Ultils.ViewModel;
using System.Text.Json;

namespace SSI.Pages
{
    [Authorize(Roles = "investor")]
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
            byte[] userBytes;
            int userId=0;
            if (HttpContext.Session.TryGetValue("UserSession", out userBytes))
            {
                var userJson = System.Text.Encoding.UTF8.GetString(userBytes);
                var userViewModel = JsonSerializer.Deserialize<UserViewModel>(userJson);
                userId = userViewModel.UserId; 
            }
            var investReq = new Models.InvestmentRequest
            {
                IdeaId = IdeaId,
                UserId = userId,
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
