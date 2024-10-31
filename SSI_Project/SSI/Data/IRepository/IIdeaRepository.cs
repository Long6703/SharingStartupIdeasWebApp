using SSI.Models;

namespace SSI.Data.IRepository
{
    public interface IIdeaRepository
    {
        Task<IEnumerable<Idea>> GetIdeasByUserIdAndRoleAsync(int userId, string role, int pageNumber, int pageSize);
        Task<int> GetTotalIdeasCountByUserIdAndRoleAsync(int userId, string role);
        Task<Idea?> GetIdeaWithDetailsAsync(int ideaId);
        Task<Ideadetail?> GetMilestoneDetailByIdAsync(int ideaDetailId);
    }
}
