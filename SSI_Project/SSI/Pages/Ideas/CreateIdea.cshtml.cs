using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Models;
using SSI.Services.IService;
using SSI.Services.Service;
using SSI.Ultils.ViewModel;
using System.Text.Json;

namespace SSI.Pages.Ideas
{
    public class CreateIdeaModel : PageModel
    {
        private readonly IIdeaService _ideaService;
        private readonly ICategoryService _categoryService;
        private readonly CloudinaryService _cloudinaryService;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<CreateIdeaModel> _logger;

        [BindProperty]
        public Idea Idea { get; set; }

        [BindProperty]
        public Ideadetail Ideadetail { get; set; }

        [BindProperty]
        public IFormFile PosterImage { get; set; }

        [BindProperty]
        public List<IFormFile> Images { get; set; }

        public List<Category> Categories { get; set; }

        public CreateIdeaModel(IIdeaService ideaService, ICategoryService categoryService,
                               CloudinaryService cloudinaryService, IWebHostEnvironment environment,
                               ILogger<CreateIdeaModel> logger)
        {
            _ideaService = ideaService;
            _categoryService = categoryService;
            _cloudinaryService = cloudinaryService;
            _environment = environment;
            _logger = logger;
        }

        public void OnGet()
        {
            Categories = _categoryService.GetAllCategories();
        }

        public async Task<IActionResult> OnPostAsync(int userId)
        {
            var session = HttpContext.Session;
            if (session.TryGetValue("UserSession", out var userBytes))
            {
                var userJson = System.Text.Encoding.UTF8.GetString(userBytes);
                var userViewModel = JsonSerializer.Deserialize<UserViewModel>(userJson);
                if (userViewModel != null)
                {
                    userId = userViewModel.UserId;
                }
            }

            try
            {
                Idea.UserId = userId;
                Idea.Status = "pending";
                Idea.CreatedAt = DateTime.UtcNow;

                // Upload Poster Image to Cloudinary using temporary file path
                if (PosterImage != null && PosterImage.Length > 0)
                {
                    var tempPosterPath = Path.GetTempFileName(); // Create a temporary file path
                    using (var stream = new FileStream(tempPosterPath, FileMode.Create))
                    {
                        await PosterImage.CopyToAsync(stream);
                    }

                    // Upload to Cloudinary and delete the temporary file
                    Idea.PosterImg = await _cloudinaryService.UploadImageAsync(tempPosterPath);
                    System.IO.File.Delete(tempPosterPath); // Delete temp file after upload
                }

                await _ideaService.CreateIdeaAsync(Idea);

                Ideadetail.CreatedAt = DateTime.UtcNow;
                Ideadetail.IdeaId = Idea.IdeaId;

                List<Image> imagesToSave = new List<Image>();

                // Upload additional images to Cloudinary using temporary file paths
                foreach (var formFile in Images)
                {
                    if (formFile.Length > 0)
                    {
                        var tempImagePath = Path.GetTempFileName(); // Create a temporary file path
                        using (var stream = new FileStream(tempImagePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }

                        // Upload to Cloudinary and delete the temporary file
                        var imageUrl = await _cloudinaryService.UploadImageAsync(tempImagePath);
                        imagesToSave.Add(new Image { Url = imageUrl, IdeaDetailId = Ideadetail.IdeaDetailId });
                        System.IO.File.Delete(tempImagePath); // Delete temp file after upload
                    }
                }

                await _ideaService.CreateIdeaWithDetailAsync(Ideadetail);

                // Save image URLs to the database
                foreach (var image in imagesToSave)
                {
                    image.IdeaDetailId = Ideadetail.IdeaDetailId;
                    await _ideaService.CreateImageAsync(image);
                }

                return RedirectToPage("/Ideas/StartupIdeaList", new { id = userId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the idea.");
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request. Please try again.");
                Categories = _categoryService.GetAllCategories();
                return Page();
            }
        }

    }
}
