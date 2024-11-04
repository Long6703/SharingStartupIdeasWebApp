using Microsoft.EntityFrameworkCore;
using SSI.Models;

namespace SSI.Services.IService
{
    public interface IAdminService
    {
        IEnumerable<Models.User> GetAllUsers();
        IEnumerable<Models.User> GetFounders();
        IEnumerable<Models.User> GetInvestors();

        void LockAccount(int id);
        void UnlockAccount(int id);
    }
}
