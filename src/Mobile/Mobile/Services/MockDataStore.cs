using Mobile.Models;
using Mobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mobile.Services
{
    public class MockDataStore : IAssignmentStore<Assignment>
    {
        ICollection<Assignment> assignments;

        public MockDataStore()
        {
            assignments = new List<Assignment>()
            {
                new Assignment { Id = 1, Title="IAB330 Assignment 1", Description = "This is a short description", Skills = null, DateDue = new DateTime(2020, 12, 25), CoverPhoto = null, CoverColour = null},
                new Assignment { Id = 1, Title="CAB220 Assignment 4", Description = "This is a short description for my CAB220 Assignment", Skills = null, DateDue = new DateTime(2020, 11, 25), CoverPhoto = null, CoverColour = null}
            };
        }

        public async Task<ICollection<Assignment>> GetAllByUserAsync(int userID, bool forceRefresh = false)
        {
            return await Task.FromResult(assignments);
        }

        public async Task<Assignment> GetById(long id)
        {
            foreach (Assignment assignment in assignments)
            {
                if (assignment.Id == id)
                {
                    return await Task.FromResult(assignment);
                }
            }

            return null;
        }
    }
}
