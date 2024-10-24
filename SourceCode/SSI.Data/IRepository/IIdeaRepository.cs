using SSI.Share.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.Data.IRepository
{
    public interface IIdeaRepository
    {
        List<Idea> SearchIdeas(string searchTerm, int? categoryId);
    }
}
