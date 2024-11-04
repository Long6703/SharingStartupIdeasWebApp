using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;

namespace SSI.Pages.Admin
{
    public class InvestorModel : PageModel
    {
        private readonly IAdminService _adminService;
        public InvestorModel(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public IEnumerable<Models.User> Users { get; set; } = new List<Models.User>();

        public void OnGet()
        {
            Users = _adminService.GetInvestors();
        }

        public IActionResult OnPost(int id, string action)
        {
            if (action.Equals("Lock"))
            {
                _adminService.LockAccount(id);
            }
            else
            {
                _adminService.UnlockAccount(id);
            }
            return RedirectToPage();
        }
    }
}
