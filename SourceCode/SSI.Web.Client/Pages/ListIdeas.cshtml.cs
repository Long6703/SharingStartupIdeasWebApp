using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;
using SSI.Services.Service;
using SSI.Share.Domain;

namespace SSI.Web.Client.Pages
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
        public ListIdeasModel(IIdeaService ideaService, ICategoryService categoryService)
        {
            _ideaService = ideaService;
            _categoryService = categoryService;
        }
        public void OnGet()
        {
            Categories = _categoryService.GetAllCategories();
            Ideas = _ideaService.SearchIdeas(SearchTerm, SelectedCategoryId);
        }
    }
}
