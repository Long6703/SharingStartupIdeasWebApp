using SSI.Ultils.ViewModel;

namespace SSI.Data.IRepository
{
    public interface IAdminRepository
    {
        IEnumerable<Models.User> GetAllUsers();
        Models.User GetUser(int id);
        Models.Category GetCategory(int id);
        void LockAccount(int id);
        void UnlockAccount(int id);

        int CountNoSuccesInvest(int userId);

        int CountNoRejecrInvest(int userId);
        decimal SumAmountInvest(int investorId);

        decimal SumAmountIncome();

        int CountTransaction();

        int CountIdeas();

        IEnumerable<MonthlyStats> GetMonthlyRevenueAndIdeas();

        IEnumerable<UserMonthly> GetMonthlyInvestAndFounder();
    }
}
