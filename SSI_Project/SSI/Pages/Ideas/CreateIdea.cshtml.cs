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
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<CreateIdeaModel> _logger;

        [BindProperty]
        public Idea Idea { get; set; }

        [BindProperty]
        public Ideadetail Ideadetail { get; set; }

        [BindProperty]
        public IFormFile PosterImage { get; set; }  // Single image for poster_img

        [BindProperty]
        public List<IFormFile> Images { get; set; }  // Multiple images for Ideadetail

        public List<Category> Categories { get; set; }

        public CreateIdeaModel(IIdeaService ideaService, ICategoryService categoryService,
                               IWebHostEnvironment environment, ILogger<CreateIdeaModel> logger)
        {
            _ideaService = ideaService;
            _categoryService = categoryService;
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
            //    if (!ModelState.IsValid)
            //{
            //    Categories = _categoryService.GetAllCategories();
            //    return Page();
            //}

            try
            {
                // Tạo đối tượng Idea và gán các thuộc tính
                Idea.UserId = userId;
                Idea.Status = "Pending";
                Idea.CreatedAt = DateTime.UtcNow;

                // 1. Xử lý Poster Image cho Idea
                if (PosterImage != null && PosterImage.Length > 0)
                {
                    var posterFileName = Guid.NewGuid() + Path.GetExtension(PosterImage.FileName);
                    var posterPath = Path.Combine(_environment.WebRootPath, "images", posterFileName);

                    using (var stream = new FileStream(posterPath, FileMode.Create))
                    {
                        await PosterImage.CopyToAsync(stream);
                    }

                    Idea.PosterImg = posterFileName;
                }

                // Lưu Idea vào database trước để lấy IdeaId
                await _ideaService.CreateIdeaAsync(Idea);

                // 2. Xử lý và lưu các hình ảnh cho Ideadetail
                Ideadetail.CreatedAt = DateTime.UtcNow;
                Ideadetail.IdeaId = Idea.IdeaId; // Gán IdeaId vừa tạo vào Ideadetail

                // Danh sách hình ảnh để lưu vào cơ sở dữ liệu
                List<Image> imagesToSave = new List<Image>();
                var imagesDirectory = Path.Combine(_environment.WebRootPath, "images");

                foreach (var formFile in Images)
                {
                    if (formFile.Length > 0)
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(formFile.FileName);
                        var filePath = Path.Combine(imagesDirectory, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }

                        // Thêm hình ảnh vào danh sách để lưu vào cơ sở dữ liệu
                        imagesToSave.Add(new Image { Url = fileName, IdeaDetailId = Ideadetail.IdeaDetailId });
                    }
                }

                // Lưu chi tiết ý tưởng và hình ảnh
                await _ideaService.CreateIdeaWithDetailAsync(Ideadetail);

                foreach (var image in imagesToSave)
                {
                    image.IdeaDetailId = Ideadetail.IdeaDetailId;
                    await _ideaService.CreateImageAsync(image);
                }

                return RedirectToPage("StartupIdeaList");
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
