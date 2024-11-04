namespace SSI.Data.IRepository
{
    public interface IAdminRepository
    {
        IEnumerable<Models.User> GetAllUsers();

        void LockAccount(int id);
        void UnlockAccount(int id);
    }
}
