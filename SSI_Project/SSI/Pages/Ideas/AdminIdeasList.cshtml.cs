using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Models;
using SSI.Services.IService;

namespace SSI.Pages.Ideas
{
    public class AdminIdeasListModel : PageModel
    {
        private readonly IAdminIdeasService _adminIdeasService;
        public AdminIdeasListModel(IAdminIdeasService adminIdeasService)
        {
            _adminIdeasService = adminIdeasService;
        }

        public IEnumerable<Idea> Ideas { get; set; } = new List<Idea>();
        public void OnGet()
        {
            Ideas = _adminIdeasService.GetIdeas();
        }
    }
}
