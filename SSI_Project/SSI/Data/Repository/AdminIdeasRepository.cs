using Microsoft.EntityFrameworkCore;
using SSI.Data.IRepository;
using SSI.Models;

namespace SSI.Data.Repository
{
    public class AdminIdeasRepository : IAdminIdeasRepository
    {
        private readonly SSIV2Context _context;
        public AdminIdeasRepository(SSIV2Context context) 
        {
            _context = context;
        }

        public void ApproveIdeas(int id)
        {
            var i = _context.Ideas.FirstOrDefault(i => i.IdeaId == id);
            if (i != null) 
            {
                i.Status = "approved";
                _context.Ideas.Update(i);
            }
            _context.SaveChanges();
        }

        public Models.Idea GetIdeaById(int id)
        {
            return _context.Ideas.FirstOrDefault(i =>i.IdeaId == id);
        }

        public ICollection<Ideadetail> GetIdeaDetailByIdeaId(int id)
        {
            return _context.Ideadetails.Where(i => i.IdeaId == id).ToList();
        }

        public IEnumerable<Idea> GetIdeas()
        {
            return _context.Ideas.ToList();
        }

        public ICollection<Idea> GetIdeasByUser(int userId)
        {
            return _context.Ideas
                    .Where(i => i.UserId == userId)
                    .Include(i => i.User)
                    .ToList();
        }

        public ICollection<Image> GetImagesByIdDetail(int id)
        {
            return _context.Images.Where(i => i.IdeaDetailId == id).ToList();
        }

        public InvestmentRequest GetInvestmentById(int id)
        {
            return _context.InvestmentRequests.FirstOrDefault(i => i.RequestId == id);
        }

        public void RejectIdeas(int id)
        {
            var i = _context.Ideas.FirstOrDefault(i => i.IdeaId == id);
            if(i != null)
            {
                i.Status = "rejected";
                _context.Ideas.Update(i);
            }
            _context.SaveChanges();
        }


    }
}
