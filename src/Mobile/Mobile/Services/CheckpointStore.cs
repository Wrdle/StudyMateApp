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
using ChecklistItemEntity = Mobile.Data.Entites.ChecklistItem;
using CheckpointEntity = Mobile.Data.Entites.Checkpoint;
using UserCheckpointEntity = Mobile.Data.Entites.UserCheckpoint;

namespace Mobile.Services
{
    public class CheckpointStore : ICheckpointStore
    {
        //------------------------------
        //          Fields
        //------------------------------

        private readonly IUserStore _userStore;
        private readonly IAssignmentStore _assignmentStore;
        private readonly ImageConverter _imageConverter;

        //------------------------------
        //          Constructors
        //------------------------------

        public CheckpointStore()
        {
            _userStore = DependencyService.Get<IUserStore>();
            _assignmentStore = DependencyService.Get<IAssignmentStore>();
            _imageConverter = DependencyService.Get<ImageConverter>();
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
                    Description = checkpoint.Notes,
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
                        Notes = c.Description,
                        DueDate = c.DateDue.ToLocalTime(),
                        AssignedUsers = new List<CheckpointUserListItem>()
                    })
                    .ToListAsync();

                var checkpointIds = checkpoints.Select(c => c.Id).ToList();

                var userCheckpoints = await dbContext.UserCheckpoints
                    .Include(uc => uc.User)
                    .Where(uc => checkpointIds.Contains(uc.CheckpointId))
                    .Select(uc => new
                    {
                        CheckpointId = uc.CheckpointId,
                        User = new CheckpointUserListItem
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

        public async Task<ICollection<Checkpoint>> GetByUserId(long userId)
        {
            if (!_userStore.IsLoggedIn)
            {
                throw new Exception(Error.NotLoggedIn);
            }

            using (var dbContext = new AppDbContext())
            {
                // Get all assignment id's associated with user
                var assignments = await _assignmentStore.GetByUserIdAsync(userId, true);
                var assignmentsIds = assignments.Select(c => c.Id).ToList();

                List<Checkpoint> allAssociatedCheckpoints = new List<Checkpoint>();

                // Get all checkpoints associated with assignments and add to allAssociatedCheckpoints
                foreach (long currentAssignmentId in assignmentsIds)
                {
                    var currentCheckpoints = await dbContext.Checkpoints
                    .Where(c => c.AssignmentId == currentAssignmentId)
                    .Select(c => new Checkpoint
                    {
                        Id = c.Id,
                        AssignmentId = c.AssignmentId,
                        Title = c.Title,
                        Notes = c.Description,
                        DueDate = c.DateDue.ToLocalTime(),
                        AssignedUsers = new List<CheckpointUserListItem>()
                    })
                    .ToListAsync();

                    // Combine the two lists
                    allAssociatedCheckpoints.AddRange(currentCheckpoints);
                }

                return allAssociatedCheckpoints;
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

        public async Task<Checkpoint> GetById(long id)
        {
            using (var dbContext = new AppDbContext())
            {
                var checkpoint = await dbContext.Checkpoints
                    .Include(c => c.ChecklistItems)
                    .SingleOrDefaultAsync(c => c.Id == id);

                if (checkpoint == null)
                {
                    throw new Exception(Error.CheckpointDoesNotExist);
                }

                var checklistItems = checkpoint.ChecklistItems
                    .Select(ci => new ChecklistItem
                    {
                        Id = ci.Id,
                        Text = ci.Text,
                        IsDone = ci.IsDone
                    })
                    .ToList();

                var assignedUserEntities = await dbContext.UserCheckpoints
                    .Include(uc => uc.User)
                    .Where(uc => uc.CheckpointId == id)
                    .ToListAsync();

                var assignedUsers = assignedUserEntities
                    .Select(uc => new CheckpointUserListItem
                    {
                        Id = uc.User.Id,
                        ProfilePicture = _imageConverter.BytesToImage(uc.User.ProfilePicture),
                        Email = uc.User.Email,
                        FirstName = uc.User.FirstName,
                        LastName = uc.User.LastName
                    })
                    .ToList();

                return new Checkpoint
                {
                    Id = checkpoint.Id,
                    Title = checkpoint.Title,
                    Notes = checkpoint.Description,
                    AssignmentId = checkpoint.AssignmentId,
                    DueDate = checkpoint.DateDue,
                    AssignedUsers = assignedUsers,
                    ChecklistItems = checklistItems
                };
            }
        }

        public async Task<ChecklistItem> AddTaskToCheckpoint(long checkpointId, string task)
        {
            using (var dbContext = new AppDbContext())
            {
                var checklistItem = new ChecklistItemEntity { CheckpointId = checkpointId, Text = task, IsDone = false };
                await dbContext.ChecklistItems.AddAsync(checklistItem);
                await dbContext.SaveChangesAsync();
                return new ChecklistItem { Id = checklistItem.Id, Text = checklistItem.Text, IsDone = checklistItem.IsDone };
            }
        }

        public async Task<ChecklistItem> UpdateTaskFromCheckpoint(long checkpointId, ChecklistItem task)
        {
            using (var dbContext = new AppDbContext())
            {
                var checklistItem = await dbContext.ChecklistItems
                    .SingleOrDefaultAsync(cli => cli.Id == task.Id && cli.CheckpointId == checkpointId);

                if (checklistItem == null)
                {
                    throw new Exception(Error.ChecklistItemDoesNotExist);
                }

                checklistItem.Text = task.Text;
                checklistItem.IsDone = task.IsDone;

                dbContext.ChecklistItems.Update(checklistItem);
                await dbContext.SaveChangesAsync();
                return task;
            }
        }
        public async Task RemoveTaskFromCheckpoint(long checkpointId, long taskId)
        {
            using (var dbContext = new AppDbContext())
            {
                var checklistItem = await dbContext.ChecklistItems
                    .SingleOrDefaultAsync(cli => cli.Id == taskId && cli.CheckpointId == checkpointId);

                if (checklistItem == null)
                {
                    throw new Exception(Error.ChecklistItemDoesNotExist);
                }

                dbContext.ChecklistItems.Remove(checklistItem);
                await dbContext.SaveChangesAsync();
            }
        }

    }
}