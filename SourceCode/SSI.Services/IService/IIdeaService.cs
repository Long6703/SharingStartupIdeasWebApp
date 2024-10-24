using SSI.Share.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.Services.IService
{
    public interface IIdeaService
    {
        List<Idea> SearchIdeas(string searchTerm, int? categoryId);
    }
}
