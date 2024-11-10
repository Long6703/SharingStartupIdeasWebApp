using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;
using SSI.Services.Service;
using SSI.Ultils.ViewModel;

namespace SSI.Pages
{
    [Authorize]
    public class UserProfileModel : PageModel
    {
        private readonly ISessionService _sessionService;
        [BindProperty(SupportsGet = true)]
        public UserViewModel userViewModel { get; set; }
        private readonly CloudinaryService _cloudinaryService;
        private readonly IAccountService _accountService;
        public IFormFile Avatar { get; set; }
        public string AvatarUrl { get; set; }


        public UserProfileModel(ISessionService sessionService, CloudinaryService cloudinaryService, IAccountService accountService)
        {
            _sessionService = sessionService;
            _cloudinaryService = cloudinaryService;
            _accountService = accountService;
        }
        public IActionResult OnGet()
        {
            userViewModel = _sessionService.GetSession<UserViewModel>("UserSession");
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateAvatarAsync()
        {
            if (Avatar != null && Avatar.Length > 0)
            {
                var filePath = Path.Combine(Path.GetTempPath(), Avatar.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Avatar.CopyToAsync(stream);
                }

                try
                {
                    AvatarUrl = await _cloudinaryService.UploadImageAsync(filePath);

                    var user = _sessionService.GetSession<UserViewModel>("UserSession");
                    user.AvatarUrl = AvatarUrl;
                    bool check = await _accountService.UpdateProfile(user);
                    if (check)
                    {
                        _sessionService.SetSession("UserSession", user);
                        return RedirectToPage("UserProfile");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Update failed");
                        return Page();
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return Page();
                }
            }
            return RedirectToPage("/UserProfile");
        }
    }
}
