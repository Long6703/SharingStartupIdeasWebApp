using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Models;
using SSI.Services.IService;

namespace SSI.Pages.Ideas
{
    public class StartupIdeaListModel : PageModel
    {
        private readonly IIdeaService _ideaService;
        private const int PageSize = 5;

        public StartupIdeaListModel(IIdeaService ideaService)
        {
            _ideaService = ideaService;
        }

        public IEnumerable<Idea> Ideas { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int UserId { get; set; }
        public async Task OnGetAsync(int userId, int pageNumber = 1)
        {
            UserId = userId;
            PageNumber = pageNumber;
            int totalIdeas = await _ideaService.GetTotalIdeasCountByUserIdAndRoleAsync(userId, "startup");
            TotalPages = (int)Math.Ceiling(totalIdeas / (double)PageSize);

            Ideas = await _ideaService.GetIdeasByUserIdAndRoleAsync(userId, "startup", PageNumber, PageSize);
        }
    }
}
