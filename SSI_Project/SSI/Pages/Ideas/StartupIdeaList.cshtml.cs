using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Models;
using SSI.Services.IService;
using SSI.Services.Service;
using SSI.Ultils.ViewModel;

namespace SSI.Pages.Ideas
{
    public class StartupIdeaListModel : PageModel
    {
        private readonly ISessionService _sessionService;
        [BindProperty(SupportsGet = true)]
        public UserViewModel userViewModel { get; set; }
        private readonly IIdeaService _ideaService;
        private const int PageSize = 5;

        public StartupIdeaListModel(IIdeaService ideaService, ISessionService sessionService)
        {
            _sessionService = sessionService;
            _ideaService = ideaService;
        }

        public IEnumerable<Idea> Ideas { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public async Task OnGetAsync(int pageNumber = 1)
        {
            userViewModel = _sessionService.GetSession<UserViewModel>("UserSession");
            PageNumber = pageNumber;
            int totalIdeas = await _ideaService.GetTotalIdeasCountByUserIdAndRoleAsync(userViewModel.UserId, "founder");
            TotalPages = (int)Math.Ceiling(totalIdeas / (double)PageSize);

            Ideas = await _ideaService.GetIdeasByUserIdAndRoleAsync(userViewModel.UserId, "founder", PageNumber, PageSize);
        }
    }
}
