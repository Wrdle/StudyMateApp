﻿using Microsoft.EntityFrameworkCore;
using Mobile.Constants;
using Mobile.Data;
using Mobile.Models;
using Mobile.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using UserEntity = Mobile.Data.Entites.User;

namespace Mobile.Services
{
    public class UserStore : IUserStore
    {
        //------------------------------
        //          Fields
        //------------------------------

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
        }

        public async Task CreateAccount(string email, string firstName, string lastName)
        {
            var user = new UserEntity
            {
                Email = email.ToUpper(),
                FirstName = firstName.ToUpper(),
                LastName = lastName.ToUpper(),
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
                var userSkills = await dbContext.UserSkills
                    .Include(us => us.User)
                    .Include(us => us.Skill)
                    .Where(us => us.UserId == _currentUserId.Value)
                    .ToListAsync();

                if (userSkills.Count < 1)
                {
                    throw new Exception(Error.AccountDoesNotExist);
                }

                var user = userSkills[0].User;
                var skills = userSkills.Select(us => new Skill(us.Skill.Id, us.Skill.Name)).ToList();

                return new User
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Institution = user.Institution,
                    Major = user.Major,
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
                var savedUser = await dbContext.Users.SingleAsync(u => u.Id == _currentUserId.Value);
                if (savedUser == null)
                {
                    throw new Exception(Error.AccountDoesNotExist);
                }

                savedUser.FirstName = user.FirstName;
                savedUser.LastName = user.LastName;
                savedUser.Institution = user.Institution;
                savedUser.Major = user.Major;

                try
                {
                    dbContext.Users.Update(savedUser);
                    await dbContext.SaveChangesAsync();
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