using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Models;
using SSI.Services.IService;

namespace SSI.Pages.Ideas
{
    public class IdeaDetailsModel : PageModel
    {
        private readonly IAdminService adminService;
        private readonly IAdminIdeasService adminIdeasService;
        public IdeaDetailsModel(IAdminService adminService, IAdminIdeasService adminIdeasService)
        {
            this.adminService = adminService;
            this.adminIdeasService = adminIdeasService;
        }
        public ICollection<Idea> Ideas { get; set; }
        public Models.User User { get; set; } = new Models.User();
        public Models.Idea Idea { get; set; } = new Idea();
        public ICollection<Ideadetail> ideadetails { get; set; }
        public ICollection<Image> Images { get; set; }
        public void OnGet(int id)
        {
            Idea = adminIdeasService.GetIdeaById(id);
            User = adminService.GetUser(Idea.UserId);
        }
    }
}
