using SSI.Data.IRepository;
using SSI.Models;
using SSI.Services.IService;

namespace SSI.Services.Service
{
    public class AdminIdeasService : IAdminIdeasService
    {
        private readonly IAdminIdeasRepository _repository;
        public AdminIdeasService(IAdminIdeasRepository repository) 
        {
            _repository = repository;
        }

        public void ApproveIdeas(int ideasId)
        {
             _repository.ApproveIdeas(ideasId);
        }

        public Idea GetIdeaById(int ideasId)
        {
            return _repository.GetIdeaById(ideasId);
        }



        

        public IEnumerable<Idea> GetIdeas()
        {
            return _repository.GetIdeas();
        }

        public ICollection<Idea> GetIdeasByFounder(int id)
        {
            return _repository.GetIdeasByFounder(id);
        }

        public ICollection<Idea> GetIdeasByInvestore(int id)
        {
            return _repository.GetIdeasByInvestore(id);
        }

        public ICollection<Image> GetImagesById(int id)
        {
            return _repository.GetImagesByIdDetail(id);
        }

        public InvestmentRequest GetInvestmentRequestById(int id)
        {
            return _repository.GetInvestmentById(id);
        }

        public void RejectIdeas(int ideasId)
        {
            _repository.RejectIdeas(ideasId);
        }

        //ideadetails

        public ICollection<Ideadetail> GetIdeadetailsByIdeaId(int IdeaId)
        {
            return _repository.GetIdeadetailsByIdeaId(IdeaId);
        }

        public ICollection<Image> GetImages(int ideaDetailsId)
        {
            return _repository.GetImages(ideaDetailsId);
        }

        public int CountIdeaDetailByIdeaId(int ideId)
        {
            return _repository.CountIdeaDetailByIdeaId(ideId);
        }

        public Models.Category GetCategoryById(int ideId)
        {
            return _repository.GetCategoryById(ideId);
        }
    }
}
