using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;
using SSI.Ultils.ViewModel;

namespace SSI.Web.Client.Pages
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly IAccountService _accountService;
        [BindProperty]
        public RegisterViewModel RegisterViewModel { get; set; }
        public RegisterModel(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost(string Role)
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            RegisterViewModel.Role = Role;

            _accountService.Register(RegisterViewModel);
            return Page();
        }
    }
}
