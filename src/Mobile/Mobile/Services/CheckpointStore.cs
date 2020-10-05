using Microsoft.EntityFrameworkCore;
using Mobile.Constants;
using Mobile.Data;
using Mobile.Models;
using Mobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using CheckpointEntity = Mobile.Data.Entites.Checkpoint;
using UserCheckpointEntity = Mobile.Data.Entites.UserCheckpoint;

namespace Mobile.Services
{
    public class CheckpointStore : ICheckpointstore
    {
        //------------------------------
        //          Fields
        //------------------------------

        private readonly IUserStore _userStore;

        //------------------------------
        //          Constructors
        //------------------------------

        public CheckpointStore()
        {
            _userStore = DependencyService.Get<IUserStore>();
        }

        //------------------------------
        //          Methods
        //------------------------------

        public async Task<Checkpoint> Add(Checkpoint checkpoint)
        {
            if (!_userStore.IsLoggedIn)
            {
                throw new Exception(Error.NotLoggedIn);
            }

            using (var dbContext = new AppDbContext())
            {
                var assignment = await dbContext.Assignments
                    .Include(a => a.UserAssignments)
                    .SingleOrDefaultAsync(a => a.Id == checkpoint.AssignmentId);

                if (assignment == null)
                {
                    throw new Exception(Error.AssignmentDoesNotExist);
                }

                if (assignment.UserAssignments.Any(ua => ua.UserId == _userStore.CurrentUserId))
                {
                    throw new Exception(Error.Unauthorized);
                }

                var newCheckpoint = new CheckpointEntity
                {
                    AssignmentId = checkpoint.AssignmentId,
                    Title = checkpoint.Title,
                    Description = checkpoint.Description,
                    DateDue = checkpoint.DueDate.ToUniversalTime()
                };

                await dbContext.Checkpoints.AddAsync(newCheckpoint);
                await dbContext.SaveChangesAsync();
                checkpoint.Id = newCheckpoint.Id;
                return checkpoint;
            }
        }

        public async Task AssignToUser(long checkpointId, long userId)
        {
            if (!_userStore.IsLoggedIn)
            {
                throw new Exception(Error.NotLoggedIn);
            }

            using (var dbContext = new AppDbContext())
            {
                try
                {
                    await dbContext.UserCheckpoints.AddAsync(new UserCheckpointEntity
                    {
                        UserId = userId,
                        CheckpointId = checkpointId
                    });

                    await dbContext.SaveChangesAsync();
                }
                catch (Exception)
                {
                    throw new Exception(Error.ServerFailure);
                }
            }
        }

        public async Task<ICollection<Checkpoint>> GetByAssignmentId(long assignmentId)
        {
            if (!_userStore.IsLoggedIn)
            {
                throw new Exception(Error.NotLoggedIn);
            }

            using (var dbContext = new AppDbContext())
            {
                var checkpoints = await dbContext.Checkpoints
                    .Where(c => c.AssignmentId == assignmentId)
                    .Select(c => new Checkpoint
                    {
                        Id = c.Id,
                        AssignmentId = c.AssignmentId,
                        Title = c.Title,
                        Description = c.Description,
                        DueDate = c.DateDue.ToLocalTime(),
                        AssignedUsers = new List<UserListItem>()
                    })
                    .ToListAsync();

                var userCheckpoints = await dbContext.UserCheckpoints
                    .Include(uc => uc.User)
                    .Where(uc => checkpoints.Any(c => c.Id == uc.CheckpointId))
                    .Select(uc => new
                    {
                        CheckpointId = uc.CheckpointId,
                        User = new UserListItem
                        {
                            Id = uc.User.Id,
                            Email = uc.User.Email,
                            FirstName = uc.User.FirstName,
                            LastName = uc.User.LastName
                        }
                    })
                    .ToListAsync();

                foreach (var checkpoint in checkpoints)
                {
                    var assignedUsers = userCheckpoints
                        .Where(uc => uc.CheckpointId == checkpoint.Id)
                        .Select(uc => uc.User)
                        .ToList();

                    checkpoint.AssignedUsers.AddRange(assignedUsers);
                }

                return checkpoints;
            }
        }

        public async Task Remove(long checkpointId)
        {
            if (!_userStore.IsLoggedIn)
            {
                throw new Exception(Error.NotLoggedIn);
            }

            using (var dbContext = new AppDbContext())
            {
                var checkpoint = await dbContext.Checkpoints.FindAsync(checkpointId);
                dbContext.Remove(checkpoint);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task UnassignUser(long checkpointId, long userId)
        {
            if (!_userStore.IsLoggedIn)
            {
                throw new Exception(Error.NotLoggedIn);
            }

            using (var dbContext = new AppDbContext())
            {
                var userCheckpoint = await dbContext.UserCheckpoints
                    .SingleOrDefaultAsync(uc => uc.CheckpointId == checkpointId && uc.UserId == userId);

                if (userCheckpoint != null)
                {
                    dbContext.UserCheckpoints.Remove(userCheckpoint);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
