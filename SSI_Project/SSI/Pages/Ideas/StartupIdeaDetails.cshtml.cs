using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Models;
using SSI.Services.IService;

namespace SSI.Pages.Ideas
{
    [Authorize(Roles = "founder")]
    public class StartupIdeaDetailsModel : PageModel
    {
        private readonly IIdeaService _ideaService;

        public StartupIdeaDetailsModel(IIdeaService ideaService)
        {
            _ideaService = ideaService;
        }

        public Idea? Idea { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Idea = await _ideaService.GetIdeaWithDetailsAsync(id);

            if (Idea == null)
            {
                return NotFound();
            }
            Idea.Ideadetails = Idea.Ideadetails.OrderBy(d => d.CreatedAt).ToList();
            return Page();
        }
    }
}
