using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Models;
using SSI.Services.IService;
using System.Threading.Tasks;
using System.Linq;
using SSI.Ultils.ViewModel;

namespace SSI.Pages.Ideas
{
    public class StartupIdeaMilestoneDetailsModel : PageModel
    {
        private readonly IIdeaService _ideaService;
        private const int CommentsPerPage = 5;
        private readonly ISessionService _sessionService;
        [BindProperty(SupportsGet = true)]
        public UserViewModel userViewModel { get; set; }
        public StartupIdeaMilestoneDetailsModel(IIdeaService ideaService, ISessionService sessionService)
        {
            _sessionService = sessionService;
            _ideaService = ideaService;
        }

        public Ideadetail? MilestoneDetail { get; set; }
        public Idea? Idea { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, int ideaId, int page = 1)
        {
            MilestoneDetail = await _ideaService.GetMilestoneDetailByIdAsync(id);
            if (MilestoneDetail == null)
            {
                return NotFound();
            }

            Idea = await _ideaService.GetIdeaWithDetailsAsync(ideaId);
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
        public async Task<IActionResult> OnPostAddReplyAsync(int parentId, string replyContent,int IdeaDetailId, int IdeaId)
        {
            userViewModel = _sessionService.GetSession<UserViewModel>("UserSession");
            if (string.IsNullOrWhiteSpace(replyContent))
            {
                ModelState.AddModelError(string.Empty, "Reply content cannot be empty.");
                return RedirectToPage(new { id = MilestoneDetail?.IdeaDetailId, ideaId = Idea?.IdeaId, page = CurrentPage });
            }

            var parentComment = await _ideaService.GetCommentByIdAsync(parentId);
            if (parentComment == null)
            {
                return NotFound();
            }

            var reply = new Comment
            {
                Content = replyContent,
                CreatedAt = DateTime.UtcNow,
                UserId = userViewModel.UserId, 
                ParentId = parentId,
                IdeaDetailId = parentComment.IdeaDetailId 
            };

            await _ideaService.AddCommentAsync(reply);
            return RedirectToPage(new { id = IdeaDetailId, ideaId = IdeaId });
        }
    }
}
