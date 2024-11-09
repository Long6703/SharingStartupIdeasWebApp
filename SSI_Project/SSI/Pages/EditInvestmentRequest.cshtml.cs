using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Models;
using SSI.Services.IService;
using SSI.Ultils.ViewModel;
using System.Text.Json;
namespace SSI.Pages
{
    [Authorize(Roles = "investor")]
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
            byte[] userBytes;
            int userId = 0;
            if (HttpContext.Session.TryGetValue("UserSession", out userBytes))
            {
                var userJson = System.Text.Encoding.UTF8.GetString(userBytes);
                var userViewModel = JsonSerializer.Deserialize<UserViewModel>(userJson);
                userId = userViewModel.UserId;
            }
            var investReq = new Models.InvestmentRequest
            {
                RequestId = RequestId,
                IdeaId = IdeaId,
                UserId = userId,  
                Amount = Amount,
                Status = "pending",
                CreatedAt = DateTime.Now,
                EquityPercentage = EquityPercentage,
                InvestmentPeriod = InvestmentPeriod,  
                Description = Description
            };
            await _investReqService.UpdateInvestmentRequestAsync(investReq);
            return RedirectToPage("/ManageRequestInvestor");
        }
    }
}
