using SSI.Models;
using SSI.Ultils.ViewModel;

namespace SSI.Data.IRepository
{
    public interface IAccountRepository : IRepositoryBase<User>
    {
        Task RegisterAsync(User user);

        User LoginAsync(LoginViewModel loginViewModel);
    }
}
