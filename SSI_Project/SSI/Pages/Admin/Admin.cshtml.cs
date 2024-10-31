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

        public List<User> Users { get; set; } = new List<User>();
        public void OnGet()
        {
            Users = _adminService.GetAllUsers();
        }
    }
}
