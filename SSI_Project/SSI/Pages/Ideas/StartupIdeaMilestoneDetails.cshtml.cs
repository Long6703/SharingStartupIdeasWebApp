using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Models;
using SSI.Services.IService;

namespace SSI.Pages.Ideas
{
    public class StartupIdeaMilestoneDetailsModel : PageModel
    {
        private readonly IIdeaService _ideaService;

        private const int CommentsPerPage = 5;

        public StartupIdeaMilestoneDetailsModel(IIdeaService ideaService)
        {
            _ideaService = ideaService;
        }

        public Ideadetail? MilestoneDetail { get; set; }
        public Idea? Idea { get; set; }

        // Pagination properties
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, int page = 1)
        {
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

            var comments = MilestoneDetail.Comments
                            .Where(c => c.ParentId == null)
                            .OrderByDescending(c => c.CreatedAt)
                            .ToList();

            TotalPages = (int)Math.Ceiling(comments.Count / (double)CommentsPerPage);
            CurrentPage = page;
            MilestoneDetail.Comments = comments.Skip((page - 1) * CommentsPerPage)
                                               .Take(CommentsPerPage)
                                               .ToList();
            return Page();
        }
    }
}
