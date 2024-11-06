using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Models;
using SSI.Services.IService;
using System.Text.Json;

namespace SSI.Pages.Ideas
{
    public class InterestListModel : PageModel
    {
        private readonly IIdeaService _ideaService;
        public List<IdeaInterest> InterestList { get; set; } = new();
        public InterestListModel(IIdeaService ideaService)
        {
            _ideaService = ideaService;
        }
        public IActionResult OnGet()
        {
            var userSessionBytes = HttpContext.Session.Get("UserSession");
            if (userSessionBytes == null)
            {
                return RedirectToPage("/Login");
            }

            var userJson = System.Text.Encoding.UTF8.GetString(userSessionBytes);
            var user = JsonSerializer.Deserialize<User>(userJson);

            if (user == null || user.UserId == 0)
            {
                return RedirectToPage("/Login");
            }
            InterestList = _ideaService.GetInterestList(user.UserId);
            return Page();
        }
        public IActionResult OnPostDelete(int interestId)
        {
            _ideaService.DeleteInterest(interestId);
            return RedirectToPage(); 
        }
    }
}
