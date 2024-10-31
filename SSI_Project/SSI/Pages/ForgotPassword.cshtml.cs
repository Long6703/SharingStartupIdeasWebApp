using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SSI.Web.Client.Pages
{
    [Authorize(Roles = "founder")]
    public class ForgotPasswordModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
