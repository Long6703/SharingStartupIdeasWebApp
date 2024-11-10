using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
        public List<String> Status { get; set; }

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
            var uniqueStatuses = investRequests
    .Select(request => request.Status) 
    .Distinct() 
    .ToList();
            Status = uniqueStatuses;
            return Page();
        }

        public async Task<IActionResult> OnPostSearchByStatusAsync(string status)
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
            var uniqueStatuses = investRequests
    .Select(request => request.Status)
    .Distinct()
    .ToList();
            Status = uniqueStatuses;
            // Convert to IQueryable and apply filtering if a status is provided
            if (!string.IsNullOrEmpty(status))
            {
                investRequests = investRequests.Where(r => r.Status == status);
            }

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