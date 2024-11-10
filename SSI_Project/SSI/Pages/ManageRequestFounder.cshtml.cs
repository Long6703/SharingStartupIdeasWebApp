using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;
using SSI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using SSI.Ultils.ViewModel;
using System.Text.Json;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Authorization;

namespace SSI.Pages
{
    [Authorize(Roles = "founder")]
    public class ManageRequestFounderModel : PageModel
    {
        private readonly IInvestmentRequestService _investRequestService;
        private readonly ISessionService _sessionService;
        [BindProperty(SupportsGet = true)]
        public UserViewModel userViewModel { get; set; }
        public ManageRequestFounderModel(IInvestmentRequestService investmentRequestService, ISessionService sessionService)
        {
            _sessionService = sessionService;
            _investRequestService = investmentRequestService;
        }

        public List<InvestmentRequest> InvestmentRequests { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            byte[] userBytes;
            int founderId = 0;
            if (HttpContext.Session.TryGetValue("UserSession", out userBytes))
            {
                var userJson = System.Text.Encoding.UTF8.GetString(userBytes);
                var userViewModel = JsonSerializer.Deserialize<UserViewModel>(userJson);
                founderId = userViewModel.UserId;
            }

            var investRequests = await _investRequestService.GetIdeasWithInvestmentRequestsByFounderAsync(founderId);
            InvestmentRequests = investRequests.ToList();
            var uniqueStatuses = investRequests
            .Select(request => request.Status)
            .Distinct()
            .ToList();
            return Page();
        }
    }
}
