using Groupchat_Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Groupchat_Api.Data
{
    public class GroupchatDbContext : DbContext
    {
        public GroupchatDbContext(DbContextOptions<GroupchatDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;
        public DbSet<GroupUser> GroupUsers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.HasIndex(u => u.UserName).IsUnique();
                entity.Property(u => u.UserName).HasMaxLength(50).IsRequired();
                entity.Property(u => u.PasswordHash).IsRequired();
                entity.Property(u => u.Bio).HasMaxLength(250);
            });


            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(g => g.Id);
                entity.Property(g => g.GroupName).HasMaxLength(50).IsRequired();
                entity.Property(g => g.InviteCode).HasMaxLength(32).IsRequired();
                entity.Property(g => g.Bio).HasMaxLength(250);
                entity.Property(g => g.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });


            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Content).HasMaxLength(1000).IsRequired();
                entity.Property(m => m.SentAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(m => m.User)
                    .WithMany(u => u.Messages)
                    .HasForeignKey(m => m.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(m => m.Group)
                    .WithMany(g => g.Messages)
                    .HasForeignKey(m => m.GroupId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(m => m.MessageCode)
                    .IsUnique();
            });


            modelBuilder.Entity<GroupUser>(entity =>
            {
                entity.HasKey(gu => new { gu.UserId, gu.GroupId });

                entity.HasOne(gu => gu.User)
                    .WithMany(u => u.GroupUsers)
                    .HasForeignKey(gu => gu.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(gu => gu.Group)
                    .WithMany(g => g.GroupUsers)
                    .HasForeignKey(gu => gu.GroupId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}