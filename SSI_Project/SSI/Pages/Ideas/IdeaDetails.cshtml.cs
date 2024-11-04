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
        public List<Comment> Comments { get; set; }
        public Ideadetail SelectedMilestone { get; set; }
        public List<Comment> MilestoneComments { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int TotalPages => (int)Math.Ceiling((double)TotalCommentCount / CommentsPerPage);
        public int CommentsPerPage { get; } = 2;
        public int TotalCommentCount => (SelectedMilestone != null ? MilestoneComments.Count : Comments.Count); 
        public IdeaDetailsModel(IIdeaService ideaService)
        {
            _ideaService = ideaService;
            MilestoneComments = new List<Comment>();
            Comments = new List<Comment>();
        }

        public IActionResult OnGet(int id)
        {
            (Idea, CommentCount, Comments) = _ideaService.GetIdeaById(id);
            if (Idea == null)
            {
                return NotFound();
            }
            return Page();
        }
        public IActionResult OnPostMilestone(int milestoneId, int ideaId)
        {
            (Idea, _, Comments) = _ideaService.GetIdeaById(ideaId);
            (SelectedMilestone, MilestoneComments) = _ideaService.GetMilestoneDetailsById(milestoneId);
            CommentCount = MilestoneComments.Count;
            return Page();
        }
        public IActionResult OnPostChangePage(int milestoneId, int ideaId, int page)
        {
            CurrentPage = page;
            (Idea, _, Comments) = _ideaService.GetIdeaById(ideaId);

            if (milestoneId > 0)
            {
                (SelectedMilestone, MilestoneComments) = _ideaService.GetMilestoneDetailsById(milestoneId);
                CommentCount = MilestoneComments.Count;
            }
            else
            {
                SelectedMilestone = null;
                CommentCount = Comments.Count;
            }

            return Page();
        }
        public IActionResult SubmitComment(int userId)
        {
            return Page();
        }
    }
}
