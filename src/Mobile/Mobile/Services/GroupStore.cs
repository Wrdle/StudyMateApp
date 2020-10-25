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
using GroupEntity = Mobile.Data.Entites.Group;
using UserGroupEntity = Mobile.Data.Entites.UserGroup;

namespace Mobile.Services
{
    public class GroupStore : IGroupStore
    {
        //------------------------------
        //          Fields
        //------------------------------

        private readonly IUserStore _userStore;
        private readonly ImageConverter _imageConverter;

        //------------------------------
        //          Constructor
        //------------------------------

        public GroupStore()
        {
            _userStore = DependencyService.Get<IUserStore>();
            _imageConverter = DependencyService.Get<ImageConverter>();
        }

        //------------------------------
        //          Methods
        //------------------------------

        public async Task Create(string name)
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
                        var userGroups = new List<UserGroupEntity>
                        {
                            new UserGroupEntity { UserId = _userStore.CurrentUserId }
                        };

                        var group = new GroupEntity
                        {
                            Name = name,
                            DateCreated = DateTime.Now,
                            CoverPhotoBytes = new byte[] { },
                            CoverColorId = 1,
                            UserGroups = userGroups
                        };

                        await dbContext.Groups.AddAsync(group);
                        await dbContext.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw new Exception(Error.ServerFailure);
                    }
                }
            }
        }

        public async Task Update(Group group)
        {
            using (var dbContext = new AppDbContext())
            {
                var groupEntity = await dbContext.Groups.SingleOrDefaultAsync(g => g.Id == group.Id);
                if (groupEntity == null)
                {
                    throw new Exception();
                }

                groupEntity.Name = group.Name;
                groupEntity.CoverColorId = group.CoverColor.Id;
                groupEntity.CoverPhotoBytes = group.CoverPhotoBytes;
                dbContext.Groups.Update(groupEntity);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task Leave(long id)
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
                        var group = await dbContext.Groups
                            .Include(g => g.UserGroups)
                            .SingleOrDefaultAsync(g => g.Id == id);

                        var userGroup = group.UserGroups.SingleOrDefault(ug => ug.UserId == _userStore.CurrentUserId);
                        if (userGroup == null)
                        {
                            throw new Exception(Error.Unauthorized);
                        }

                        group.UserGroups.Remove(userGroup);
                        if (group.UserGroups.Count < 1)
                        {
                            dbContext.Groups.Remove(group);
                        }
                        else
                        {
                            dbContext.Groups.Update(group);
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

        public async Task<Group> GetById(long id)
        {
            if (!_userStore.IsLoggedIn)
            {
                throw new Exception(Error.NotLoggedIn);
            }

            using (var dbContext = new AppDbContext())
            {
                var group = await dbContext.Groups
                    .Include(g => g.CoverColor)
                    .SingleOrDefaultAsync(g => g.Id == id);

                var members = await dbContext.UserGroups
                    .Include(ug => ug.User)
                    .Where(ug => ug.GroupId == id)
                    .Select(ug => new UserListItem
                    {
                        Id = ug.User.Id,
                        Email = ug.User.Email,
                        FirstName = ug.User.FirstName,
                        LastName = ug.User.LastName,
                        ProfilePicture = ug.User.ProfilePictureBytes
                    })
                    .ToListAsync();

                var assignments = await dbContext.GroupAssignments
                    .Include(ga => ga.Assignment)
                    .ThenInclude(ga => ga.CoverColor)
                    .Where(ga => ga.GroupId == id)
                    .Select(ga => new Assignment
                    {
                        Id = ga.Assignment.Id,
                        Title = ga.Assignment.Title,
                        DateDue = ga.Assignment.Due,
                        Description = ga.Assignment.Description,
                        CoverColor = new CoverColor { Id = ga.Assignment.CoverColor.Id, BackgroundColor = ga.Assignment.CoverColor.BackgroundColorFromHex, FontColor = ga.Assignment.CoverColor.FontColorFromHex },
                        CoverPhotoBytes = ga.Assignment.CoverPhotoBytes
                    })
                    .ToListAsync();

                return new Group
                {
                    Id = group.Id,
                    Name = group.Name,
                    DateCreated = group.DateCreated,
                    CoverPhotoBytes = group.CoverPhotoBytes,
                    CoverColor = new CoverColor { Id = group.CoverColor.Id, BackgroundColor = group.CoverColor.BackgroundColorFromHex, FontColor = group.CoverColor.FontColorFromHex },
                    Members = members,
                    Assignments = assignments
                };
            }
        }

        public async Task<ICollection<GroupListItem>> MyGroups()
        {
            if (!_userStore.IsLoggedIn)
            {
                throw new Exception(Error.NotLoggedIn);
            }

            using (var dbContext = new AppDbContext())
            {
                var groups = await dbContext.UserGroups
                    .Include(ug => ug.Group)
                    .ThenInclude(g => g.CoverColor)
                    .Where(ug => ug.UserId == _userStore.CurrentUserId)
                    .Select(ug => new GroupListItem
                    {
                        Id = ug.Group.Id,
                        Name = ug.Group.Name,
                        DateCreated = ug.Group.DateCreated,
                        CoverPhotoBytes = ug.Group.CoverPhotoBytes,
                        CoverColor = new CoverColor { Id = ug.Group.CoverColor.Id, BackgroundColor = ug.Group.CoverColor.BackgroundColorFromHex, FontColor = ug.Group.CoverColor.FontColorFromHex }
                    })
                    .ToListAsync();

                return groups;
            }
        }

        public async Task<ICollection<GroupListItem>> Search(string searchTerm)
        {
            if (!_userStore.IsLoggedIn)
            {
                throw new Exception(Error.NotLoggedIn);
            }

            using (var dbContext = new AppDbContext())
            {
                var myGroupIds = await dbContext.UserGroups
                    .Where(ug => ug.UserId == _userStore.CurrentUserId)
                    .Select(ug => ug.GroupId)
                    .ToListAsync();

                var normalizedSearchTerm = searchTerm.ToLower();
                var groups = await dbContext.Groups
                    .Where(g => g.Name.ToLower().Contains(normalizedSearchTerm) && !myGroupIds.Contains(g.Id))
                    .Select(g => new GroupListItem { Id = g.Id, Name = g.Name })
                    .ToListAsync();

                return groups;
            }
        }

    }
}