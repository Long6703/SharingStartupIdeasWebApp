using SSI.Data.IRepository;
using SSI.Share.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.Data.Repository
{
    public class TestRepository : RepositoryBase<Share.Domain.Role>, ITestRepository
    {
        public TestRepository(SSIContext context) : base(context)
        {
        }

        public async Task TestAddRoleAsync()
        {
            var newRole = new Role
            {
                RoleName = "Admin"
            };

            await AddAsync(newRole);

            await SaveChangesAsync();
        }
    }
}
