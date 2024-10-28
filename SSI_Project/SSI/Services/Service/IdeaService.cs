using SSI.Data.IRepository;
using SSI.Models;
using SSI.Services.IService;

namespace SSI.Services.Service
{
    public class IdeaService : IIdeaService
    {
        private readonly IIdeaRepository _ideaRepository;

        public IdeaService(IIdeaRepository ideaRepository)
        {
            _ideaRepository = ideaRepository;
        }

        public async Task<IEnumerable<Idea>> GetIdeasByUserIdAndRoleAsync(int userId, string role, int pageNumber, int pageSize)
        {
            return await _ideaRepository.GetIdeasByUserIdAndRoleAsync(userId, role, pageNumber, pageSize);
        }

        public async Task<int> GetTotalIdeasCountByUserIdAndRoleAsync(int userId, string role)
        {
            return await _ideaRepository.GetTotalIdeasCountByUserIdAndRoleAsync(userId, role);
        }
        public async Task<Idea?> GetIdeaWithDetailsAsync(int ideaId)
        {
            return await _ideaRepository.GetIdeaWithDetailsAsync(ideaId);
        }
    }
}
