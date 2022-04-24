using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAL.EF
{
    public class AppDbContext : IdentityDbContext<User, Role, string,
        IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatRoomUser> ChatRoomUsers{ get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>()
                .HasOne(x => x.User)
                .WithMany(x => x.Roles)
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            builder.Entity<UserRole>()
                .HasOne(x => x.Role)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.RoleId)
                .IsRequired();

            builder.Entity<ChatRoomUser>()
                .HasOne(x => x.User)
                .WithMany(x => x.ChatRooms)
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            builder.Entity<ChatRoomUser>()
                .HasOne(x => x.ChatRoom)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.ChatRoomId)
                .IsRequired();

            foreach (var x in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                x.DeleteBehavior = DeleteBehavior.ClientCascade;
        }
    }
}
