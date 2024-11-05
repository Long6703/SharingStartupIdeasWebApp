using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Models;
using SSI.Services.IService;
using System.Text.Json;

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
        public int TotalCommentCount => (SelectedMilestone != null ? MilestoneComments.Count(c=>c.ParentId==null) : Comments.Count(c=>c.ParentId==null)); 
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
            ViewData["SelectedMilestone"] = SelectedMilestone;
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
        public IActionResult OnPostSubmitReview(string reviewContent, int milestoneId, int ideaId)
        {
            var userSessionBytes = HttpContext.Session.Get("UserSession");
            if (userSessionBytes == null)
            {
                return RedirectToPage("/Login"); 
            }

            var userJson = System.Text.Encoding.UTF8.GetString(userSessionBytes);
            var user = JsonSerializer.Deserialize<User>(userJson);

            if (user == null || user.UserId == 0)
            {
                return RedirectToPage("/Login"); 
            }

            var newComment = new Comment
            {
                IdeaDetailId = milestoneId,
                UserId = user.UserId,
                Content = reviewContent,
                CreatedAt = DateTime.UtcNow
            };

            _ideaService.AddComment(newComment);

            return RedirectToPage(new { id = ideaId });
        }
        public IActionResult OnPostSubmitReply(string replyContent, int parentId, int ideaDetailId, int ideaId)
        {
            var userSessionBytes = HttpContext.Session.Get("UserSession");
            if (userSessionBytes == null)
            {
                return RedirectToPage("/Login");
            }

            var userJson = System.Text.Encoding.UTF8.GetString(userSessionBytes);
            var user = JsonSerializer.Deserialize<User>(userJson);
            if (user == null || user.UserId == 0)
            {
                return RedirectToPage("/Login");
            }

            var newReply = new Comment
            {
                IdeaDetailId = ideaDetailId,
                UserId = user.UserId,
                ParentId = parentId, 
                Content = replyContent,
                CreatedAt = DateTime.UtcNow
            };

            _ideaService.AddComment(newReply);

            return RedirectToPage(new { id = ideaId });
        }

    }
}
