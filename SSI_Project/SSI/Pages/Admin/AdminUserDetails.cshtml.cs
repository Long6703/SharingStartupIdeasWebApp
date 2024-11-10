using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSI.Models;
using SSI.Services.IService;
using System.Text;

namespace SSI.Pages.Admin
{
    [Authorize(Roles = "admin")]
    public class AdminUserDetailsModel : PageModel
    {
        private readonly IAdminService adminService;
        private readonly IAdminIdeasService adminIdeasService;
        public AdminUserDetailsModel(IAdminService adminService, IAdminIdeasService adminIdeas)
        {
            this.adminService = adminService;
            this.adminIdeasService = adminIdeas;
        }

        public ICollection<Idea> Ideas { get; set; }
        public Models.User User { get; set; } = new Models.User();
        public ICollection<Ideadetail> ideadetails { get; set; }

        public Models.Transaction Transaction { get; set; }
        public ICollection<InvestmentRequest> investments { get; set; }
        public ICollection<Image> Images { get; set; }
        public int NoOfSuccessInvest {  get; set; }
        public int SumAmountInvest {  get; set; }
        public int NoOfRejectInvest { get; set; }
        public List<string> Paras { get; set; } = new List<string>();
        public void OnGet(int id)
        {
            User = adminService.GetUser(id);
            Paras = GetPara(User.Bio);
            if (User.Role.Equals("investor"))
            {
                Ideas = adminIdeasService.GetIdeasByInvestore(id);
                foreach(var i in Ideas)
                {
                    investments = adminIdeasService.GetInvestByIdeaId(i.IdeaId,id);
                    i.InvestmentRequests = investments;
                    foreach(var t in investments)
                    {
                        var trans = adminIdeasService.GetTransactionByReqId(t.RequestId, i.IdeaId, User.UserId);
                        if(trans != null)
                        {
                            Transaction = trans;
                        }
                    }
                }
            }
            else
            {
                Ideas = adminIdeasService.GetIdeasByFounder(id);
                foreach (var i in Ideas)
                {
                    investments = adminIdeasService.GetInvestByIdeaId(i.IdeaId, id);
                    i.InvestmentRequests = investments;
                }
            }
            
            User.Ideas = Ideas;
            NoOfSuccessInvest = adminService.CountNoSuccesInvest(id);
            NoOfRejectInvest = adminService.CountNoRejecrInvest(id);
            SumAmountInvest = (int)adminService.SumAmountInvest(id);
        }

        public IActionResult OnPost(int id, string action)
        {
            User = adminService.GetUser(id);
            if (action.Equals("UnLock"))
            {
                adminService.UnlockAccount(id);
            }
            else
            {
                adminService.LockAccount(id);
            }
            return RedirectToPage(new {id = id});
        }

        private List<string> GetPara(string text)
        {
            List<string> para = new List<string>();
            string[] words = text.Split(' ');
            StringBuilder currentParagraph = new StringBuilder();
            int wordCount = 0;
            foreach (var word in words)
            {
                currentParagraph.Append(word).Append(" ");
                wordCount++;
                if (wordCount >= 50 && word.EndsWith("."))
                {
                    para.Add(currentParagraph.ToString().Trim());
                    currentParagraph.Clear();
                    wordCount = 0;
                }
            }
            if (currentParagraph.Length > 0)
            {
                para.Add(currentParagraph.ToString().Trim());
            }
            return para;
        }
    }
}
