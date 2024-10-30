using SSI.Data.IRepositoryBase;
using SSI.Models;

namespace SSI.Data.Repository
{
    public class AccountRepository : RepositoryBase<User>, IAccountRepository
    {   
        public AccountRepository(SSIV2Context context) : base(context)
        {
        }

        public async Task RegisterAsync(User user)
        {
            await AddAsync(user);
            await SaveChangesAsync();
        }
    }
}
