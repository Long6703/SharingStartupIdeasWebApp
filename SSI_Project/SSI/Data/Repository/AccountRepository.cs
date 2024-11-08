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

        public bool ChangePasswordAsync(string newpassword, string userEmail)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
            if (user == null)
            {
                return false;
            }
            user.Password = BCrypt.Net.BCrypt.HashPassword(newpassword);
            SaveChanges();
            return true;
        }

        public bool CheckEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            return user != null;
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

        public Task UpdateProfileAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
