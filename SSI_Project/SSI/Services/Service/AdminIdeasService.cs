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

        public ICollection<Ideadetail> GetIdeaDetailById(int id)
        {
            return _repository.GetIdeaDetailByIdeaId(id);
        }

        public IEnumerable<Idea> GetIdeas()
        {
            return _repository.GetIdeas();
        }

        public ICollection<Idea> GetIdeasByUser(int id)
        {
            return _repository.GetIdeasByUser(id);
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


    }
}
