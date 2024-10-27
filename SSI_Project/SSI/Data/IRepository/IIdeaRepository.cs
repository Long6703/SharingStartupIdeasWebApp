using SSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.Data.IRepository
{
    public interface IIdeaRepository
    {
        (Idea,int) GetIdeaById(int id);
        List<Idea> SearchIdeas(string searchTerm, int? categoryId);
    }
}
