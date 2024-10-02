using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;

namespace SSI.Web.Client.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ITestService _testService;
        public string Message { get; set; } = string.Empty;

        public IndexModel(ILogger<IndexModel> logger, ITestService testService)
        {
            _logger = logger;
            _testService = testService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _testService.TestAddRoleAsync();
                Message = "Add role successfully!";
            }
            catch (Exception ex)
            {
                Message = $"Failed to add role: {ex.Message}";
            }

            return Page();
        }
    }
}
