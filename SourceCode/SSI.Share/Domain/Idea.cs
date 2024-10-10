using System;
using System.Collections.Generic;

namespace SSI.Share.Domain
{
    public partial class Idea
    {
        public Idea()
        {
            Comments = new HashSet<Comment>();
            IdeaInterests = new HashSet<IdeaInterest>();
            MediaDescriptions = new HashSet<MediaDescription>();
            Milestones = new HashSet<Milestone>();
            ProjectUpdates = new HashSet<ProjectUpdate>();
        }

        public int IdeaId { get; set; }
        public int? UserId { get; set; }
        public int? CategoryId { get; set; }
        public int? SubcategoryId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Status { get; set; } = null!;

        public virtual Category? Category { get; set; }
        public virtual Subcategory? Subcategory { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<IdeaInterest> IdeaInterests { get; set; }
        public virtual ICollection<MediaDescription> MediaDescriptions { get; set; }
        public virtual ICollection<Milestone> Milestones { get; set; }
        public virtual ICollection<ProjectUpdate> ProjectUpdates { get; set; }
    }
}
