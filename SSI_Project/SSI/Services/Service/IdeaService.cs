using SSI.Data.IRepository;
using SSI.Services.IService;
using SSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<Ideadetail?> GetMilestoneDetailByIdAsync(int ideaDetailId)
        {
            return await _ideaRepository.GetMilestoneDetailByIdAsync(ideaDetailId);
        public List<Idea> SearchIdeas(string searchTerm, int? categoryId)
        {
            return _ideaRepository.SearchIdeas(searchTerm, categoryId);
        }
        public (Idea,int, List<Comment>) GetIdeaById(int id)
        {
            return _ideaRepository.GetIdeaById(id);
        }
    }
}
