using Microsoft.EntityFrameworkCore;
using SSI.Data.IRepository;
using SSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.Data.Repository
{
    public class IdeaRepository : IIdeaRepository
    {
        private readonly SSIV2Context _context;
        public IdeaRepository(SSIV2Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Idea>> GetIdeasByUserIdAndRoleAsync(int userId, string role, int pageNumber, int pageSize)
        {
            return await _context.Ideas
                .Include(i => i.User)
                .Include(i => i.Category)
                .Where(i => i.UserId == userId && i.User.Role == role)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalIdeasCountByUserIdAndRoleAsync(int userId, string role)
        {
            return await _context.Ideas
                .Include(i => i.User)
                .CountAsync(i => i.UserId == userId && i.User.Role == role);
        }

        public async Task<Idea?> GetIdeaWithDetailsAsync(int ideaId)
        {
            return await _context.Ideas
                .Include(i => i.Ideadetails)
                    .ThenInclude(d => d.Images)
                .Include(i => i.Ideadetails)
                    .ThenInclude(d => d.Comments)
                        .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(i => i.IdeaId == ideaId);
        }
        public async Task<Ideadetail?> GetMilestoneDetailByIdAsync(int ideaDetailId)
        {
            return await _context.Ideadetails
                .Include(d => d.Images)
                .Include(d => d.Comments)
                    .ThenInclude(c => c.User)
                    .OrderByDescending(d => d.CreatedAt)
                .FirstOrDefaultAsync(d => d.IdeaDetailId == ideaDetailId);
        }
        public List<Idea> SearchIdeas(string searchTerm, int? categoryId)
        {
            var query = _context.Ideas
                .Include(i => i.Category)
                .Include(i => i.Ideadetails)
                .ThenInclude(d => d.Images)
                .Where(i => i.Status.ToLower() == "approved")
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(i => i.Title.Contains(searchTerm));
            }

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(i => i.CategoryId == categoryId.Value);
            }

            return query.ToList();
        }

        public (Idea, int, List<Comment>) GetIdeaById(int id)
        {
            var idea = _context.Ideas
                .Include(i => i.Category)
                .Include(i => i.User)
                .FirstOrDefault(i => i.IdeaId == id);

            if (idea == null)
            {
                return (null, 0, new List<Comment>());
            }

            _context.Entry(idea)
                .Collection(i => i.Ideadetails)
                .Query()
                .Include(d => d.Images)
                .Include(d => d.Comments)
                    .ThenInclude(c => c.User)
                .Load();

            int commentCount = idea.Ideadetails.SelectMany(d => d.Comments).Count();
            var comments = idea.Ideadetails.SelectMany(d => d.Comments).ToList();

            return (idea, commentCount, comments);
        }

        public (Ideadetail, List<Comment>) GetMilestoneDetailsById(int milestoneId)
        {
            var detail = _context.Ideadetails
                .Include(d => d.Images)
                .Include(d => d.Comments)
                    .ThenInclude(c => c.User)
                .FirstOrDefault(d => d.IdeaDetailId == milestoneId);

            if (detail == null)
            {
                return (null, new List<Comment>());
            }

            var comments = detail.Comments.ToList();
            return (detail, comments);
        }
        public void AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public List<Idea> RelatedIdea(int ideaId)
        {
            var idea = _context.Ideas.Where(i => i.IdeaId == ideaId).FirstOrDefault();
            var rerelatedIdeas = _context.Ideas.Where(i => i.CategoryId == idea.CategoryId).Where(i => i.IdeaId != idea.IdeaId).Where(i => i.Status.ToLower() == "approved").ToList();
            return rerelatedIdeas.ToList();
        }

        public void AddIdeasToInterestList(IdeaInterest ideaInterest)
        {
            _context.IdeaInterests.Add(ideaInterest);
            _context.SaveChanges();
        }

        public bool IsIdeaInInterestList(int ideaId, int userId)
        {
            return _context.IdeaInterests.Any(i => i.IdeaId == ideaId && i.UserId == userId);
        }

        public List<IdeaInterest> GetInterestList(int userId)
        {
            var interestList = _context.IdeaInterests.Include(i => i.Idea).ThenInclude(idea => idea.Category).Where(i => i.UserId == userId).ToList();
            return interestList;
        }

        public void DeleteInterest(int interestId)
        {
            var interest = _context.IdeaInterests.Find(interestId);
            if (interest != null)
            {
                _context.IdeaInterests.Remove(interest);
                _context.SaveChanges();
            }
        }

        public Dictionary<string, int> countNumber()
        {
            int numOfUser = _context.Users.Count();
            int numOfInvestor = _context.Users.Where(u => u.Role.ToLower() == "investor").Count();
            int numOfStartup = _context.Users.Where(u => u.Role.ToLower() == "startup").Count();
            int numOfIdeas = _context.Ideas.Count();
            int numOfIdeaIsSeeking = _context.Ideas.Where(i => i.IsSeekingInvestment == true).Count();
            int numOfIdeaImplement = _context.Ideas.Where(i => i.IsImplement == true).Count();
            return new Dictionary<string, int>
                {
                    { "numOfUser", numOfUser },
                    { "numOfInvestor", numOfInvestor },
                    { "numOfStartup", numOfStartup },
                    { "numOfIdeas", numOfIdeas },
                    { "numOfIdeaIsSeeking", numOfIdeaIsSeeking },
                    {"numOfIdeaImplement", numOfIdeaImplement }

                };
        }

        public List<User> ProminentInvestor()
        {
            var prominentInvestorIds = _context.InvestmentRequests
                                                .GroupBy(ir => ir.UserId)
                                                .OrderByDescending(g => g.Count())
                                                .Take(4)
                                                .Select(g => g.Key)
                                                .ToList();


            var prominentInvestors = _context.Users
                .Where(u => prominentInvestorIds.Contains(u.UserId))
                .ToList();
            return prominentInvestors;
        }

        public List<Idea> GetNewIdea()
        {
            var newIdea = _context.Ideas.Include(i => i.Category).Include(i => i.User).OrderByDescending(i => i.CreatedAt).Take(3).ToList();
            return newIdea;
        }

        public async Task CreateIdeaAsync(Idea idea)
        {
            _context.Ideas.Add(idea);
            await _context.SaveChangesAsync();
        }

        public async Task CreateIdeaDetailAsync(Ideadetail ideaDetail)
        {
            _context.Ideadetails.Add(ideaDetail);
            await _context.SaveChangesAsync();
        }

        public async Task CreateImageAsync(Image image)
        {
            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();
        }

        public async Task AddCommentAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }
        public async Task<Comment?> GetCommentByIdAsync(int commentId)
        {
            return await _context.Comments
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.CommentId == commentId);
        }
    }
}
