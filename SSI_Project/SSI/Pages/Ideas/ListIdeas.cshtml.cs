using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Models;
using SSI.Services.IService;
using SSI.Services.Service;
using System.Text.Json;

namespace SSI.Pages.Ideas
{
    public class ListIdeasModel : PageModel
    {
        private readonly IIdeaService _ideaService;
        private readonly ICategoryService _categoryService;
        public List<Idea> Ideas { get; set; } = new List<Idea>();
        public List<Category> Categories { get; set; } = new List<Category>();
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SelectedCategoryId { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 3;
        public int TotalPages { get; set; }
        public ListIdeasModel(IIdeaService ideaService, ICategoryService categoryService)
        {
            _ideaService = ideaService;
            _categoryService = categoryService;
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
            Categories = _categoryService.GetAllCategories();
            var ideas = _ideaService.SearchIdeas(SearchTerm, SelectedCategoryId);
            TotalPages = (int)Math.Ceiling(ideas.Count / (double)PageSize);

            Ideas = ideas
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToList();
            return Page();
        }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}
