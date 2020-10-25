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
using SkillEntity = Mobile.Data.Entites.Skill;
using UserEntity = Mobile.Data.Entites.User;
using UserSubjectEntity = Mobile.Data.Entites.UserSubject;
using UserSkillEntity = Mobile.Data.Entites.UserSkill;
using Mobile.Views;

namespace Mobile.Services
{
    public class UserStore : IUserStore
    {
        //------------------------------
        //          Fields
        //------------------------------

        private readonly ImageConverter _imageConverter;

        private static long? _currentUserId = null;

        /// <summary>
        /// Gets the ID of the currently logged in user. Returns 0 if there is no currently logged in user.
        /// </summary>
        public long CurrentUserId
        {
            get
            {
                try
                {
                    return _currentUserId.Value;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        public bool IsLoggedIn
        {
            get
            {
                return _currentUserId != null;
            }
        }

        //------------------------------
        //          Constructors
        //------------------------------

        public UserStore()
        {
            _imageConverter = DependencyService.Get<ImageConverter>();
        }

        //------------------------------
        //          Methods
        //------------------------------

        public async Task Login(string email, string password)
        {
            using (var dbContext = new AppDbContext())
            {
                var normalizedEmail = email.ToUpper();
                var user = await dbContext.Users.SingleOrDefaultAsync(u => u.Email == normalizedEmail);
                if (user == null)
                {
                    throw new Exception(Error.IncorrectEmailPassword);
                }

                _currentUserId = user.Id;
            }
        }

        public async Task Logout()
        {
            _currentUserId = null;
            Application.Current.MainPage = new LoginPage();
        }

        public async Task CreateAccount(string email, string firstName, string lastName)
        {
            var user = new UserEntity
            {
                Email = email.ToUpper(),
                FirstName = firstName.ToUpper(),
                LastName = lastName.ToUpper(),
                ProfilePictureBytes = new byte[] { }
            };

            using (var dbContext = new AppDbContext())
            {
                if (await dbContext.Users.AnyAsync(u => u.Email == user.Email))
                {
                    throw new Exception(Error.AccountWithEmailExists);
                }

                try
                {
                    await dbContext.Users.AddAsync(user);
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception)
                {
                    throw new Exception(Error.ServerFailure);
                }
            }
        }

        public async Task<User> GetProfile()
        {
            if (_currentUserId == null)
            {
                throw new Exception(Error.NotLoggedIn);
            }

            using (var dbContext = new AppDbContext())
            {
                // Get user entity
                var user = await dbContext.Users
                    .Include(u => u.UserSubjects)
                    .SingleOrDefaultAsync(u => u.Id == CurrentUserId);

                // Get user's current skills
                var skills = await dbContext.UserSkills
                    .Include(us => us.User)
                    .Include(us => us.Skill)
                    .Where(us => us.UserId == _currentUserId.Value)
                    .Select(us => us.Skill.Name)
                    .ToListAsync();

                return new User
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Institution = user.Institution,
                    Major = user.Major,

                    ProfilePicture = user.ProfilePictureBytes != null && user.ProfilePictureBytes.Length > 0 ? _imageConverter.BytesToImage(user.ProfilePictureBytes) : ImageSource.FromFile("user.png"),
                    CurrentSubjects = user.UserSubjects.Where(us => us.IsCurrent).Select(us => us.Subject).ToList(),
                    PreviousSubjects = user.UserSubjects.Where(us => !us.IsCurrent).Select(us => us.Subject).ToList(),
                    Skills = skills
                };
            }
        }

        public async Task<User> UpdateUser(User user)
        {
            if (_currentUserId == null)
            {
                throw new Exception(Error.NotLoggedIn);
            }

            using (var dbContext = new AppDbContext())
            {
                using (var transaction = await dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // Get current user
                        var savedUser = await dbContext.Users
                            .Include(u => u.UserSubjects)
                            .Include(u => u.UserSkills)
                            .SingleOrDefaultAsync(u => u.Id == _currentUserId.Value);

                        if (savedUser == null)
                        {
                            throw new Exception(Error.AccountDoesNotExist);
                        }

                        // Update user fields
                        savedUser.FirstName = user.FirstName;
                        savedUser.LastName = user.LastName;
                        savedUser.Institution = user.Institution;
                        savedUser.Major = user.Major;
                        savedUser.ProfilePictureBytes = user.ProfilePictureBytes;
                        savedUser.UserSubjects.Clear();

                        var currentSubjects = user.CurrentSubjects.Select((cs) => new UserSubjectEntity { Subject = cs, IsCurrent = true });
                        (savedUser.UserSubjects as List<UserSubjectEntity>).AddRange(currentSubjects);
                        var previousSubjects = user.PreviousSubjects.Select((cs) => new UserSubjectEntity { Subject = cs, IsCurrent = false });
                        (savedUser.UserSubjects as List<UserSubjectEntity>).AddRange(currentSubjects);

                        savedUser.UserSkills.Clear();
                        dbContext.Users.Update(savedUser);
                        await dbContext.SaveChangesAsync();

                        // Normalize updated skills
                        for (int i = 0; i < user.Skills.Count; i++)
                        {
                            user.Skills[i] = user.Skills[i].Trim().ToLower();
                        }

                        // Get skills that exist in the database
                        var existingSkills = await dbContext.Skills
                            .Where(s => user.Skills.Contains(s.Name))
                            .ToListAsync();

                        // Get skills that don't exist in the database and add them
                        var skillsNotInDb = new List<SkillEntity>();
                        foreach (var skill in user.Skills)
                        {
                            if (!existingSkills.Any(s => s.Name == skill))
                            {
                                skillsNotInDb.Add(new SkillEntity { Name = skill });
                            }
                        }
                        await dbContext.Skills.AddRangeAsync(skillsNotInDb);
                        await dbContext.SaveChangesAsync();
                        existingSkills.Concat(skillsNotInDb);

                        // Assign skills to user
                        var newUserSkills = existingSkills
                            .Select(s => new UserSkillEntity { UserId = savedUser.Id, SkillId = s.Id })
                            .ToList();
                        await dbContext.UserSkills.AddRangeAsync(newUserSkills);
                        await dbContext.SaveChangesAsync();

                        // Commit transaction
                        await transaction.CommitAsync();
                        return user;
                    }
                    catch (Exception)
                    {
                        throw new Exception(Error.ServerFailure);
                    }
                }
            }
        }

    }
}