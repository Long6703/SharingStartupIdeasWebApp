using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Models;
using SSI.Services.IService;
using SSI.Services.Service;
using SSI.Ultils.ViewModel;
using System.IO;
using System.Text.Json;

namespace SSI.Pages.Ideas
{
    public class UpdateIdeaDetailsModel : PageModel
    {
        private readonly IIdeaService _ideaService;
        private readonly CloudinaryService _cloudinaryService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<UpdateIdeaDetailsModel> _logger;

        [BindProperty]
        public Ideadetail NewIdeaDetail { get; set; } = new Ideadetail();

        [BindProperty]
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();

        public int IdeaId { get; set; }

        public UpdateIdeaDetailsModel(IIdeaService ideaService, CloudinaryService cloudinaryService,
                                       IWebHostEnvironment webHostEnvironment, ILogger<UpdateIdeaDetailsModel> logger)
        {
            _ideaService = ideaService;
            _cloudinaryService = cloudinaryService;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(int ideaId)
        {
            IdeaId = ideaId;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int ideaId)
        {
            NewIdeaDetail.IdeaId = ideaId;
            NewIdeaDetail.CreatedAt = DateTime.UtcNow;

            var imageList = new List<Image>();

            if (Images != null && Images.Count > 0)
            {
                foreach (var formFile in Images)
                {
                    if (formFile.Length > 0)
                    {
                        try
                        {
                            // Tạo file tạm thời để upload lên Cloudinary
                            var tempImagePath = Path.GetTempFileName(); // Create a temporary file path
                            using (var stream = new FileStream(tempImagePath, FileMode.Create))
                            {
                                await formFile.CopyToAsync(stream);
                            }

                            // Upload ảnh lên Cloudinary và xóa file tạm
                            var imageUrl = await _cloudinaryService.UploadImageAsync(tempImagePath);
                            imageList.Add(new Image { Url = imageUrl });

                            System.IO.File.Delete(tempImagePath); // Xóa file tạm sau khi upload
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "An error occurred while uploading image.");
                            ModelState.AddModelError("", "Could not upload image: " + ex.Message);
                            return Page();
                        }
                    }
                }
            }

            NewIdeaDetail.Images = imageList;

            try
            {
                // Lưu chi tiết ý tưởng vào cơ sở dữ liệu
                await _ideaService.CreateIdeaWithDetailAsync(NewIdeaDetail);

                // Trả về trang chi tiết ý tưởng
                return RedirectToPage("/Ideas/StartupIdeaDetails", new { id = ideaId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating idea details.");
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again.");
                return Page();
            }
        }
    }
}
