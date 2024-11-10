using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;
using SSI.Services.Service;
using SSI.Ultils.ViewModel;

namespace SSI.Pages
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly EmailService _emailService;
        private readonly ISessionService _sessionService;
        private readonly IAccountService _accountService;
        public string ErrorMessage { get; set; }

        public ForgotPasswordModel(EmailService emailService, ISessionService sessionService, IAccountService accountService)
        {
            _emailService = emailService;
            _sessionService = sessionService;
            _accountService = accountService;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ErrorMessage = "Email is required!";
                return Page();
            }

            if(!_accountService.CheckEmail(email)) {
                ErrorMessage = "Email is not exist!";
                return Page();
            }

            string otp = GenerateOTP();
            await sendOTP(email, otp);
            _sessionService.SetSession("OTP", otp);
            return RedirectToPage("/VerifyCodeForForgotpassword", new {userEmail = email});
        }

        private async Task sendOTP(string email, string otp)
        {
            string subject = "SSI Verify Code To Forgot Password!";
            string body = "Your OTP is: " + otp;
            await _emailService.SendEmailAsync(email, subject, body);

        }

        private string GenerateOTP()
        {
            Random random = new Random();
            int otp = random.Next(100000, 999999);
            return otp.ToString();
        }
    }
}
