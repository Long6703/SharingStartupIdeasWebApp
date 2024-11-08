using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;
using SSI.Services.Service;
using SSI.Ultils.ViewModel;
using System.Text.Json;

namespace SSI.Pages
{
    public class SelectRoleModel : PageModel
    {
        private readonly ISessionService _sessionService;
        private readonly IAccountService _accountService;
        [BindProperty]
        public RegisterViewModel registerViewModel { get; set; }
        public bool check = false;
        public string? ErrorMessage { get; set; }
        public SelectRoleModel(ISessionService sessionService, IAccountService accountService)
        {
            _sessionService = sessionService;
            _accountService = accountService;
        }
        public void OnGet()
        {
            registerViewModel = _sessionService.GetSession<RegisterViewModel>("UserSession");
        }

        public async Task<IActionResult> OnPost(string role)
        {
            registerViewModel = _sessionService.GetSession<RegisterViewModel>("UserSession");
            registerViewModel.Role = role;
            await _accountService.Register(registerViewModel);
            return RedirectToPage("/Index");
        }
    }
}
