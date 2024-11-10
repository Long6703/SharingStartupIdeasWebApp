using Microsoft.AspNetCore.Mvc.Formatters;
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

            var lis = (from i in _context.Ideas
                       join u in _context.Users on i.UserId equals u.UserId
                       where u.UserId == userId
                       select i).ToList();
            return lis;
        }

        public ICollection<Idea> GetIdeasByInvestore(int userId)
        {
           var list = (from u in _context.Users
                       join i in _context.Ideas on u.UserId equals i.UserId
                       where u.UserId == userId
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
            var idea = (from i in _context.Ideas
                       where i.IdeaId == id
                       select i).FirstOrDefault();
            return idea;
        }
        public ICollection<Ideadetail> GetIdeadetailsByIdeaId(int ideaId)
        {
            var list = (from ideadetail in _context.Ideadetails
                        join i in _context.Ideas on ideadetail.IdeaId equals i.IdeaId
                        where ideadetail.IdeaId == ideaId
                        select ideadetail).ToList();
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
                   select category;
            return c as Models.Category;
        }

        public ICollection<InvestmentRequest> GetInvestByIdeaId(int ideaId, int userId)
        {
            var  l = (from i in _context.Ideas
                      join invest in _context.InvestmentRequests on i.IdeaId equals invest.IdeaId
                      where invest.IdeaId == ideaId && invest.UserId == userId
                      select invest).ToList();
            return l;
        }

        public Transaction GetTransactionByReqId(int reqId, int ideaId, int userId)
        {
            var trans = (from u in _context.Users
                     join i in _context.InvestmentRequests on u.UserId equals i.UserId
                     join t in _context.Transactions on i.RequestId equals t.InvestmentRequestId
                     where i.IdeaId == ideaId && t.InvestmentRequestId == reqId && i.UserId == userId && t.Status == "completed"
                     select t) as Transaction;
            return trans;
        }
    }
}
