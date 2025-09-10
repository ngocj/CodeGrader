using Domain.Entities;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class CMContext : DbContext
    {
        public CMContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public DbSet<Comment> Comment { get; set; }                    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CommentConfiguration());

            // seed data
            modelBuilder.Entity<Comment>().HasData(
                new Comment
                {
                    Id = 1,
                    UserId = 1,
                    ProblemId = 1,
                    CommentText = "This is a sample comment.",
                    Like = 10,
                    ParentCommentId = null,
                    CreatedAt = DateTime.UtcNow
                },
                new Comment
                {
                    Id = 2,
                    UserId = 2,
                    ProblemId = 1,
                    CommentText = "This is a reply to the sample comment.",
                    Like = 5,
                    ParentCommentId = 1,
                    CreatedAt = DateTime.UtcNow
                },
                new Comment
                {
                    Id = 3,
                    UserId = 2,
                    ProblemId = 1,
                    CommentText = "Another top-level comment.",
                    Like = 2,
                    ParentCommentId = null,
                    CreatedAt = DateTime.UtcNow
                }
                );


        }
    }
}
