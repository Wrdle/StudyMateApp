using Microsoft.EntityFrameworkCore;
using Mobile.Data.Entites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mobile.Data
{
    public class AppDbSeeder
    {
        //------------------------------
        //          Fields
        //------------------------------

        public static User TestUser = new User { Email = "test-user@studymate.com", FirstName = "Test", LastName = "User" };

        //------------------------------
        //          Constructors
        //------------------------------

        public AppDbSeeder()
        {

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

                // USER
                TestUser.Email = TestUser.Email.ToUpper();
                await dbContext.Users.AddAsync(TestUser);
                await dbContext.SaveChangesAsync();

                // GROUP
                var group = new Group { Name = "Test Group", UserGroups = new List<UserGroup> { new UserGroup { UserId = TestUser.Id } } };
                await dbContext.Groups.AddAsync(group);
                await dbContext.SaveChangesAsync();

                // USER ASSIGNMENT
                var assignment = new Assignment
                {
                    Title = "Test Assignment",
                    Description = "blah blah blah",
                    Due = DateTime.UtcNow,
                    CoverColour = "444444",
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
                    CoverColour = "333333",
                    GroupAssignments = new List<GroupAssignment>
                    {
                        new GroupAssignment { GroupId = group.Id }
                    }
                };
                await dbContext.Assignments.AddAsync(groupAssignment);
                await dbContext.SaveChangesAsync();
            }
        }

    }
}
