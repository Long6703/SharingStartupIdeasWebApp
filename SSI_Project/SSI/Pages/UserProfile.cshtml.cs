using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SSI.Pages
{
    [Authorize]
    public class UserProfileModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
