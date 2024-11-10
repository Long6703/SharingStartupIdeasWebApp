using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Models;
using SSI.Services.IService;

namespace SSI.Pages.Admin
{
    [Authorize(Roles = "admin")]
    public class AdminUserDetailsModel : PageModel
    {
        private readonly IAdminService adminService;
        private readonly IAdminIdeasService adminIdeasService;
        public AdminUserDetailsModel(IAdminService adminService, IAdminIdeasService adminIdeas)
        {
            this.adminService = adminService;
            this.adminIdeasService = adminIdeas;
        }

        public ICollection<Idea> Ideas { get; set; }
        public Models.User User { get; set; } = new Models.User();
        public ICollection<Ideadetail> ideadetails { get; set; }
        public ICollection<Image> Images { get; set; }
        public int NoOfSuccessInvest {  get; set; }
        public int SumAmountInvest {  get; set; }
        public int NoOfRejectInvest { get; set; }
        public void OnGet(int id)
        {
            User = adminService.GetUser(id);
            if (User.Role.Equals("investor"))
            {
                Ideas = adminIdeasService.GetIdeasByInvestore(id);
            }
            else
            {
                Ideas = adminIdeasService.GetIdeasByFounder(id);
            }
            
            User.Ideas = Ideas;
            NoOfSuccessInvest = adminService.CountNoSuccesInvest(id);
            NoOfRejectInvest = adminService.CountNoRejecrInvest(id);
            SumAmountInvest = (int)adminService.SumAmountInvest(id);
        }

        public IActionResult OnPost(int id, string action)
        {
            User = adminService.GetUser(id);
            if (action.Equals("UnLock"))
            {
                adminService.UnlockAccount(id);
            }
            else
            {
                adminService.LockAccount(id);
            }
            return RedirectToPage(new {id = id});
        }
    }
}
