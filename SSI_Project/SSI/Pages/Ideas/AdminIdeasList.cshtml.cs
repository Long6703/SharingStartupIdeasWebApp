using MailKit.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Models;
using SSI.Services.IService;
using SSI.Services.Service;
using System.Collections;

namespace SSI.Pages.Ideas
{
    public class AdminIdeasListModel : PageModel
    {
        private readonly IAdminIdeasService _adminIdeasService;
        private readonly IAdminService _adminService;
        private readonly AdminEmailService _emailService;
        public AdminIdeasListModel(IAdminIdeasService adminIdeasService, IAdminService adminService, AdminEmailService emailService)
        {
            _adminIdeasService = adminIdeasService;
            _adminService = adminService;
            _emailService = emailService;
        }

        public Models.User User { get; set; } = new Models.User();
        public Models.Category Category { get; set; } = new Models.Category();
        public IEnumerable<Models.Idea> Ideas { get; set; } = new List<Models.Idea>();
        public void OnGet()
        {
            Ideas = _adminIdeasService.GetIdeas();

            foreach (var idea in Ideas)
            {
                User = _adminService.GetUser(idea.UserId);
                Category = _adminService.GetCategory(idea.IdeaId);
                idea.Category = Category;
                idea.User = User;
            }
        }

        public IActionResult OnPost(string action, int id)
        {
            var idea = _adminIdeasService.GetIdeaById(id);
            var user = _adminService.GetUser(idea.UserId);
            string subject, body;
            if (action == "Approve")
            {
                _adminIdeasService.ApproveIdeas(id);
               SendIdeaApprovalEmail(user, idea);

            }
            else if (action == "View")
            {
                return RedirectToPage("AdminIdeaDetails", new { ideId = id,uid =user.UserId });
            }
            else
            {
                _adminIdeasService.RejectIdeas(id);
                SendIdeaRejectionEmail(user, idea);
            }

            return RedirectToPage();
        }

        private void SendIdeaApprovalEmail(User user, Idea idea)
        {
            var subject = "Notification: Your Idea Has Been Approved";
            var body = $"<p>Hello {user.Displayname},</p>" +
                       $"<p>We are pleased to inform you that your idea titled '<strong>{idea.Title}</strong>' has been approved and is now live on our platform.</p>" +
                       "<p>Thank you for your valuable contribution. We look forward to seeing its positive impact!</p>";

            _emailService.SendEmail(user.Email, subject, body);
        }

        private void SendIdeaRejectionEmail(User user, Idea idea)
        {
            var subject = "Notification: Your Idea Has Been Rejected";
            var body = $"<p>Hello {user.Displayname},</p>" +
                       $"<p>We regret to inform you that your idea titled '<strong>{idea.Title}</strong>' has been rejected.</p>" +
                       "<p>Thank you for your submission, and please feel free to reach out if you have any questions.</p>";

            _emailService.SendEmail(user.Email, subject, body);
        }

    }
}
