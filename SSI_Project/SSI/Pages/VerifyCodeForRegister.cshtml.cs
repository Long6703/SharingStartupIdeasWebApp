using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;
using SSI.Ultils.ViewModel;

namespace SSI.Pages
{
    public class VerifyCodeForRegisterModel : PageModel
    {
        private readonly ISessionService _sessionService;
        [BindProperty(SupportsGet = true)]
        public RegisterViewModel registerViewModel { get; set; }
        public string? ErrorMessage { get; set; }

        public VerifyCodeForRegisterModel(ISessionService sesstionService)
        {
            _sessionService = sesstionService;
        }

        public void OnGet()
        {
            registerViewModel = _sessionService.GetSession<RegisterViewModel>("UserSession");
        }

        public IActionResult OnPost(string code)
        {
            registerViewModel = _sessionService.GetSession<RegisterViewModel>("UserSession");
            if (code.Equals(registerViewModel.verifyCode))
            {
                return RedirectToPage("/SelectRole");
            }
            else
            {
                ErrorMessage = "The verification code you entered is incorrect. Please try again.";
                return Page();
            }
        }
    }
}
