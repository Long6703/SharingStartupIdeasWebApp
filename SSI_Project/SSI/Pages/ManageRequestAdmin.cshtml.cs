using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Models;
using SSI.Services.IService;
using SSI.Ultils.ViewModel;
using System.Text.Json;

namespace SSI.Pages
{
    [Authorize(Roles = "admin")]
    public class ManageRequestAdminModel : PageModel
    {
        private readonly ILogger<ManageRequestAdminModel> _logger;
        private readonly IInvestmentRequestService _investReqService;
        public ManageRequestAdminModel(ILogger<ManageRequestAdminModel> logger, IInvestmentRequestService investReqService)
        {
            _logger = logger;
            _investReqService = investReqService;
        }
        public List<InvestmentRequest> InvestmentRequests { get; set; }
        public List<String> Status { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            

            var investRequests = await _investReqService.GetAllInvestmentRequestAsync();
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
            var investRequests = await _investReqService.GetAllInvestmentRequestAsync();
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

    }
}
