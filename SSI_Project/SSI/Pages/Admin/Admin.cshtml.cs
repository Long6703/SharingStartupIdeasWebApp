using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Models;
using SSI.Services.IService;

namespace SSI.Pages.Admin
{
    public class AdminModel : PageModel
    {
        private readonly IAdminService _adminService;
        public AdminModel(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public IEnumerable<User> Users { get; set; } = new List<User>();
        public void OnGet()
        {
            Users = _adminService.GetAllUsers();
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
