using Microsoft.EntityFrameworkCore;
using Mobile.Data.Entites;
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

                TestUser.Email = TestUser.Email.ToUpper();
                await dbContext.Users.AddAsync(TestUser);
                await dbContext.SaveChangesAsync();

                await dbContext.Groups.AddAsync(new Group { Name = "Test Group", UserGroups = new List<UserGroup> { new UserGroup { UserId = TestUser.Id } } });
                await dbContext.SaveChangesAsync();
            }
        }

    }
}
