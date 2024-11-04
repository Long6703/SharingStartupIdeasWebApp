using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
//using SSI.Models;
using SSI.Services.IService;
using SSI.Services.Service;
using SSI.Ultils.ViewModel;

namespace SSI.Web.Client.Pages
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly IAccountService _accountService;
        [BindProperty]
        public RegisterViewModel RegisterViewModel { get; set; }
        private readonly EmailService _emailService;
        public RegisterModel(IAccountService accountService, EmailService emailService)
        {
            _accountService = accountService;
            _emailService = emailService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string Role)
        {
            Console.WriteLine("1");
            ModelState.Remove("RegisterViewModel.Role");
            ModelState.Remove("RegisterViewModel.Status");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            RegisterViewModel.Role = Role;
            await _accountService.Register(RegisterViewModel);
            return RedirectToPage("/Login");
        }

        private async Task sendOTP(string email, string otp)
        {
            string subject = GenerateOTP();
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
