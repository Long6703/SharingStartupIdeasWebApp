using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace SSI.Web.Client.Pages
{
    public class IndexModel : PageModel
    {

        public async Task<IActionResult> OnGet()
        {
            return Page();
        }
    }
}
