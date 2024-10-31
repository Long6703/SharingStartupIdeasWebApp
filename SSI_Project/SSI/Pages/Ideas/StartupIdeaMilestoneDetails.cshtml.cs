using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Models;
using SSI.Services.IService;

namespace SSI.Pages.Ideas
{
    public class StartupIdeaMilestoneDetailsModel : PageModel
    {
        private readonly IIdeaService _ideaService;
        public StartupIdeaMilestoneDetailsModel(IIdeaService ideaService)
        {
            _ideaService = ideaService;
        }
        public Ideadetail? MilestoneDetail { get; set; }
        public Idea? Idea { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Retrieve the milestone detail by ID
            MilestoneDetail = await _ideaService.GetMilestoneDetailByIdAsync(id);

            if (MilestoneDetail == null)
            {
                return NotFound();
            }
            Idea = await _ideaService.GetIdeaWithDetailsAsync(MilestoneDetail.IdeaId);

            if (Idea == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
