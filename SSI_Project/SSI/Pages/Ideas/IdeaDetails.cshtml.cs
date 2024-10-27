using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Models;
using SSI.Services.IService;

namespace SSI.Pages.Ideas
{
    public class IdeaDetailsModel : PageModel
    {
        private readonly IIdeaService _ideaService;
        public Idea Idea { get; set; }
        public int CommentCount { get; set; }
        public IdeaDetailsModel(IIdeaService ideaService)
        {
            _ideaService = ideaService;
        }

        public IActionResult OnGet(int id)
        {
            (Idea, CommentCount) = _ideaService.GetIdeaById(id);
            if (Idea == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
