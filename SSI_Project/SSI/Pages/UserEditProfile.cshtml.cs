using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;
using SSI.Ultils.ViewModel;

namespace SSI.Pages
{
    [Authorize]
    public class UserEditProfileModel : PageModel
    {
        [BindProperty]
        public UserViewModel userViewModel { get; set; }
        private readonly ISessionService _sessionService;
        private readonly IAccountService _accountService;
        public UserEditProfileModel(ISessionService sessionService, IAccountService accountService)
        {
            _sessionService = sessionService;
            _accountService = accountService;
        }

        public IActionResult OnGet()
        {
            userViewModel = _sessionService.GetSession<UserViewModel>("UserSession");
            return Page();
        }

        public async Task<IActionResult> OnPost(UserViewModel model)
        {
            userViewModel = _sessionService.GetSession<UserViewModel>("UserSession");

            userViewModel.Displayname = model.Displayname ?? userViewModel.Displayname;
            userViewModel.Bio = model.Bio ?? userViewModel.Bio;
            userViewModel.Location = model.Location ?? userViewModel.Location;
            userViewModel.Profession = model.Profession ?? userViewModel.Profession;
            userViewModel.WebsiteUrl = model.WebsiteUrl ?? userViewModel.WebsiteUrl;
            userViewModel.LinkedinUrl = model.LinkedinUrl ?? userViewModel.LinkedinUrl;
            userViewModel.TwitterUrl = model.TwitterUrl ?? userViewModel.TwitterUrl;
            userViewModel.FacebookUrl = model.FacebookUrl ?? userViewModel.FacebookUrl;
            userViewModel.BankAccountNumber = model.BankAccountNumber ?? userViewModel.BankAccountNumber;
            userViewModel.BankName = model.BankName ?? userViewModel.BankName;

            bool isUpdated = await _accountService.UpdateProfile(userViewModel);
            if (isUpdated)
            {
                _sessionService.SetSession("UserSession", userViewModel);

                return RedirectToPage("UserProfile"); 
            }
            else
            {
                ModelState.AddModelError("", "Update failed. Please try again.");
                return Page();
            }
        }
    }
}
