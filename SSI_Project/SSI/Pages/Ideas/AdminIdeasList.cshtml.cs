using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
                Category = _adminService.GetCategory(idea.UserId);
                idea.Category = Category;
                idea.User = User;
            }
        }

        public IActionResult OnPost(string action, int id) 
        {
            var idea = _adminIdeasService.GetIdeaById(id);
            var user = _adminService.GetUser(idea.UserId);
            string subject, body;
            if(action == "Approve")
            {
                _adminIdeasService.ApproveIdeas(id);
                subject = "Notification: Your Idea Has Been Approved";
                body = $"<p>Hello {user.Displayname},</p><p>We regret to inform you that your idea titled '<strong>{idea.Title}</strong>' has been approved.</p><p>Thank you for your submission.</p>";


            }
            else if(action == "View")
            {
                return RedirectToPage("AdminIdeaDetails", new {id = id});
            }else
            {
                _adminIdeasService.RejectIdeas(id);
                subject = "Notification: Your Idea Has Been Rejected";
                body = $"<p>Hello {user.Displayname},</p><p>We regret to inform you that your idea titled '<strong>{idea.Title}</strong>' has been rejected.</p><p>Thank you for your submission.</p>";

            }
            _emailService.SendEmail(user.Email, subject, body);
            return RedirectToPage();
        }


        
    }
}
