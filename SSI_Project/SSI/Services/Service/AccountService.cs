using AutoMapper;
using SSI.Data.IRepository;
using SSI.Data.Repository;
using SSI.Models;
using SSI.Services.IService;
using SSI.Ultils.ViewModel;

namespace SSI.Services.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        public AccountService(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public bool ChangePassword(string newpassword, string userEmail)
        {
            return _accountRepository.ChangePasswordAsync(newpassword, userEmail);
        }

        public bool CheckEmail(string email)
        {
            return _accountRepository.CheckEmail(email);
        }

        public bool CheckPassword(string email, string password)
        {
            return _accountRepository.CheckPassword(email, password);
        }

        public UserViewModel GetUserByEmail(string email)
        {
            return _accountRepository.GetUserByEmail(email);
        }

        public UserViewModel Login(LoginViewModel loginViewModel)
        {
            var user = _accountRepository.LoginAsync(loginViewModel);
            var userViewModel = _mapper.Map<UserViewModel>(user);
            return userViewModel;
        }

        public async Task Register(RegisterViewModel registerViewModel)
        {
            var user = _mapper.Map<User>(registerViewModel);
            await _accountRepository.RegisterAsync(user);
        }

        public Task<bool> UpdateProfile(UserViewModel userViewModel)
        {
            return _accountRepository.UpdateProfileAsync(userViewModel);
        }
    }
}
