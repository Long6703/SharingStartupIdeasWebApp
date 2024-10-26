using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Services.IService;

namespace SSI.Pages
{
    public class TestModel : PageModel
    {
        private readonly ITestService _testService;
        public string Test { get; set; }

        public TestModel(ITestService testService)
        {
            _testService = testService;
        }

        public void OnGet()
        {
            Test = _testService.Test();
        }
    }
}
