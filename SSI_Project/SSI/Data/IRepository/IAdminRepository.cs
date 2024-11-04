namespace SSI.Data.IRepository
{
    public interface IAdminRepository
    {
        IEnumerable<Models.User> GetAllUsers();

    }
}
