using SSI.Models;

namespace SSI.Services.IService
{
    public interface IAdminIdeasService
    {
        IEnumerable<Idea> GetIdeas();
    }
}
