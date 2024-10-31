using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Models;
using SSI.Services.IService;
using SSI.Services.Service;

namespace SSI.Pages.Ideas
{
    public class CreateIdeaModel : PageModel
    {
        private readonly IIdeaService _ideaService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _environment;

        [BindProperty]
        public Idea Idea { get; set; }

        [BindProperty]
        public Ideadetail Ideadetail { get; set; }

        [BindProperty]
        public List<IFormFile> Images { get; set; }

        public List<Category> Categories { get; set; }

        public CreateIdeaModel(IIdeaService ideaService, ICategoryService categoryService, IWebHostEnvironment environment)
        {
            _ideaService = ideaService;
            _categoryService = categoryService;
            _environment = environment;
        }

        public void OnGetAsync()
        {
            // Load categories to populate the dropdown
            Categories =  _categoryService.GetAllCategories();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    // Reload categories if form submission fails
            //    Categories = await _categoryService.GetAllCategoriesAsync();
            //    return Page();
            //}

            // Set initial properties for the new idea
            Idea.Status = "Pending";
            Idea.CreatedAt = DateTime.UtcNow;
            Ideadetail.CreatedAt = DateTime.UtcNow;

            // Save each uploaded image file to wwwroot/images and add to Ideadetail
            Ideadetail.Images = new List<Image>();
            foreach (var formFile in Images)
            {
                if (formFile.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                    var filePath = Path.Combine(_environment.WebRootPath, "images", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    Ideadetail.Images.Add(new Image { Url = fileName });
                }
            }

            // Associate Ideadetail with Idea
            Idea.Ideadetails = new List<Ideadetail> { Ideadetail };

            // Save the idea and detail using the service
            await _ideaService.CreateIdeaWithDetailAsync(Idea);

            return RedirectToPage("IdeaList");
        }
    }
}
