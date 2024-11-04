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
        public IEnumerable<Idea> GetIdeas()
        {
            return _repository.GetIdeas();
        }
    }
}
