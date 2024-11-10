using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Models;
using SSI.Services.IService;
using System.Text.Json;

namespace SSI.Pages.Ideas
{
    [Authorize(Roles = "investor")]
    public class InterestListModel : PageModel
    {
        private readonly IIdeaService _ideaService;
        public List<IdeaInterest> InterestList { get; set; } = new();
        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1; 
        public int PageSize { get; set; } = 6; 
        public int TotalPages { get; set; } 
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
            var allInterests = _ideaService.GetInterestList(user.UserId);
            TotalPages = (int)Math.Ceiling(allInterests.Count / (double)PageSize);

            InterestList = allInterests
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            return Page();
        }
        public IActionResult OnPostDelete(int interestId)
        {
            _ideaService.DeleteInterest(interestId);
            return RedirectToPage(); 
        }
    }
}
