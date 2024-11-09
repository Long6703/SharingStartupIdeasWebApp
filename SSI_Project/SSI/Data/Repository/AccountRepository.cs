using AutoMapper;
using SSI.Data.IRepository;
using SSI.Models;
using SSI.Ultils.ViewModel;

namespace SSI.Data.Repository
{
    public class AccountRepository : RepositoryBase<User>, IAccountRepository
    {   
        private readonly IMapper _mapper;
        public AccountRepository(SSIV3Context context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
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

        public UserViewModel GetUserByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            var userViewModel = _mapper.Map<UserViewModel>(user);
            return userViewModel;
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

        public async Task<bool> UpdateProfileAsync(UserViewModel userViewModel)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == userViewModel.Email);
            if (user == null)
            {
                return false;
            }
            user.Displayname = userViewModel.Displayname;
            user.Location = userViewModel.Location;
            user.Profession = userViewModel.Profession;
            user.WebsiteUrl = userViewModel.WebsiteUrl;
            user.LinkedinUrl = userViewModel.LinkedinUrl;
            user.TwitterUrl = userViewModel.TwitterUrl;
            user.FacebookUrl = userViewModel.FacebookUrl;
            user.BankAccountNumber = userViewModel.BankAccountNumber;
            user.BankName = userViewModel.BankName;
            user.Bio = userViewModel.Bio;
            user.AvatarUrl = userViewModel.AvatarUrl;
            user.Status = userViewModel.Status;

            _context.Users.Update(user);
            SaveChanges();

            return true;

        }
    }
}
