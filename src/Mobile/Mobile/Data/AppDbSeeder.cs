using Mobile.Data.Entites;
using Mobile.Services;
using Mobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mobile.Data
{
    public class AppDbSeeder
    {
        //------------------------------
        //          Fields
        //------------------------------

        private readonly IAssignmentStore _assignmentStore;
        private readonly ImageConverter _imageConverter;


        public static User TestUser = new User
        {
            Email = "test-user@studymate.com",
            FirstName = "Test",
            LastName = "User",
            Institution = "QUT",
            Major = "Pro Gamer",
            UserSubjects = new List<UserSubject>
            {
                new UserSubject { Subject = "Swag101", IsCurrent = true },
                new UserSubject { Subject = "HowToBuyALamboCash202", IsCurrent = false }
            }
        };

        //------------------------------
        //          Constructors
        //------------------------------

        public AppDbSeeder()
        {
            _assignmentStore = DependencyService.Get<IAssignmentStore>();
            _imageConverter = DependencyService.Get<ImageConverter>();
        }

        //------------------------------
        //          Methods
        //------------------------------

        public async Task Seed()
        {
            using (var dbContext = new AppDbContext())
            {
                await dbContext.Database.EnsureDeletedAsync();
                await dbContext.Database.EnsureCreatedAsync();

                // COVER COLORS
                var colors = new List<CoverColor>
                {
                    new CoverColor { BackgroundColorHex = "4F4F4F", FontColorHex = "FFFFFF" },
                    new CoverColor { BackgroundColorHex = "828282", FontColorHex = "FFFFFF" },
                    new CoverColor { BackgroundColorHex = "EB5757", FontColorHex = "FFFFFF" },
                    new CoverColor { BackgroundColorHex = "F2994A", FontColorHex = "FFFFFF" },
                    new CoverColor { BackgroundColorHex = "F2C94C", FontColorHex = "FFFFFF" },
                    new CoverColor { BackgroundColorHex = "219653", FontColorHex = "FFFFFF" },
                    new CoverColor { BackgroundColorHex = "27AE60", FontColorHex = "FFFFFF" },
                    new CoverColor { BackgroundColorHex = "6FCF97", FontColorHex = "4F4F4F" },
                    new CoverColor { BackgroundColorHex = "2F80ED", FontColorHex = "FFFFFF" },
                    new CoverColor { BackgroundColorHex = "2D9CDB", FontColorHex = "FFFFFF" },
                    new CoverColor { BackgroundColorHex = "56CCF2", FontColorHex = "FFFFFF" },
                    new CoverColor { BackgroundColorHex = "9B51E0", FontColorHex = "FFFFFF" },
                    new CoverColor { BackgroundColorHex = "BB6BD9", FontColorHex = "FFFFFF" }
                };

                await dbContext.CoverColors.AddRangeAsync(colors);
                await dbContext.SaveChangesAsync();

                // USER
                TestUser.Email = TestUser.Email.ToUpper();
                await dbContext.Users.AddAsync(TestUser);
                await dbContext.SaveChangesAsync();

                // GROUP
                var group = new Group
                {
                    Name = "Test Group",
                    DateCreated = DateTime.Now,
                    CoverPhoto = await _imageConverter.ImageToBytes(null),
                    CoverColorId = 6,
                    UserGroups = new List<UserGroup>
                    {
                        new UserGroup { UserId = TestUser.Id }
                    }
                };

                await dbContext.Groups.AddAsync(group);
                await dbContext.SaveChangesAsync();

                // USER ASSIGNMENT
                var assignment = new Assignment
                {
                    Title = "Test Assignment",
                    Description = "blah blah blah",
                    Due = DateTime.UtcNow,
                    CoverPhoto = await _imageConverter.ImageToBytes(null),
                    CoverColorId = 1,
                    UserAssignments = new List<UserAssignment>
                    {
                        new UserAssignment { UserId = TestUser.Id }
                    }
                };
                await dbContext.Assignments.AddAsync(assignment);

                // GROUP ASSIGNMENT
                var groupAssignment = new Assignment
                {
                    Title = "Test Group Assignment",
                    Description = "blafadagsflah",
                    Due = DateTime.UtcNow,
                    CoverPhoto = await _imageConverter.ImageToBytes(null),
                    CoverColorId = 8,
                    GroupAssignments = new List<GroupAssignment>
                    {
                        new GroupAssignment { GroupId = group.Id }
                    }
                };
                await dbContext.Assignments.AddAsync(groupAssignment);
                await dbContext.SaveChangesAsync();

                // CHECKPOINT
                var checkpoint = new Checkpoint
                {
                    Title = "Checkpoint 1",
                    Description = "This is the description/notes for checkpoint 1",
                    DateDue = DateTime.Now.AddDays(2),
                    Assignment = assignment,
                    ChecklistItems = new List<ChecklistItem>
                    {
                        new ChecklistItem {  Text = "dfgdgdg", IsDone = false },
                        new ChecklistItem {  Text = "asdf", IsDone = true },
                    }
                };
                await dbContext.Checkpoints.AddAsync(checkpoint);

                // CHECKPOINT WITH USERS
                var checkpoint2 = new Checkpoint
                {
                    Title = "Checkpoint 2",
                    Description = "This is the description/notes for checkpoint 2",
                    DateDue = DateTime.Now.AddDays(3),
                    Assignment = assignment,
                    UserCheckpoints = new List<UserCheckpoint> {
                        new UserCheckpoint { UserId = TestUser.Id }
                    }
                };
                await dbContext.Checkpoints.AddAsync(checkpoint2);

                // CHECKPOINT WITH USERS OUTSIDE NEXT 7 DAYS
                var checkpoint3 = new Checkpoint
                {
                    Title = "Checkpoint 3",
                    Description = "This is the description/notes for checkpoint 3",
                    DateDue = DateTime.Now.AddDays(10),
                    Assignment = groupAssignment,
                    UserCheckpoints = new List<UserCheckpoint> {
                        new UserCheckpoint { UserId = TestUser.Id }
                    }
                };
                await dbContext.Checkpoints.AddAsync(checkpoint3);
                await dbContext.SaveChangesAsync();

                // NEW USERSKILL TEST
                var skill = new Skill
                {
                    Id = 1,
                    Name = "Python",
                    UserSkills = new List<UserSkill>
                    {
                        new UserSkill { UserId = TestUser.Id }
                    }
                };
                var skill2 = new Skill
                {
                    Id = 2,
                    Name = "Unity",
                    UserSkills = new List<UserSkill>
                    {
                        new UserSkill { UserId = TestUser.Id }
                    }
                };

                await dbContext.Skills.AddAsync(skill);
                await dbContext.Skills.AddAsync(skill2);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}