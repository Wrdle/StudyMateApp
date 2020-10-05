using Microsoft.EntityFrameworkCore;
using Mobile.Constants;
using Mobile.Data;
using Mobile.Helpers;
using Mobile.Models;
using Mobile.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using AssignmentEntity = Mobile.Data.Entites.Assignment;
using GroupAssignmentEntity = Mobile.Data.Entites.GroupAssignment;
using UserAssignmentEntity = Mobile.Data.Entites.UserAssignment;

namespace Mobile.Services
{
    public class AssignmentStore : IAssignmentStore
    {
        //------------------------------
        //          Fields
        //------------------------------

        private readonly IUserStore _userStore;

        //------------------------------
        //          Constructor
        //------------------------------

        public AssignmentStore()
        {
            _userStore = DependencyService.Get<IUserStore>();
        }

        //------------------------------
        //          Methods
        //------------------------------

        /// <summary>
        /// Creates the assignment and return the ID if it is successful.
        /// </summary>
        /// <param name="assignment"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task<long> Create(Assignment assignment, long? groupId = null)
        {
            if (!_userStore.IsLoggedIn)
            {
                throw new Exception(Error.NotLoggedIn);
            }

            using (var dbContext = new AppDbContext())
            {
                using (var transaction = await dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var dbAssignment = new AssignmentEntity
                        {
                            Title = assignment.Title,
                            Description = assignment.Description,
                            CoverColour = ColourToHex(assignment.CoverColour),
                            CoverPhoto = await ImageToBytes(assignment.CoverPhoto),
                            Due = assignment.DateDue
                        };

                        await dbContext.Assignments.AddAsync(dbAssignment);
                        await dbContext.SaveChangesAsync();

                        if (groupId != null && groupId > 0)
                        {
                            await dbContext.GroupAssignments.AddAsync(new GroupAssignmentEntity { GroupId = groupId.Value, AssignmentId = dbAssignment.Id });
                        }
                        else
                        {
                            await dbContext.UserAssignments.AddAsync(new UserAssignmentEntity { UserId = _userStore.CurrentUserId, AssignmentId = dbAssignment.Id });
                        }
                        await dbContext.SaveChangesAsync();

                        await transaction.CommitAsync();
                        return dbAssignment.Id;
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw new Exception(Error.ServerFailure);
                    }
                }
            }
        }

        public async Task Delete(long id)
        {
            if (!_userStore.IsLoggedIn)
            {
                throw new Exception(Error.NotLoggedIn);
            }

            using (var dbContext = new AppDbContext())
            {
                using (var transaction = await dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var assignment = await dbContext.Assignments
                            .Include(a => a.UserAssignments)
                            .SingleOrDefaultAsync(a => a.Id == id);

                        if (assignment == null)
                        {
                            throw new Exception(Error.AssignmentDoesNotExist);
                        }

                        var userAssignment = assignment.UserAssignments.SingleOrDefault(ua => ua.UserId == _userStore.CurrentUserId);
                        if (userAssignment == null)
                        {
                            throw new Exception(Error.Unauthorized);
                        }

                        assignment.UserAssignments.Remove(userAssignment);
                        if (assignment.UserAssignments.Count < 1)
                        {
                            dbContext.Assignments.Remove(assignment);
                        }
                        else
                        {
                            dbContext.Assignments.Update(assignment);
                        }

                        await dbContext.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception)
                    {
                        throw new Exception(Error.ServerFailure);
                    }
                }
            }
        }

        public async Task<ICollection<Assignment>> GetByUserIdAsync(long userId, bool includeGroupAssignments)
        {
            if (!_userStore.IsLoggedIn)
            {
                throw new Exception(Error.NotLoggedIn);
            }

            using (var dbContext = new AppDbContext())
            {
                var assignments = await dbContext.UserAssignments
                    .Include(ua => ua.Assignment)
                    .Where(ua => ua.UserId == _userStore.CurrentUserId)
                    .Select(ua => new Assignment
                    {
                        Id = ua.Assignment.Id,
                        Title = ua.Assignment.Title,
                        Description = ua.Assignment.Description,
                        DateDue = ua.Assignment.Due,
                        CoverColour = HexToColour(),
                        CoverPhoto = BytesToImage(ua.Assignment.CoverPhoto),
                        Skills = new List<Skill>()
                    })
                    .ToListAsync();

                if (includeGroupAssignments)
                {
                    var groupIds = await dbContext.UserGroups
                        .Where(ug => ug.UserId == _userStore.CurrentUserId)
                        .Select(ug => ug.GroupId)
                        .ToListAsync();

                    var groupAssignments = await dbContext.GroupAssignments
                        .Include(ga => ga.Assignment)
                        .Where(ga => groupIds.Contains(ga.GroupId))
                        .Select(ga => new Assignment
                        {
                            Id = ga.Assignment.Id,
                            Title = ga.Assignment.Title,
                            Description = ga.Assignment.Description,
                            DateDue = ga.Assignment.Due,
                            CoverColour = HexToColour(),
                            CoverPhoto = BytesToImage(ga.Assignment.CoverPhoto),
                            Skills = new List<Skill>()
                        })
                        .ToListAsync();

                    assignments.AddRange(groupAssignments);
                }

                assignments.OrderByDescending(a => a.DateDue);
                return assignments;
            }
        }

        public async Task<ICollection<Assignment>> GetByGroupId(long groupId)
        {
            if (!_userStore.IsLoggedIn)
            {
                throw new Exception(Error.NotLoggedIn);
            }

            using (var dbContext = new AppDbContext())
            {
                var assignments = await dbContext.GroupAssignments
                    .Include(ga => ga.Assignment)
                    .Where(ga => ga.GroupId == groupId)
                    .Select(ga => new Assignment
                    {
                        Id = ga.Assignment.Id,
                        Title = ga.Assignment.Title,
                    })
                    .ToListAsync();

                return assignments;
            }
        }

        public async Task<Assignment> GetById(long id)
        {
            using (var dbContext = new AppDbContext())
            {
                var assignment = await dbContext.Assignments.FindAsync(id);
                if (assignment == null)
                {
                    throw new Exception(Error.AssignmentDoesNotExist);
                }

                return new Assignment
                {
                    Id = assignment.Id,
                    Title = assignment.Title,
                    Description = assignment.Description,
                    DateDue = assignment.Due,
                    CoverColour = HexToColour(),
                    CoverPhoto = BytesToImage(assignment.CoverPhoto),
                    Skills = new List<Skill>()
                };
            }
        }

        public async Task Update(Assignment assignment)
        {
            using (var dbContext = new AppDbContext())
            {
                var dbAssignment = await dbContext.Assignments.FindAsync(assignment.Id);
                if (dbAssignment == null)
                {
                    throw new Exception(Error.AssignmentDoesNotExist);
                }

                dbAssignment.Title = assignment.Title;
                dbAssignment.Description = assignment.Description;
                dbAssignment.Due = assignment.DateDue;
                dbAssignment.CoverColour = ColourToHex(assignment.CoverColour);
                dbAssignment.CoverPhoto = await ImageToBytes(assignment.CoverPhoto);

                dbContext.Assignments.Update(dbAssignment);
                await dbContext.SaveChangesAsync();
            }
        }

        public Task<long> GenerateNewAssignmentID()
        {
            long biggestId;
            using (var dbContext = new AppDbContext())
            {
                biggestId = dbContext.Assignments
                .Select(assignment => assignment.Id)
                .Max();
                return Task.FromResult(biggestId);
            }
        }

        //------------------------------
        //          Helpers
        //------------------------------

        private async Task<byte[]> ImageToBytes(ImageSource imageSource)
        {
            var cancellationToken = System.Threading.CancellationToken.None;
            using (var imageStream = await ((StreamImageSource)imageSource).Stream(cancellationToken))
            using (var byteStream = new MemoryStream())
            {
                await imageStream.CopyToAsync(byteStream);
                return byteStream.ToArray();
            }
        }

        private ImageSource BytesToImage(byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                return ImageSource.FromStream(() => { return stream; });
            }
        }

        private string ColourToHex(SMColour colour)
        {
            return colour.BackgroundColour.ToHex();
        }

        private SMColour HexToColour()
        {
            return SMColours.Blue;
        }

    }
}
