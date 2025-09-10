using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.ValueEntities;
using Infrastructure.Config;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts
{
    public class ProgressContext : DbContext
    {
        public ProgressContext(DbContextOptions options) : base(options)
        {
        }

        protected ProgressContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new SubmissionConfig());
            modelBuilder.ApplyConfiguration(new ProblemStatsConfig());
            modelBuilder.ApplyConfiguration(new UserProgressConfig());

            modelBuilder.Entity<Submission>().HasData(
                new Submission()
                {
                    Id = 1,
                    UserId = 2,
                    ProblemId = 1,
                    Language = "c sharp",
                    Point = 9,
                    SubmisstionAt = DateTime.Now,
                }
            );
            modelBuilder.Entity<Submission>().OwnsOne(s => s.EvaluationCriteria).HasData(
                new
                {
                    SubmissionId = 1, // Foreign key tới Submission
                    Algorithm = "Algorithm is correct and efficient for the given task. No issues detected.",
                    CleanCode = "Code is readable and follows basic C++ conventions. Could benefit from comments for clarity."
                },
                new
                {
                    SubmissionId = 2, // Foreign key tới Submission
                    Algorithm = "Algorithm is correct and efficient for the given task. No issues detected.",
                    CleanCode = "Code is readable and follows basic C++ conventions. Could benefit from comments for clarity."
                }
             );

            modelBuilder.Entity<ProblemStats>().HasData(
                new ProblemStats()
                {
                    Id = 1,
                    TotalSubmisstion = 1,
                    AvgPoint = 9
                },
                new ProblemStats()
                {
                    Id = 2,
                    TotalSubmisstion = 1,
                    AvgPoint = 3
                }
            );

            modelBuilder.Entity<UserProgress>().HasData(
                new UserProgress()
                {
                    Id = 1,
                    EasySolved = 1,
                    MediumSolved = 0,
                    HardSolved = 0,
                    Rank = 900,
                    TotalSubmisstion = 1
                },
                new UserProgress()
                {
                    Id = 2,
                    EasySolved = 1,
                    MediumSolved = 0,
                    HardSolved = 0,
                    Rank = 1,
                    TotalSubmisstion = 1
                }              
            );
        }

        public DbSet<ProblemStats> ProblemStats { get; set; }
        public DbSet<Submission> Submission { get; set; }
        public DbSet<UserProgress> UserProgress { get; set; }
    }
}
