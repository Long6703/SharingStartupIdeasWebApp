using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;
using SSI.Services.Service;
using SSI.Ultils.ViewModel;
using System.Text.Json;

namespace SSI.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IAccountService _accountService;
        [BindProperty]
        public RegisterViewModel RegisterViewModel { get; set; }
        private readonly EmailService _emailService;
        public string ErrorMessage { get; set; }
        public RegisterModel(IAccountService accountService, EmailService emailService)
        {
            _accountService = accountService;
            _emailService = emailService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("RegisterViewModel.Role");
            ModelState.Remove("RegisterViewModel.Status");
            ModelState.Remove("RegisterViewModel.verifyCode");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_accountService.CheckEmail(RegisterViewModel.Email))
            {
                ErrorMessage = "Email already exists!";
                return Page();
            }

            string otp = GenerateOTP();
            await sendOTP(RegisterViewModel.Email, otp);
            RegisterViewModel.verifyCode = otp;
            var userJson = JsonSerializer.Serialize(RegisterViewModel);
            HttpContext.Session.Set("UserSession", System.Text.Encoding.UTF8.GetBytes(userJson));
            return RedirectToPage("/VerifyCodeForRegister");
        }

        private async Task sendOTP(string email, string otp)
        {
            string subject = "SSI Verify Code To Register Account!";
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
