using SSI.Services.IService;

namespace SSI.Services.Service
{
    public class TestService : ITestService
    {
        public string Test()
        {
            return "Test";
        }
    }
}
