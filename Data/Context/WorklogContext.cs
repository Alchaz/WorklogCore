using Entities;
using Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class WorklogContext : DbContext
    {
        public WorklogContext(DbContextOptions options) : base(options)
        {


         }

        public DbSet<User> Users { get; set; }

        public DbSet<Worklog> Worklog { get; set; }

        public DbSet<T> GetDbSet<T>() where T : class => Set<T>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "user1", DailyMinHours = 3, DailyMaxHours = 5, Role = UserRoleEnum.User.ToString() },
                 new User { Id = 2, Name = "user2", DailyMinHours = 7, DailyMaxHours = 9, Role = UserRoleEnum.User.ToString() },
                 new User { Id = 3, Name = "admin", DailyMinHours = 0, DailyMaxHours = 15, Role = UserRoleEnum.Admin.ToString() }

            );
                modelBuilder.Entity<Worklog>().HasData(
         new Worklog { Id = 1, Description = "Scheduled to work on backend APIs", date = DateOnly.FromDateTime(DateTime.Today.AddDays(-1)), WorkedHours = 6, UserId = 1 },
         new Worklog { Id = 2, Description = "Planned bug fixing in reports module", date = DateOnly.FromDateTime(DateTime.Today.AddDays(-1)), WorkedHours = 5, UserId = 1 },
         new Worklog { Id = 3, Description = "Documentation update planned", date = DateOnly.FromDateTime(DateTime.Today.AddDays(-2)), WorkedHours = 4, UserId = 1 },
         new Worklog { Id = 4, Description = "Frontend layout tasks scheduled", date = DateOnly.FromDateTime(DateTime.Today.AddDays(-2)), WorkedHours = 6, UserId = 2 },
         new Worklog { Id = 5, Description = "Integration test planning", date = DateOnly.FromDateTime(DateTime.Today.AddDays(-2)), WorkedHours = 3, UserId = 2 },
         new Worklog { Id = 6, Description = "Team meeting setup", date = DateOnly.FromDateTime(DateTime.Today.AddDays(-3)), WorkedHours = 2, UserId = 2 },
         new Worklog { Id = 7, Description = "Research on new authentication method", date = DateOnly.FromDateTime(DateTime.Today.AddDays(-3)), WorkedHours = 5, UserId = 3 },
         new Worklog { Id = 8, Description = "UX review for the dashboard module", date = DateOnly.FromDateTime(DateTime.Today.AddDays(-4)), WorkedHours = 6, UserId = 3 },
         new Worklog { Id = 9, Description = "Client feedback analysis", date = DateOnly.FromDateTime(DateTime.Today.AddDays(-4)), WorkedHours = 4, UserId = 1 },
         new Worklog { Id = 10, Description = "Unit testing setup for core module", date = DateOnly.FromDateTime(DateTime.Today.AddDays(-5)), WorkedHours = 7, UserId = 1 },
         new Worklog { Id = 11, Description = "CI/CD configuration tasks", date = DateOnly.FromDateTime(DateTime.Today.AddDays(-5)), WorkedHours = 6, UserId = 2 },
         new Worklog { Id = 12, Description = "Code review for team contributions", date = DateOnly.FromDateTime(DateTime.Today.AddDays(-6)), WorkedHours = 3, UserId = 3 },
         new Worklog { Id = 13, Description = "Sprint retrospective session", date = DateOnly.FromDateTime(DateTime.Today.AddDays(-6)), WorkedHours = 2, UserId = 1 },
         new Worklog { Id = 14, Description = "Fixing security vulnerabilities", date = DateOnly.FromDateTime(DateTime.Today.AddDays(-7)), WorkedHours = 6, UserId = 2 },
         new Worklog { Id = 15, Description = "Scheduled database migration", date = DateOnly.FromDateTime(DateTime.Today.AddDays(-7)), WorkedHours = 8, UserId = 3 },
         new Worklog { Id = 16, Description = "Planning for upcoming sprint", date = DateOnly.FromDateTime(DateTime.Today.AddDays(-8)), WorkedHours = 4, UserId = 1 },
         new Worklog { Id = 17, Description = "Bug verification and closure", date = DateOnly.FromDateTime(DateTime.Today.AddDays(-9)), WorkedHours = 5, UserId = 2 },
         new Worklog { Id = 18, Description = "API versioning research", date = DateOnly.FromDateTime(DateTime.Today.AddDays(-10)), WorkedHours = 4, UserId = 1 },
         new Worklog { Id = 19, Description = "New feature mockup design", date = DateOnly.FromDateTime(DateTime.Today.AddDays(-11)), WorkedHours = 6, UserId = 3 },
         new Worklog { Id = 20, Description = "Cross-browser compatibility check", date = DateOnly.FromDateTime(DateTime.Today.AddDays(-12)), WorkedHours = 5, UserId = 2 },
         new Worklog { Id = 21, Description = "Updated project README and contribution guide", date = DateOnly.FromDateTime(DateTime.Today.AddDays(-13)), WorkedHours = 3, UserId = 1 },
         new Worklog { Id = 22, Description = "Set up environment for new dev team member", date = DateOnly.FromDateTime(DateTime.Today.AddDays(-13)), WorkedHours = 2, UserId = 2 },
         new Worklog { Id = 23, Description = "Analytics report generation and verification", date = DateOnly.FromDateTime(DateTime.Today.AddDays(-14)), WorkedHours = 7, UserId = 3 },
         new Worklog { Id = 24, Description = "Upgraded to latest .NET version", date = DateOnly.FromDateTime(DateTime.Today.AddDays(-14)), WorkedHours = 5, UserId = 1 },
         new Worklog { Id = 25, Description = "Error log monitoring and cleanup", date = DateOnly.FromDateTime(DateTime.Today.AddDays(-15)), WorkedHours = 4, UserId = 1 },
         new Worklog { Id = 26, Description = "DevOps pipeline optimization", date = DateOnly.FromDateTime(DateTime.Today.AddDays(-15)), WorkedHours = 6, UserId = 2 },
         new Worklog { Id = 27, Description = "UI theme consistency check", date = DateOnly.FromDateTime(DateTime.Today.AddDays(-16)), WorkedHours = 3, UserId = 3 },
         new Worklog { Id = 28, Description = "Optimized SQL queries for reports", date = DateOnly.FromDateTime(DateTime.Today.AddDays(-17)), WorkedHours = 6, UserId = 1 },
         new Worklog { Id = 29, Description = "Resolved merge conflicts from recent PRs", date = DateOnly.FromDateTime(DateTime.Today.AddDays(-17)), WorkedHours = 4, UserId = 2 },
         new Worklog { Id = 30, Description = "Reviewed system architecture proposal", date = DateOnly.FromDateTime(DateTime.Today.AddDays(-18)), WorkedHours = 5, UserId = 3 }
     );
            }

            modelBuilder.Entity<Worklog>()
              .Property(c => c.WorkedHours)
              .HasPrecision(4, 2);
        }
     }
}
