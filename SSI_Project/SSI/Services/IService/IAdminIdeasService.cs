using SSI.Models;

namespace SSI.Services.IService
{
    public interface IAdminIdeasService
    {
        IEnumerable<Idea> GetIdeas();
        void ApproveIdeas(int ideasId);
        void RejectIdeas(int ideasId);

        Models.Idea GetIdeaById(int ideasId); 
        Models.InvestmentRequest GetInvestmentRequestById(int id);

        ICollection<Ideadetail> GetIdeaDetailById(int id);
        ICollection<Image> GetImagesById(int id);

        ICollection<Idea> GetIdeasByUser(int id);
    }
}
