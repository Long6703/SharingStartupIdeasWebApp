using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SSI.Pages
{
    public class UserEditProfileModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
