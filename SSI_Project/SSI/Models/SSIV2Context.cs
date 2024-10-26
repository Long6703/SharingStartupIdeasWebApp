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

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.HasIndex(e => e.Name, "UQ__category__72E12F1B1FBC5A56")
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
                    .HasConstraintName("FK__comment__idea_de__74AE54BC");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__comment__parent___76969D2E");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__comment__user_id__75A278F5");
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
                    .HasConstraintName("FK__idea__category_i__6A30C649");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Ideas)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__idea__user_id__693CA210");
            });

            modelBuilder.Entity<IdeaInterest>(entity =>
            {
                entity.HasKey(e => e.InterestId)
                    .HasName("PK__idea_int__0F5A1FAD7BF984DC");

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
                    .HasConstraintName("FK__idea_inte__idea___02FC7413");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.IdeaInterests)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__idea_inte__user___02084FDA");
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
                    .HasConstraintName("FK__ideadetai__idea___6E01572D");
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
                    .HasConstraintName("FK__image__idea_deta__70DDC3D8");
            });

            modelBuilder.Entity<InvestmentRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId)
                    .HasName("PK__investme__18D3B90F5CA0E31F");

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
                    .HasConstraintName("FK__investmen__idea___7D439ABD");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.InvestmentRequests)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__investmen__user___7E37BEF6");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("transaction");

                entity.HasIndex(e => e.TransactionCode, "UQ__transact__DD5740BED09C9587")
                    .IsUnique();

                entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("amount");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdeaId).HasColumnName("idea_id");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasColumnName("status")
                    .HasDefaultValueSql("('pending')");

                entity.Property(e => e.TransactionCode)
                    .HasMaxLength(50)
                    .HasColumnName("transaction_code");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Idea)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.IdeaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__transacti__idea___0A9D95DB");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__transacti__user___09A971A2");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Email, "UQ__user__AB6E616410B41849")
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
