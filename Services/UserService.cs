﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchollOfDevs.Entities;
using SchollOfDevs.Helpers;
using BC = BCrypt.Net.BCrypt;

namespace SchollOfDevs.Services
{
    public interface IUserService
    {
        public Task<User> Create(User user);
        public Task<User> GetById(int id);
        public Task<List<User>> GetAll();
        public Task Update(User userIn, int id);
        public Task Delete(int id);

    }
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {   
               _context = context;
        }
        public async Task<User> Create(User user)
        {
            if(!user.Password.Equals(user.ConfirmPassword))
                throw new Exception("Password does not match ConfirmPassword");

            User userDb = await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.UserName == user.UserName);

            if (userDb is null)
                throw new Exception($"UserName {user.UserName} already exist.");

            user.Password = BC.HashPassword(user.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task Delete(int id)
        {
            User userDb = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);

            if (userDb is null)
                throw new Exception($"UserName {id} not found");

            _context.Users.Remove(userDb);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAll() => await _context.Users.ToListAsync();

        public async Task<User> GetById(int id)
        {
            User userDb = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);

            if (userDb is null)
                throw new Exception($"UserName {id} not found");

            return userDb;
        }

        public async Task Update(User userIn, int id)
        {
            if(userIn.Id != id)
                throw new Exception("Route id differs User id");

            else if (!userIn.Password.Equals(userIn.ConfirmPassword))
                throw new Exception("Password does not match ConfirmPassword");

            User userDb = await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == id);

            if (userDb is null)
                throw new Exception($"UserName {id} not found");
            else if (!BC.Verify(userIn.CurrentPassword, userDb.Password))
                throw new Exception("Incorrect Password");

            userIn.CreateAt = userDb.CreateAt;
            userIn.Password = BC.HashPassword(userDb.Password);

            _context.Entry(userIn).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
