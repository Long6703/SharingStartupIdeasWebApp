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
        public Models.InvestmentRequest InvestmentRequests { get; set; }
        public async Task OnGetAsync(int requestId)
        {
            RequestId = requestId;
            var investRequests = await _investReqService.GetInvestmentRequestByIdAsync(RequestId);
            InvestmentRequests = investRequests;
        }
    }
}
