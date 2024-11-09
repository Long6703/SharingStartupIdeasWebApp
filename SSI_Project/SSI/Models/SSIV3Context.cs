using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SSI.Models
{
    public partial class SSIV3Context : DbContext
    {
        public SSIV3Context()
        {
        }

        public SSIV3Context(DbContextOptions<SSIV3Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Idea> Ideas { get; set; } = null!;
        public virtual DbSet<IdeaInterest> IdeaInterests { get; set; } = null!;
        public virtual DbSet<Ideadetail> Ideadetails { get; set; } = null!;
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<InvestmentRequest> InvestmentRequests { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.HasIndex(e => e.Name, "UQ__category__72E12F1B7A9DEDE3")
                    .IsUnique();

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("comment");

                entity.Property(e => e.CommentId).HasColumnName("comment_id");

                entity.Property(e => e.Content)
                    .HasColumnType("text")
                    .HasColumnName("content");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdeaDetailId).HasColumnName("idea_detail_id");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.IdeaDetail)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.IdeaDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__comment__idea_de__4CA06362");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__comment__parent___4E88ABD4");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__comment__user_id__4D94879B");
            });

            modelBuilder.Entity<Idea>(entity =>
            {
                entity.ToTable("idea");

                entity.Property(e => e.IdeaId).HasColumnName("idea_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.IsImplement)
                    .HasColumnName("is_implement")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsSeekingInvestment)
                    .HasColumnName("is_seeking_investment")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PosterImg)
                    .HasColumnType("text")
                    .HasColumnName("poster_img");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Ideas)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__idea__category_i__4222D4EF");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Ideas)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__idea__user_id__412EB0B6");
            });

            modelBuilder.Entity<IdeaInterest>(entity =>
            {
                entity.HasKey(e => e.InterestId)
                    .HasName("PK__idea_int__0F5A1FAD01E1BE36");

                entity.ToTable("idea_interest");

                entity.Property(e => e.InterestId).HasColumnName("interest_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdeaId).HasColumnName("idea_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Idea)
                    .WithMany(p => p.IdeaInterests)
                    .HasForeignKey(d => d.IdeaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__idea_inte__idea___59063A47");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.IdeaInterests)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__idea_inte__user___5812160E");
            });

            modelBuilder.Entity<Ideadetail>(entity =>
            {
                entity.ToTable("ideadetail");

                entity.Property(e => e.IdeaDetailId).HasColumnName("idea_detail_id");

                entity.Property(e => e.Content)
                    .HasColumnType("text")
                    .HasColumnName("content");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdeaId).HasColumnName("idea_id");

                entity.HasOne(d => d.Idea)
                    .WithMany(p => p.Ideadetails)
                    .HasForeignKey(d => d.IdeaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ideadetai__idea___45F365D3");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("image");

                entity.Property(e => e.ImageId).HasColumnName("image_id");

                entity.Property(e => e.IdeaDetailId).HasColumnName("idea_detail_id");

                entity.Property(e => e.Url)
                    .HasColumnType("text")
                    .HasColumnName("url");

                entity.HasOne(d => d.IdeaDetail)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.IdeaDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__image__idea_deta__48CFD27E");
            });

            modelBuilder.Entity<InvestmentRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId)
                    .HasName("PK__investme__18D3B90F40D05AC5");

                entity.ToTable("investment_request");

                entity.Property(e => e.RequestId).HasColumnName("request_id");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("amount");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.EquityPercentage)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("equity_percentage");

                entity.Property(e => e.IdeaId).HasColumnName("idea_id");

                entity.Property(e => e.InvestmentPeriod)
                    .HasMaxLength(50)
                    .HasColumnName("investment_period");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Idea)
                    .WithMany(p => p.InvestmentRequests)
                    .HasForeignKey(d => d.IdeaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__investmen__idea___534D60F1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.InvestmentRequests)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__investmen__user___5441852A");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("transaction");

                entity.HasIndex(e => e.TransactionCode, "UQ__transact__DD5740BEEC1A1F20")
                    .IsUnique();

                entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("amount");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InvestmentRequestId).HasColumnName("investment_request_id");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasColumnName("status");

                entity.Property(e => e.TransactionCode)
                    .HasMaxLength(50)
                    .HasColumnName("transaction_code");

                entity.HasOne(d => d.InvestmentRequest)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.InvestmentRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__transacti__inves__5DCAEF64");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Email, "UQ__user__AB6E6164B47A3FE5")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.AvatarUrl)
                    .HasColumnType("text")
                    .HasColumnName("avatar_url");

                entity.Property(e => e.BankAccountNumber)
                    .HasMaxLength(50)
                    .HasColumnName("bank_account_number");

                entity.Property(e => e.BankName)
                    .HasMaxLength(100)
                    .HasColumnName("bank_name");

                entity.Property(e => e.Bio)
                    .HasColumnType("text")
                    .HasColumnName("bio");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Displayname)
                    .HasMaxLength(50)
                    .HasColumnName("displayname");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.FacebookUrl)
                    .HasColumnType("text")
                    .HasColumnName("facebook_url");

                entity.Property(e => e.LinkedinUrl)
                    .HasColumnType("text")
                    .HasColumnName("linkedin_url");

                entity.Property(e => e.Location)
                    .HasMaxLength(100)
                    .HasColumnName("location");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.Profession)
                    .HasMaxLength(100)
                    .HasColumnName("profession");

                entity.Property(e => e.Role)
                    .HasMaxLength(20)
                    .HasColumnName("role");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasColumnName("status");

                entity.Property(e => e.TwitterUrl)
                    .HasColumnType("text")
                    .HasColumnName("twitter_url");

                entity.Property(e => e.WebsiteUrl)
                    .HasColumnType("text")
                    .HasColumnName("website_url");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
