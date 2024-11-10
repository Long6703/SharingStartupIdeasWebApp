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
        

        ICollection<Image> GetImagesByIdDetail(int id);

        ICollection<Idea> GetIdeasByFounder(int userId);

        ICollection<Idea> GetIdeasByInvestore(int userId);

        //ideadetail

        ICollection<Ideadetail> GetIdeadetailsByIdeaId(int id);
        ICollection<Image> GetImages(int ideaDetailsId);

        public int CountIdeaDetailByIdeaId(int ideId);

        public Models.Category GetCategoryById(int id);

        ICollection<InvestmentRequest> GetInvestByIdeaId(int ideaId, int useid);

        Models.Transaction GetTransactionByReqId(int reqId, int ideaId, int userId);

    }

}
