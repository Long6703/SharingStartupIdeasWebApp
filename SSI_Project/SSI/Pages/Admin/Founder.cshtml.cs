using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;

namespace SSI.Pages.Admin
{
    public class FounderModel : PageModel
    {
        private readonly IAdminService _adminService;
        public FounderModel(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [BindProperty]
        public Models.User User { get; set; }
        public IEnumerable<Models.User> Users { get; set; } = new List<Models.User>();
        public void OnGet()
        {
            Users = _adminService.GetFounders();
       
        }
    }
}
