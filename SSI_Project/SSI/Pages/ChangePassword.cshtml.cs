using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;
using SSI.Ultils.EditModel;
using SSI.Ultils.ViewModel;

namespace SSI.Pages
{
    [Authorize]
    public class ChangePasswordModel : PageModel
    {
        private readonly ISessionService _sessionService;
        private readonly IAccountService _accountService;
        public string ErrorMessage { get; set; }

        [BindProperty]
        public ChangePasswordEditModel changePasswordModel { get; set; }

        public ChangePasswordModel(ISessionService sessionService, IAccountService accountService)
        {
            _accountService = accountService;
            _sessionService = sessionService;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userSession = _sessionService.GetSession<UserViewModel>("UserSession");

            if (!_accountService.CheckPassword(userSession.Email, changePasswordModel.CurrentPassword))
            {
                ErrorMessage = "Current Password is incorrect";
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var isPasswordChanged = _accountService.ChangePassword(changePasswordModel.NewPassword, userSession.Email);

            if (isPasswordChanged)
            {
                return RedirectToPage("Logout");
            }else
            {
                return Page();
            }
        }
    }
}
