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

        public IActionResult OnPost(int id, string action, string nAc)
        {
            

            if (!string.IsNullOrEmpty(nAc))
            {
                FilterUsers(nAc);
            }
            else
            {
                if (action.Equals("Lock"))
                {
                    _adminService.LockAccount(id);

                }else if (action.Equals("View"))
                {
                    return RedirectToPage("AdminUserDetails", new {id = id});
                }
                else
                {
                    _adminService.UnlockAccount(id);
                }
                return RedirectToPage();
            }
            return  Page();
        }

        private void FilterUsers(string nAc)
        {
            if (nAc.Equals("Total"))
            {
                Users = _adminService.GetAllUsers(); 
            }
            else if (nAc.Equals("Investor"))
            {
                Users = _adminService.GetInvestors();
            }
            else if (nAc.Equals("Founder"))
            {
                Users = _adminService.GetFounders();
            }
        }
    }
}
