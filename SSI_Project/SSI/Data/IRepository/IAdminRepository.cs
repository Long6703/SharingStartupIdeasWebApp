namespace SSI.Data.IRepository
{
    public interface IAdminRepository
    {
        IEnumerable<Models.User> GetAllUsers();
        Models.User GetUser(int id);
        Models.Category GetCategory(int id);
        void LockAccount(int id);
        void UnlockAccount(int id);
        decimal GetTotalAmount(int id);
        int GetTotalIdeasByUserId(int userId);
    }
}
