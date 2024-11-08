using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;
using SSI.Ultils.ViewModel;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.Google;

namespace SSI.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginViewModel LoginViewModel { get; set; }
        private readonly IAccountService _accountService;
        public string message = string.Empty;
        public LoginModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
            var userViewModel = _accountService.Login(LoginViewModel);
            if (userViewModel == null)
            {
                message = "Invalid email or password";
                return Page();
            }

            var userJson = JsonSerializer.Serialize(userViewModel);
            HttpContext.Session.Set("UserSession", System.Text.Encoding.UTF8.GetBytes(userJson));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userViewModel.UserId.ToString()),
                new Claim(ClaimTypes.Email, userViewModel.Email),
                new Claim(ClaimTypes.Role, userViewModel.Role ?? "User")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            Console.WriteLine("Login success-----------------------------------------------------------------------------------------");

            return RedirectToPage("/Index");
        }
        public async Task<IActionResult> OnGetLoginGoogle()
        {
            var redirectUrl = Url.Page("/GoogleResponse");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }
    }
}
