using SSI.Data.IRepository;
using SSI.Services.IService;
using SSI.Share.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.Services.Service
{
    public class IdeaService : IIdeaService
    {
        private readonly IIdeaRepository _ideaRepository;
        public IdeaService(IIdeaRepository ideaRepository)
        {
            _ideaRepository = ideaRepository;
        }

        public List<Idea> SearchIdeas(string searchTerm, int? categoryId)
        {
            return _ideaRepository.SearchIdeas(searchTerm, categoryId);
        }
    }
}
