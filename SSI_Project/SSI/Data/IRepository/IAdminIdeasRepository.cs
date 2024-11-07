using SSI.Models;

namespace SSI.Data.IRepository
{
    public interface IAdminIdeasRepository
    {
        IEnumerable<Models.Idea> GetIdeas();
        void ApproveIdeas(int id);
        void RejectIdeas(int id);
        Models.Idea GetIdeaById(int id);

        Models.InvestmentRequest GetInvestmentById(int id);
        
        ICollection<Ideadetail> GetIdeaDetailByIdeaId(int id);
        ICollection<Image> GetImagesByIdDetail(int id);

        ICollection<Idea> GetIdeasByUser(int userId);
    }

}
