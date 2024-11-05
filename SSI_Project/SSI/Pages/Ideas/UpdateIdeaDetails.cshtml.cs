using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SSI.Models;
using SSI.Services.IService;
using System.IO;

namespace SSI.Pages.Ideas
{
    public class UpdateIdeaDetailsModel : PageModel
    {
        private readonly IIdeaService _ideaService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UpdateIdeaDetailsModel(IIdeaService ideaService, IWebHostEnvironment webHostEnvironment)
        {
            _ideaService = ideaService;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Ideadetail NewIdeaDetail { get; set; } = new Ideadetail();

        [BindProperty]
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();

        public int IdeaId { get; set; }

        public async Task<IActionResult> OnGetAsync(int ideaId)
        {
            // Set the IdeaId for the new milestone
            IdeaId = ideaId;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int ideaId)
        {
            // Nếu model không hợp lệ, trả về trang hiện tại với thông báo lỗi
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            // Thiết lập các thuộc tính cho NewIdeaDetail
            NewIdeaDetail.IdeaId = ideaId;
            NewIdeaDetail.CreatedAt = DateTime.UtcNow;

            // Xử lý upload hình ảnh
            var imageList = new List<Image>();
            if (Images != null && Images.Count > 0)
            {
                var uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                foreach (var formFile in Images)
                {
                    if (formFile.Length > 0)
                    {
                        var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(formFile.FileName)}";
                        var filePath = Path.Combine(uploadFolder, fileName);

                        try
                        {
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await formFile.CopyToAsync(stream);
                            }

                            // Thêm hình ảnh vào danh sách
                            imageList.Add(new Image
                            {
                                Url = $"/images/{fileName}"
                            });
                        }
                        catch (Exception ex)
                        {
                            // Log lỗi nếu có vấn đề trong khi upload file
                            ModelState.AddModelError("", "Could not upload image: " + ex.Message);
                            return Page();
                        }
                    }
                }
            }

            // Gán danh sách hình ảnh vào NewIdeaDetail
            NewIdeaDetail.Images = imageList;

            // Lưu mới chi tiết ý tưởng vào cơ sở dữ liệu
            
            await _ideaService.CreateIdeaWithDetailAsync(NewIdeaDetail);
            return RedirectToPage("/Ideas/StartupIdeaMilestoneDetails", new { id = ideaId });
        }
    }
}
