using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Data.Repository;
using SSI.Models;
using SSI.Services.IService;
using SSI.Services.Service;
using System.Net.WebSockets;
using System.Text;

namespace SSI.Pages.Ideas
{
    [Authorize(Roles ="admin")]
    public class AdminIdeaDetailsModel : PageModel
    {
        private readonly IAdminService adminService;
        private readonly IAdminIdeasService adminIdeasService;
        private readonly AdminEmailService _emailService;
        public AdminIdeaDetailsModel(IAdminService adminService, IAdminIdeasService adminIdeasService, AdminEmailService emailService)
        {
            this.adminService = adminService;
            this.adminIdeasService = adminIdeasService;
            _emailService = emailService;
        }
  
        public Models.User User { get; set; } = new Models.User();
        public Models.Idea Idea { get; set; } = new Models.Idea();
        public ICollection<Ideadetail> ideadetails { get; set; }
        public ICollection<Image> images { get; set; }
        public Models.Category Category { get; set; } = new Models.Category();
        public int countIdeaDetais {  get; set; }

        public List<string> Paras {  get; set; } = new List<string>();
        public void OnGet(int ideId, int uid)
        {
            Idea = adminIdeasService.GetIdeaById(ideId);
            ideadetails = adminIdeasService.GetIdeadetailsByIdeaId(ideId);
            User = adminService.GetUser(uid);
            Idea.Ideadetails = ideadetails;
            Idea.User = User;
            Category = adminService.GetCategory(Idea.IdeaId);
            foreach (var i in ideadetails)
            {
                images = adminIdeasService.GetImages(i.IdeaDetailId);
                if (images != null) {
                    i.Images = images;
                }
                
                i.Idea.Category = Category;
            }
            countIdeaDetais = adminIdeasService.CountIdeaDetailByIdeaId(ideId);
            Paras = GetPara(Idea.Description);
        }

        public IActionResult OnPost(string action, int id)
        {
            var cate = adminService.GetCategory(Idea.IdeaId);
            var idea = adminIdeasService.GetIdeaById(id);
            var user = adminService.GetUser(idea.UserId);
       
            if (action == "Approve")
            {
                adminIdeasService.ApproveIdeas(id);
                SendIdeaApprovalEmail(user, idea);

            }
            else
            {
                adminIdeasService.RejectIdeas(id);
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


        private List<string> GetPara(string text)
        {
            List<string> para = new List<string>();
            string[] words = text.Split(' ');
            StringBuilder currentParagraph = new StringBuilder();
            int wordCount = 0;
            foreach (var word in words)
            {
                currentParagraph.Append(word).Append(" ");
                wordCount++;
                if (wordCount >= 100 && word.EndsWith("."))
                {
                    para.Add(currentParagraph.ToString().Trim());
                    currentParagraph.Clear();
                    wordCount = 0;
                }
            }
            if (currentParagraph.Length > 0)
            {
                para.Add(currentParagraph.ToString().Trim());
            }
            return para;
        }
    }
}
