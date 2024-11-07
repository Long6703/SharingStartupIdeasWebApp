using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Models;
using SSI.Services.IService;

namespace SSI.Pages.Admin
{
    public class UserDetailsModel : PageModel
    {
        private readonly IAdminService adminService;
        private readonly IAdminIdeasService adminIdeasService;
        public UserDetailsModel(IAdminService adminService, IAdminIdeasService adminIdeas)
        {
            this.adminService = adminService;
            this.adminIdeasService = adminIdeas;
        }

        public ICollection<Idea> Ideas { get; set; }
        public Models.User User { get; set; } = new Models.User();
        public ICollection<Ideadetail> ideadetails { get; set; }
        public ICollection<Image> Images { get; set; }
        public decimal TotalAmountInvest { get; set; }  
        public int TotalIdeasCount { get; set; }
        public void OnGet(int id)
        {
            User = adminService.GetUser(id);
            Ideas = adminIdeasService.GetIdeasByUser(id);
            TotalAmountInvest = adminService.GetTotalAmountByUserId(id);
            TotalIdeasCount = adminService.GetTotalIdeasByUserId(id);
        }
    }
}
