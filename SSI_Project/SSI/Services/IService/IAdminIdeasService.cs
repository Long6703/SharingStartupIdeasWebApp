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


        ICollection<Image> GetImagesById(int id);

        ICollection<Idea> GetIdeasByFounder(int id);

        ICollection<Idea> GetIdeasByInvestore(int id);

        //ideadetials
        ICollection<Ideadetail> GetIdeadetailsByIdeaId(int IdeaId);
        ICollection<Image> GetImages(int ideaDetailsId);

        public int CountIdeaDetailByIdeaId(int ideId);

        public Models.Category GetCategoryById(int id);


        ICollection<InvestmentRequest> GetInvestByIdeaId(int ideaId, int userId);

        Models.Transaction GetTransactionByReqId(int reqId, int ideaId, int userId);
    }
}
