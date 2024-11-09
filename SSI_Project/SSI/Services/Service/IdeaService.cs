using SSI.Data.IRepository;
using SSI.Services.IService;
using SSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
        }

        public List<Idea> SearchIdeas(string searchTerm, int? categoryId)
        {
            return _ideaRepository.SearchIdeas(searchTerm, categoryId);
        }

        public (Idea, int, List<Comment>) GetIdeaById(int id)
        {
            return _ideaRepository.GetIdeaById(id);
        }

        public (Ideadetail, List<Comment>) GetMilestoneDetailsById(int milestoneId)
        {
            return _ideaRepository.GetMilestoneDetailsById(milestoneId);
        }

        public void AddComment(Comment comment)
        {
            _ideaRepository.AddComment(comment);
        }
        public List<Idea> RelatedIdea(int ideaId)
        {
            return _ideaRepository.RelatedIdea(ideaId);
        }
        public void AddIdeasToInterestList(IdeaInterest ideaInterest)
        {
            _ideaRepository.AddIdeasToInterestList(ideaInterest);
        }

        public bool IsIdeaInInterestList(int ideaId, int userId)
        {
            return _ideaRepository.IsIdeaInInterestList(ideaId, userId);
        }

        public List<IdeaInterest> GetInterestList(int userId)
        {
            return _ideaRepository.GetInterestList(userId);
        }

        public void DeleteInterest(int interestId)
        {
            _ideaRepository.DeleteInterest(interestId);
        }

        public Dictionary<string, int> countNumber()
        {
            return _ideaRepository.countNumber();
        }

        public List<User> ProminentInvestor()
        {
            return _ideaRepository.ProminentInvestor();
        }

        public List<Idea> GetNewIdea()
        {
            return _ideaRepository.GetNewIdea();
        }
        public async Task CreateIdeaAsync(Idea idea)
        {
            idea.Status = "Pending";
            idea.CreatedAt = DateTime.UtcNow;

            await _ideaRepository.CreateIdeaAsync(idea);
        }
        public async Task CreateIdeaWithDetailAsync(Ideadetail ideaDetail)
        {
            await _ideaRepository.CreateIdeaDetailAsync(ideaDetail); 
        }
        public async Task CreateImageAsync(Image image)
        {
            await _ideaRepository.CreateImageAsync(image);
        }
        public async Task AddCommentAsync(Comment comment)
        {
            await _ideaRepository.AddCommentAsync(comment);
        }
    }
}
