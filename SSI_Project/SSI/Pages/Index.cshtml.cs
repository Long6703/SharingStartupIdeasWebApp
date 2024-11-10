using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Models;
using SSI.Services.IService;
namespace SSI.Web.Client.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IIdeaService _ideaService;
        public Dictionary<string, int> CountData { get; private set; }
        public List<User> TopInvestors { get; private set; }
        public List<Idea> NewIdeas { get; private set; }
        public IndexModel(IIdeaService ideaService)
        {
            _ideaService = ideaService;
        }
        public void OnGet()
        {
            CountData = _ideaService.countNumber() ?? new Dictionary<string, int>
            {
                { "numOfUser", 0 },
                { "numOfInvestor", 0 },
                { "numOfStartup", 0 },
                { "numOfIdeas", 0 },
                { "numOfIdeaIsSeeking", 0 },
                { "numOfIdeaImplement", 0 }
            };
            TopInvestors = _ideaService.ProminentInvestor() ?? new List<User>();
            NewIdeas = _ideaService.GetNewIdea() ?? new List<Idea>();
        }
    }
}
