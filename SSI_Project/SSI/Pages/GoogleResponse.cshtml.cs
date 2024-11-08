using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SSI.Pages
{
    public class GoogleResponseModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!result.Succeeded) return BadRequest(); 

            var claims = result.Principal?.Identities
                            .FirstOrDefault()?.Claims.Select(c => new { c.Type, c.Value });

            return RedirectToPage("/Index");
        }
    }
}
