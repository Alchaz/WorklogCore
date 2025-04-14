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
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "user1", DailyMinHours = 3, DailyMaxHours=5, Role = UserRoleEnum.User.ToString() },
                 new User { Id = 2, Name = "user2", DailyMinHours = 7, DailyMaxHours = 9, Role = UserRoleEnum.User.ToString() },
                 new User { Id = 3, Name = "admin", DailyMinHours = 0, DailyMaxHours = 15, Role = UserRoleEnum.Admin.ToString() }
            
            );

            modelBuilder.Entity<Worklog>().HasData(
                new Worklog
                {
                    Id = 1,
                    Description = "Scheduled to work on backend APIs",
                    date = DateOnly.FromDateTime(DateTime.Today.AddDays(-1)),
                    WorkedHours = 0,
                    UserId = 1
                },
                new Worklog
                {
                    Id = 2,
                    Description = "Planned bug fixing in reports module",
                    date = DateOnly.FromDateTime(DateTime.Today.AddDays(-1)),
                    WorkedHours = 0,
                    UserId = 1
                },
                new Worklog
                {
                    Id = 3,
                    Description = "Documentation update planned",
                    date = DateOnly.FromDateTime(DateTime.Today.AddDays(-2)),
                    WorkedHours = 0,
                    UserId = 1
                },
                new Worklog
                {
                    Id = 4,
                    Description = "Frontend layout tasks scheduled",
                    date = DateOnly.FromDateTime(DateTime.Today.AddDays(-2)),
                    WorkedHours = 0,
                    UserId = 2
                },
                new Worklog
                {
                    Id = 5,
                    Description = "Integration test planning",
                    date = DateOnly.FromDateTime(DateTime.Today.AddDays(-2)),
                    WorkedHours = 0,
                    UserId = 2
                },
                new Worklog
                {
                    Id = 6,
                    Description = "Team meeting setup",
                    date = DateOnly.FromDateTime(DateTime.Today.AddDays(-3)),
                    WorkedHours = 0,
                    UserId = 2
                }
               );

            modelBuilder.Entity<Worklog>()
              .Property(c => c.WorkedHours)
              .HasPrecision(4, 2);
        }
     }
}
