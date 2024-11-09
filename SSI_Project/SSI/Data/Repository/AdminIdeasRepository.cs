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
                i.IsSeekingInvestment = true;
                i.IsImplement = false;
                _context.Ideas.Update(i);
            }
            _context.SaveChanges();
        }

        

    

       

        public IEnumerable<Idea> GetIdeas()
        {
            return _context.Ideas
                .OrderByDescending(i => i.Status == "pending" ? 1 : 0) 
                .ToList();
        }

        public ICollection<Idea> GetIdeasByFounder(int userId)
        {
            return _context.Ideas
                    .Where(i => i.UserId == userId)
                    .Include(i => i.User)
                    .Include(iv => iv.InvestmentRequests)
                    .ToList();
        }

        public ICollection<Idea> GetIdeasByInvestore(int userId)
        {
           var list = (from i in _context.Ideas
                   join iv in _context.InvestmentRequests on i.IdeaId equals iv.IdeaId
                   join t in _context.Transactions on iv.RequestId equals t.InvestmentRequestId
                    where iv.UserId == userId
                    select i).ToList();
            return list;
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
                i.IsSeekingInvestment = false;
                i.IsImplement = false;
                _context.Ideas.Update(i);
            }
            _context.SaveChanges();
        }



        //ideadetail
        public Models.Idea GetIdeaById(int id)
        {
            return _context.Ideas.FirstOrDefault(i => i.IdeaId == id);
        }
        public ICollection<Ideadetail> GetIdeadetailsByIdeaId(int ideaId)
        {
            var list = (from i in _context.Ideas
                       join id in _context.Ideadetails on i.IdeaId equals id.IdeaId
                       where i.IdeaId == ideaId
                       select id).ToList();
            return list;
        }

        public ICollection<Image> GetImages(int ideaDDetailsId)
        {
            var listImgae = (from id in _context.Ideadetails
                     join i in _context.Images on id.IdeaDetailId equals i.ImageId
                     where id.IdeaDetailId == ideaDDetailsId
                     select i).ToList();
            return listImgae;
        }

        public int CountIdeaDetailByIdeaId(int ideId)
        {
            int count = (from i in _context.Ideas
                         join id in _context.Ideadetails on i.IdeaId equals id.IdeaId
                         where i.IdeaId == ideId
                         select id).Count();
            return count;
        }

        public Models.Category GetCategoryById(int id)
        {
            var c = from category in _context.Categories
                    join i in _context.Ideas on category.CategoryId equals i.CategoryId
                    where i.IdeaId == id
                   select category;
            return c as Models.Category;
        }
    }
}
