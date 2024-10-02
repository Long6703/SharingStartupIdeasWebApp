using SSI.Data.IRepository;
using SSI.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.Services.Service
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;

        public TestService(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        public async Task TestAddRoleAsync()
        {
            await _testRepository.TestAddRoleAsync();
        }
    }
}
