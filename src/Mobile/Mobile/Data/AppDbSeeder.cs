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

        private ICollection<User> users = new List<User>
        {
            new User { Email = "marko@studymate.com", FirstName = "Marko", LastName = "Lmnop" },
            new User { Email = "louis@studymate.com", FirstName = "Louis", LastName = "Wasd" },
            new User { Email = "matt@studymate.com", FirstName = "Matt", LastName = "Hjkl" },
            new User { Email = "kiki@studymate.com", FirstName = "Kiki", LastName = "Qwer" }
        };

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
                // If users don't exist in the database, add users.
                if (!await dbContext.Users.AnyAsync())
                {
                    await dbContext.Users.AddRangeAsync(users);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

    }
}
