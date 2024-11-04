using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Models;
using SSI.Services.IService;
using SSI.Ultils.ViewModel;
using System.Text.Json;

namespace SSI.Pages
{
    public class ManageRequestInvestorModel : PageModel
    {
        private readonly ILogger<ManageRequestInvestorModel> _logger;
        private readonly IInvestmentRequestService _investReqService;
        public ManageRequestInvestorModel(ILogger<ManageRequestInvestorModel> logger, IInvestmentRequestService investReqService)
        {
            _logger = logger;
            _investReqService = investReqService;
        }
        public List<InvestmentRequest> InvestmentRequests { get; set; }
        

        public async Task<IActionResult> OnGetAsync()
        {
            byte[] userBytes;
            int investorId = 0;
            if (HttpContext.Session.TryGetValue("UserSession", out userBytes))
            {
                var userJson = System.Text.Encoding.UTF8.GetString(userBytes);
                var userViewModel = JsonSerializer.Deserialize<UserViewModel>(userJson);
                investorId = userViewModel.UserId;
            }
            
            var investRequests = await _investReqService.GetInvestmentRequestByInvestorIdAsync(investorId);
            InvestmentRequests = investRequests.ToList();
            
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _investReqService.DeleteInvestmentRequestAsync(id);
            return RedirectToPage("/ManageRequestInvestor");
        }
    }
}
