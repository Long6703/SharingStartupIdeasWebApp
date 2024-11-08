using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;
using SSI.Services.Service;
using SSI.Ultils;

namespace SSI.Pages
{
    public class VerifyCodeForForgotpasswordModel : PageModel
    {
        private readonly ISessionService _sessionService;
        private readonly EmailService _emailService;
        [BindProperty(SupportsGet = true)]
        public string userEmail { get; set; }
        public string ErrorMessage { get; set; }
        private readonly IAccountService _accountService;
        public VerifyCodeForForgotpasswordModel(ISessionService sessionService, EmailService emailService, IAccountService accountService)
        {
            _sessionService = sessionService;
            _emailService = emailService;
            _accountService = accountService;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost(string otp)
        {
            string sessionOTP = _sessionService.GetSession<string>("OTP");
            if (otp != sessionOTP)
            {
                ErrorMessage = "Invalid OTP!";
                return Page();
            }
            _sessionService.RemoveSession("OTP");
            string newPassword = PasswordGenerator.GeneratePassword();
            await _emailService.SendEmailAsync(userEmail, "SSI Reset Password", "New passowrd is : " + newPassword);
            if(_accountService.ChangePassword(newPassword, userEmail))
            {
                return RedirectToPage("Login");
            }
            return Page();
        }
    }
}
