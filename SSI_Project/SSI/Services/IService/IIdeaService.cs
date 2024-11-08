using SSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.Services.IService
{
    public interface IIdeaService
    {
        Task<IEnumerable<Idea>> GetIdeasByUserIdAndRoleAsync(int userId, string role, int pageNumber, int pageSize);
        Task<int> GetTotalIdeasCountByUserIdAndRoleAsync(int userId, string role);
        Task<Idea?> GetIdeaWithDetailsAsync(int ideaId);
        Task<Ideadetail?> GetMilestoneDetailByIdAsync(int ideaDetailId);
        (Idea,int, List<Comment>) GetIdeaById(int id);
        List<Idea> SearchIdeas(string searchTerm, int? categoryId);
        Task CreateIdeaAsync(Idea idea);
        Task CreateIdeaWithDetailAsync(Ideadetail ideaDetails);
        Task CreateImageAsync(Image image);
        Task AddCommentAsync(Comment comment);
    }
}
