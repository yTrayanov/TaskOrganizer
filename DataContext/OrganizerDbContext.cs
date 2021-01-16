namespace DataContext
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using DbModels;
    public class OrganizerDbContext : IdentityDbContext<User>
    {
        public OrganizerDbContext(DbContextOptions<OrganizerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasMany(u => u.Messages)
                .WithOne(m => m.Sender)
                .HasForeignKey(m => m.SenderId);

            builder.Entity<User>()
                .HasMany(u => u.Groups)
                .WithOne(ug => ug.User)
                .HasForeignKey(ug => ug.UserId);

            builder.Entity<User>()
                .HasMany(u => u.Tasks)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);


            builder.Entity<Group>()
                .HasMany(g => g.Messages)
                .WithOne(m => m.Group)
                .HasForeignKey(m => m.GroupId);

            builder.Entity<Group>()
                .HasMany(g => g.Tasks)
                .WithOne(t => t.Group)
                .HasForeignKey(t => t.GroupId);


            builder.Entity<Group>()
                .HasMany(g => g.Users)
                .WithOne(ug => ug.Group)
                .HasForeignKey(ug => ug.GroupId);

            builder.Entity<UserGroup>()
                .HasKey(ug => new { ug.GroupId , ug.UserId });


            base.OnModelCreating(builder);
        }
    }
}
