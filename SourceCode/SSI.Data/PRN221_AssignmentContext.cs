using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using SSI.Share.Domain;

namespace SSI.Share.Data
{
    public partial class PRN221_AssignmentContext : DbContext
    {
        public PRN221_AssignmentContext()
        {
        }

        public PRN221_AssignmentContext(DbContextOptions<PRN221_AssignmentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bill> Bills { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Idea> Ideas { get; set; } = null!;
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<Like> Likes { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Vote> Votes { get; set; } = null!;
        public virtual DbSet<Wishlist> Wishlists { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bill>(entity =>
            {
                entity.ToTable("Bill");

                entity.Property(e => e.BillId).HasColumnName("billId");

                entity.Property(e => e.BillAmount).HasColumnName("billAmount");

                entity.Property(e => e.BillDate)
                    .HasColumnType("datetime")
                    .HasColumnName("billDate");

                entity.Property(e => e.IdeaId).HasColumnName("ideaId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Idea)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.IdeaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bill_Ideas");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bill_User");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CateId);

                entity.ToTable("Category");

                entity.Property(e => e.CateId).HasColumnName("cateId");

                entity.Property(e => e.Category1)
                    .HasMaxLength(50)
                    .HasColumnName("category");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.CommentId).HasColumnName("commentId");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.IdeaId).HasColumnName("ideaId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Idea)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.IdeaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_Ideas");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_User");
            });

            modelBuilder.Entity<Idea>(entity =>
            {
                entity.Property(e => e.IdeaId).HasColumnName("ideaId");

                entity.Property(e => e.CateId).HasColumnName("cateId");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.IdeaDate)
                    .HasColumnType("datetime")
                    .HasColumnName("ideaDate");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.StatusId).HasColumnName("statusId");

                entity.Property(e => e.Summary)
                    .HasMaxLength(250)
                    .HasColumnName("summary");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Cate)
                    .WithMany(p => p.Ideas)
                    .HasForeignKey(d => d.CateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ideas_Category");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Ideas)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ideas_Status");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Ideas)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ideas_User");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("Image");

                entity.Property(e => e.ImageId).HasColumnName("imageId");

                entity.Property(e => e.IdeaId).HasColumnName("ideaId");

                entity.Property(e => e.Image1).HasColumnName("image");

                entity.HasOne(d => d.Idea)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.IdeaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Image_Ideas");
            });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.ToTable("Like");

                entity.Property(e => e.LikeId).HasColumnName("likeId");

                entity.Property(e => e.IdeaId).HasColumnName("ideaId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Idea)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.IdeaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Like_Ideas");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Like_User");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(20)
                    .HasColumnName("roleName");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.StatusId).HasColumnName("statusId");

                entity.Property(e => e.Status1)
                    .HasMaxLength(50)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .HasColumnName("address");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Phonenumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("phonenumber")
                    .IsFixedLength();

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasColumnName("userName");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Role");
            });

            modelBuilder.Entity<Vote>(entity =>
            {
                entity.ToTable("Vote");

                entity.Property(e => e.VoteId).HasColumnName("voteId");

                entity.Property(e => e.IdeaId).HasColumnName("ideaId");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Idea)
                    .WithMany(p => p.Votes)
                    .HasForeignKey(d => d.IdeaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vote_Ideas");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Votes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vote_User");
            });

            modelBuilder.Entity<Wishlist>(entity =>
            {
                entity.ToTable("Wishlist");

                entity.Property(e => e.WishlistId).HasColumnName("wishlistId");

                entity.Property(e => e.IdeaId).HasColumnName("ideaId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Idea)
                    .WithMany(p => p.Wishlists)
                    .HasForeignKey(d => d.IdeaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Wishlist_Ideas");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Wishlists)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Wishlist_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
