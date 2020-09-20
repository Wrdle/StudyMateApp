using Microsoft.EntityFrameworkCore;
using Mobile.Data.Entites;
using System.IO;
using Xamarin.Essentials;

namespace Mobile.Data
{
    public class AppDbContext : DbContext
    {
        //------------------------------
        //          Fields
        //------------------------------

        public DbSet<User> Users { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Assignment> Assignments { get; set; }

        // Many to Many / Join Tables
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserAssignment> UserAssignments { get; set; }
        public DbSet<GroupAssignment> GroupAssignments { get; set; }

        //------------------------------
        //          Constructors
        //------------------------------

        public AppDbContext()
        {
            SQLitePCL.Batteries_V2.Init();
            Database.EnsureCreated();
        }

        //------------------------------
        //          Methods
        //------------------------------

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "studymate.db3");
            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // UserSkill - Composite Key
            modelBuilder.Entity<UserSkill>().HasKey(us => new { us.UserId, us.SkillId });

            // UserGroup - Composite Key
            modelBuilder.Entity<UserGroup>().HasKey(ug => new { ug.UserId, ug.GroupId });

            // UserAssignments - Composite Key
            modelBuilder.Entity<UserAssignment>().HasKey(ua => new { ua.UserId, ua.AssignmentId });

            // GroupAssigments - Composite Key
            modelBuilder.Entity<GroupAssignment>().HasKey(ga => new { ga.GroupId, ga.AssignmentId });
        }

    }
}
