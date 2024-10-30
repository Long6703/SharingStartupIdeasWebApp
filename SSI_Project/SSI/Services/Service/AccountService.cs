using AutoMapper;
using SSI.Data.IRepositoryBase;
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

        public async Task Register(RegisterViewModel registerViewModel)
        {
            var user = _mapper.Map<User>(registerViewModel);
            await _accountRepository.RegisterAsync(user);
        }

    }
}
