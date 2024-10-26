using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SSI.Models
{
    public partial class SSIV2Context : DbContext
    {
        public SSIV2Context()
        {
        }

        public SSIV2Context(DbContextOptions<SSIV2Context> options)
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
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server =(local); database = SSIV2;uid=sa;pwd=mq808103;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.HasIndex(e => e.Name, "UQ__category__72E12F1B321D6217")
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
                    .HasConstraintName("FK__comment__idea_de__6383C8BA");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__comment__parent___656C112C");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__comment__user_id__6477ECF3");
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
                    .HasMaxLength(255)
                    .HasColumnName("poster_img");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasColumnName("status")
                    .HasDefaultValueSql("('pending')");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Ideas)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__idea__category_i__59063A47");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Ideas)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__idea__user_id__5812160E");
            });

            modelBuilder.Entity<IdeaInterest>(entity =>
            {
                entity.HasKey(e => e.InterestId)
                    .HasName("PK__idea_int__0F5A1FAD3A755CA5");

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
                    .HasConstraintName("FK__idea_inte__idea___71D1E811");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.IdeaInterests)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__idea_inte__user___70DDC3D8");
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
                    .HasConstraintName("FK__ideadetai__idea___5CD6CB2B");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("image");

                entity.Property(e => e.ImageId).HasColumnName("image_id");

                entity.Property(e => e.IdeaDetailId).HasColumnName("idea_detail_id");

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .HasColumnName("url");

                entity.HasOne(d => d.IdeaDetail)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.IdeaDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__image__idea_deta__5FB337D6");
            });

            modelBuilder.Entity<InvestmentRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId)
                    .HasName("PK__investme__18D3B90F4FAC354C");

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
                    .HasColumnName("status")
                    .HasDefaultValueSql("('pending')");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Idea)
                    .WithMany(p => p.InvestmentRequests)
                    .HasForeignKey(d => d.IdeaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__investmen__idea___6C190EBB");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.InvestmentRequests)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__investmen__user___6D0D32F4");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("transaction");

                entity.HasIndex(e => e.TransactionCode, "UQ__transact__DD5740BE5630E254")
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
                    .HasColumnName("status")
                    .HasDefaultValueSql("('pending')");

                entity.Property(e => e.TransactionCode)
                    .HasMaxLength(50)
                    .HasColumnName("transaction_code");

                entity.HasOne(d => d.InvestmentRequest)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.InvestmentRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__transacti__inves__787EE5A0");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Email, "UQ__user__AB6E6164D31555C0")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

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

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.Role)
                    .HasMaxLength(20)
                    .HasColumnName("role");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasColumnName("status")
                    .HasDefaultValueSql("('active')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
