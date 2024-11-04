using SSI.Models;

namespace SSI.Data.IRepository
{
    public interface IAdminIdeasRepository
    {
        IEnumerable<Idea> GetIdeas();
    }
}
