using SSI.Data.IRepository;
using SSI.Models;
using SSI.Ultils.ViewModel;

namespace SSI.Data.Repository
{
    public class AccountRepository : RepositoryBase<User>, IAccountRepository
    {   
        public AccountRepository(SSIV2Context context) : base(context)
        {
        }

        public User LoginAsync(LoginViewModel loginViewModel)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == loginViewModel.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginViewModel.Password, user.Password))
            {
                return null;
            }
            return user;
        }

        public async Task RegisterAsync(User user)
        {
            await AddAsync(user);
            SaveChanges();
        }
    }
}
