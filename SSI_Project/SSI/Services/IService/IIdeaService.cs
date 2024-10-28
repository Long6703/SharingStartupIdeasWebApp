using SSI.Models;

namespace SSI.Services.IService
{
    public interface IIdeaService
    {
        Task<IEnumerable<Idea>> GetIdeasByUserIdAndRoleAsync(int userId, string role, int pageNumber, int pageSize);
        Task<int> GetTotalIdeasCountByUserIdAndRoleAsync(int userId, string role);
        Task<Idea?> GetIdeaWithDetailsAsync(int ideaId);
    }
}
