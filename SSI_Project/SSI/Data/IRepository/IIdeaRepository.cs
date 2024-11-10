using SSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.Data.IRepository
{
    public interface IIdeaRepository
    {
        Task<IEnumerable<Idea>> GetIdeasByUserIdAndRoleAsync(int userId, string role, int pageNumber, int pageSize);
        Task<int> GetTotalIdeasCountByUserIdAndRoleAsync(int userId, string role);
        Task<Idea?> GetIdeaWithDetailsAsync(int ideaId);
        Task<Ideadetail?> GetMilestoneDetailByIdAsync(int ideaDetailId);
        (Idea,int, List<Comment>) GetIdeaById(int id);
        List<Idea> SearchIdeas(string searchTerm, int? categoryId);
        (Ideadetail, List<Comment>) GetMilestoneDetailsById(int milestoneId);
        void AddComment(Comment comment);
        List<Idea> RelatedIdea(int ideaId);
        void AddIdeasToInterestList(IdeaInterest ideaInterest);
        bool IsIdeaInInterestList(int ideaId, int userId);
        List<IdeaInterest> GetInterestList(int userId);
        void DeleteInterest(int interestId);
        Dictionary<string, int> countNumber();
        List<User> ProminentInvestor();
        List<Idea> GetNewIdea();
        Task CreateIdeaAsync(Idea idea);
        Task CreateIdeaDetailAsync(Ideadetail ideaDetail);
        Task CreateImageAsync(Image image);
        Task AddCommentAsync(Comment comment);
    }
}
