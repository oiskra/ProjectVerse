﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using projectverseAPI.Models;

namespace projectverseAPI.Data
{
    public class ProjectVerseContext : IdentityDbContext<User>
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<UserProfileData> UserProfileData { get; set; }
        public DbSet<ProfileDesigner> ProfileDesigners { get; set; }
        public DbSet<ProfileComponent> ProfileComponents { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<Collaboration> Collaborations { get; set; }
        public DbSet<CollaborationPosition> CollaborationPositions { get; set; }
        public DbSet<CollaborationApplicant> CollaborationApplicants { get; set; }
        public DbSet<Technology> Technologies { get; set; }


        public ProjectVerseContext(DbContextOptions<ProjectVerseContext> options) : base(options)
        { DbSeeder.Seed(this); }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CollaborationApplicant>()
                .HasOne(e => e.AppliedCollaboration)
                .WithMany(e => e.CollaborationApplicants)
                .OnDelete(DeleteBehavior.ClientCascade);                
        }

    }
}
