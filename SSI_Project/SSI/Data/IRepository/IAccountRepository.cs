using SSI.Models;

namespace SSI.Data.IRepositoryBase
{
    public interface IAccountRepository : IRepositoryBase<User>
    {
        Task RegisterAsync(User user);
    }
}
