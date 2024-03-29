﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<CoverColor> CoverColors { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Checkpoint> Checkpoints { get; set; }
        public DbSet<ChecklistItem> ChecklistItems { get; set; }

        // Many to Many / Join Tables
        public DbSet<UserSubject> UserSubjects { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserAssignment> UserAssignments { get; set; }
        public DbSet<GroupAssignment> GroupAssignments { get; set; }
        public DbSet<UserCheckpoint> UserCheckpoints { get; set; }

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
            // UserSubjects - Composite Key
            modelBuilder.Entity<UserSubject>().HasKey(us => new { us.Subject, us.UserId });

            // UserSkill - Composite Key
            modelBuilder.Entity<UserSkill>().HasKey(us => new { us.UserId, us.SkillId });

            // UserGroup - Composite Key
            modelBuilder.Entity<UserGroup>().HasKey(ug => new { ug.UserId, ug.GroupId });

            // UserAssignments - Composite Key
            modelBuilder.Entity<UserAssignment>().HasKey(ua => new { ua.UserId, ua.AssignmentId });

            // GroupAssigments - Composite Key
            modelBuilder.Entity<GroupAssignment>().HasKey(ga => new { ga.GroupId, ga.AssignmentId });

            // UserCheckPoints - Composite Key
            modelBuilder.Entity<UserCheckpoint>().HasKey(uc => new { uc.UserId, uc.CheckpointId });
        }

    }
}