using SSI.Models;

namespace SSI.Ultils.ViewModel
{
    public class IdeaDetails
    {
        public string TitleIdea {  get; set; }
        public string DescriptionIdea { get; set; }
        public string PosterUrl {  get; set; }
        public string Category {  get; set; }
        public string Content {  get; set; }
        public DateTime creatAtIdea { get; set; }
        public DateTime creatAtMilestone { get; set; }
        public string investorName {  get; set; }
        public string founderName {  get; set; }

        public ICollection<Image> images { get; set; }


    }
}
