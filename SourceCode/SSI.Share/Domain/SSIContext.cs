using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SSI.Share.Domain
{
    public partial class SSIContext : DbContext
    {
        public SSIContext()
        {
        }

        public SSIContext(DbContextOptions<SSIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<ChatMessage> ChatMessages { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Idea> Ideas { get; set; } = null!;
        public virtual DbSet<IdeaInterest> IdeaInterests { get; set; } = null!;
        public virtual DbSet<MediaDescription> MediaDescriptions { get; set; } = null!;
        public virtual DbSet<Milestone> Milestones { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<ProjectUpdate> ProjectUpdates { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Subcategory> Subcategories { get; set; } = null!;
        public virtual DbSet<SystemConfiguration> SystemConfigurations { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server =(local); database = SSI;uid=sa;pwd=123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(e => e.Name, "UQ__Categori__72E12F1B40F328BB")
                    .IsUnique();

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .HasColumnName("status")
                    .HasDefaultValueSql("('active')");
            });

            modelBuilder.Entity<ChatMessage>(entity =>
            {
                entity.HasKey(e => e.MessageId)
                    .HasName("PK__Chat_Mes__0BBF6EE623021C84");

                entity.ToTable("Chat_Messages");

                entity.Property(e => e.MessageId).HasColumnName("message_id");

                entity.Property(e => e.Message).HasColumnName("message");

                entity.Property(e => e.ReceiverId).HasColumnName("receiver_id");

                entity.Property(e => e.SenderId).HasColumnName("sender_id");

                entity.Property(e => e.SentAt)
                    .HasColumnType("datetime")
                    .HasColumnName("sent_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .HasColumnName("status")
                    .HasDefaultValueSql("('active')");

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.ChatMessageReceivers)
                    .HasForeignKey(d => d.ReceiverId)
                    .HasConstraintName("FK__Chat_Mess__recei__06CD04F7");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.ChatMessageSenders)
                    .HasForeignKey(d => d.SenderId)
                    .HasConstraintName("FK__Chat_Mess__sende__05D8E0BE");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.CommentId).HasColumnName("comment_id");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdeaId).HasColumnName("idea_id");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .HasColumnName("status")
                    .HasDefaultValueSql("('active')");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Idea)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.IdeaId)
                    .HasConstraintName("FK__Comments__idea_i__66603565");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Comments__user_i__656C112C");
            });

            modelBuilder.Entity<Idea>(entity =>
            {
                entity.Property(e => e.IdeaId).HasColumnName("idea_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .HasColumnName("status")
                    .HasDefaultValueSql("('pending')");

                entity.Property(e => e.SubcategoryId).HasColumnName("subcategory_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Ideas)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Ideas__category___5EBF139D");

                entity.HasOne(d => d.Subcategory)
                    .WithMany(p => p.Ideas)
                    .HasForeignKey(d => d.SubcategoryId)
                    .HasConstraintName("FK__Ideas__subcatego__5FB337D6");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Ideas)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Ideas__user_id__5DCAEF64");
            });

            modelBuilder.Entity<IdeaInterest>(entity =>
            {
                entity.HasKey(e => e.InterestId)
                    .HasName("PK__Idea_Int__0F5A1FADF07B7D98");

                entity.ToTable("Idea_Interests");

                entity.HasIndex(e => new { e.UserId, e.IdeaId }, "UQ__Idea_Int__2515F24E61431F76")
                    .IsUnique();

                entity.Property(e => e.InterestId).HasColumnName("interest_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdeaId).HasColumnName("idea_id");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .HasColumnName("status")
                    .HasDefaultValueSql("('active')");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Idea)
                    .WithMany(p => p.IdeaInterests)
                    .HasForeignKey(d => d.IdeaId)
                    .HasConstraintName("FK__Idea_Inte__idea___74AE54BC");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.IdeaInterests)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Idea_Inte__user___73BA3083");
            });

            modelBuilder.Entity<MediaDescription>(entity =>
            {
                entity.HasKey(e => e.MediaId)
                    .HasName("PK__Media_De__D0A840F458241D58");

                entity.ToTable("Media_Descriptions");

                entity.Property(e => e.MediaId).HasColumnName("media_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdeaId).HasColumnName("idea_id");

                entity.Property(e => e.MediaType)
                    .HasMaxLength(10)
                    .HasColumnName("media_type");

                entity.Property(e => e.MediaUrl)
                    .HasMaxLength(255)
                    .HasColumnName("media_url");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .HasColumnName("status")
                    .HasDefaultValueSql("('active')");

                entity.HasOne(d => d.Idea)
                    .WithMany(p => p.MediaDescriptions)
                    .HasForeignKey(d => d.IdeaId)
                    .HasConstraintName("FK__Media_Des__idea___6D0D32F4");
            });

            modelBuilder.Entity<Milestone>(entity =>
            {
                entity.Property(e => e.MilestoneId).HasColumnName("milestone_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.DueDate)
                    .HasColumnType("datetime")
                    .HasColumnName("due_date");

                entity.Property(e => e.IdeaId).HasColumnName("idea_id");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status")
                    .HasDefaultValueSql("('pending')");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Idea)
                    .WithMany(p => p.Milestones)
                    .HasForeignKey(d => d.IdeaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Milestones_Idea");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.NotificationId).HasColumnName("notification_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsRead)
                    .HasColumnName("is_read")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Message).HasColumnName("message");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .HasColumnName("status")
                    .HasDefaultValueSql("('active')");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Notificat__user___00200768");
            });

            modelBuilder.Entity<ProjectUpdate>(entity =>
            {
                entity.HasKey(e => e.UpdateId)
                    .HasName("PK__Project___68D2E079924219F4");

                entity.ToTable("Project_Updates");

                entity.Property(e => e.UpdateId).HasColumnName("update_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.IdeaId).HasColumnName("idea_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("update_date")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Idea)
                    .WithMany(p => p.ProjectUpdates)
                    .HasForeignKey(d => d.IdeaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectUpdates_Idea");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.Property(e => e.ReportId).HasColumnName("report_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Details).HasColumnName("details");

                entity.Property(e => e.ReportType)
                    .HasMaxLength(20)
                    .HasColumnName("report_type");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .HasColumnName("status")
                    .HasDefaultValueSql("('active')");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("roleID");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .HasColumnName("roleName");
            });

            modelBuilder.Entity<Subcategory>(entity =>
            {
                entity.Property(e => e.SubcategoryId).HasColumnName("subcategory_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .HasColumnName("status")
                    .HasDefaultValueSql("('active')");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Subcategories)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Subcatego__categ__571DF1D5");
            });

            modelBuilder.Entity<SystemConfiguration>(entity =>
            {
                entity.HasKey(e => e.ConfigId)
                    .HasName("PK__System_C__4AD1BFF13A7FBE89");

                entity.ToTable("System_Configurations");

                entity.HasIndex(e => e.ConfigKey, "UQ__System_C__BDF6033D889E1B21")
                    .IsUnique();

                entity.Property(e => e.ConfigId).HasColumnName("config_id");

                entity.Property(e => e.ConfigKey)
                    .HasMaxLength(50)
                    .HasColumnName("config_key");

                entity.Property(e => e.ConfigValue).HasColumnName("config_value");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("amount");

                entity.Property(e => e.PaymentMethod)
                    .HasMaxLength(50)
                    .HasColumnName("payment_method");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .HasColumnName("status");

                entity.Property(e => e.TransactionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("transaction_date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Transacti__user___797309D9");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email, "UQ__Users__AB6E6164CC28CF2A")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "UQ__Users__F3DBC5724EF8B44D")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Bio).HasColumnName("bio");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(255)
                    .HasColumnName("password_hash");

                entity.Property(e => e.ProfileImageUrl)
                    .HasMaxLength(255)
                    .HasColumnName("profile_image_url");

                entity.Property(e => e.RoleId).HasColumnName("roleID");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .HasColumnName("status")
                    .HasDefaultValueSql("('active')");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_User_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
