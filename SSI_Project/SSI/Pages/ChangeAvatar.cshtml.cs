using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;
using SSI.Services.Service;
using SSI.Ultils.ViewModel;

namespace SSI.Pages
{
    public class ChangeAvatarModel : PageModel
    {
        public UserViewModel userViewModel { get; set; }
        private readonly ISessionService _sessionService;
        private readonly CloudinaryService _cloudinaryService;

        [BindProperty]
        public IFormFile Avatar { get; set; }

        public ChangeAvatarModel(ISessionService sessionService, CloudinaryService cloudinaryService)
        {
            _sessionService = sessionService;
            _cloudinaryService = cloudinaryService;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("Change Avatar-----------------------------------------------------------------------------------------");
            //if (Avatar != null && Avatar.Length > 0)
            //{
            //    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "avatars", Avatar.FileName);
            //    using (var stream = new FileStream(path, FileMode.Create))
            //    {
            //        await Avatar.CopyToAsync(stream);
            //    }


            //    return RedirectToPage("/UserProfile");
            //}

            return RedirectToPage("/UserProfile");
        }
    }
}
