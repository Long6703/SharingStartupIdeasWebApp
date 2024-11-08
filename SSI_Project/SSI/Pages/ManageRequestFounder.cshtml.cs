using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;
using SSI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SSI.Pages
{
    public class ManageRequestFounderModel : PageModel
    {
        private readonly IInvestmentRequestService _investRequestService;

        public ManageRequestFounderModel(IInvestmentRequestService investmentRequestService)
        {
            _investRequestService = investmentRequestService;
        }

        public IEnumerable<Idea> IdeasWithRequests { get; set; } = new List<Idea>();
        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 10;
        public bool HasMorePages { get; set; }

        public async Task OnGetAsync(int founderUserId, int pageNumber = 1)
        {
            PageNumber = pageNumber;

            // Fetching ideas with associated investment requests
            IdeasWithRequests = await _investRequestService.GetIdeasWithInvestmentRequestsByFounderAsync(founderUserId, pageNumber, PageSize);

            // Check if we have more pages by verifying if we received exactly PageSize items
            HasMorePages = IdeasWithRequests.Count() == PageSize;
        }
    }
}
