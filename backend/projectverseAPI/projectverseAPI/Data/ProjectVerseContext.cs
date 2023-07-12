using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using projectverseAPI.Models;

namespace projectverseAPI.Data
{
    public class ProjectVerseContext : IdentityDbContext<User>
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ProfileComponent> ProfileComponents { get; set; }
        public DbSet<ComponentTheme> ComponentThemes { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<Collaboration> Collaborations { get; set; }
        public DbSet<Technology> Technologies { get; set; }


        public ProjectVerseContext(DbContextOptions<ProjectVerseContext> options) : base(options)
        {
            DbSeeder.Seed(this);
        }

    }
}
