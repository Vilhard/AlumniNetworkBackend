using AlumniNetworkBackend.Models;
using AlumniNetworkBackend.Models.Domain;
using AlumniNetworkBackend.Models.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Services
{
    public class UserService : IUserService
    {
        private readonly AlumniNetworkDbContext _context;

        public UserService(AlumniNetworkDbContext context)
        {
            _context = context;
        }

        public async Task<User> AddUser(User user)
        {
            var userInDB = await _context.Users.FindAsync(user.Id);
            if (userInDB == null)
            {
                var newUser = await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return newUser.Entity;
            }
            else
                return userInDB;
            
        }

        public async Task<User> GetUser(string id)
        {
            return (await _context.Users.FindAsync(id));
        }

        public async Task<User> UpdateUser(string id, User dtoUser)
        {
            if(dtoUser.Id != id)
            {
                return null;
            }

            User user = await _context.Users.FindAsync(id);

            if (dtoUser.Name != null)
                user.Name = dtoUser.Name;

            if (dtoUser.Username != null)
                user.Username = dtoUser.Username;

            if (dtoUser.Picture != null)
                user.Picture = dtoUser.Picture;

            if (dtoUser.Status != null)
                user.Status = dtoUser.Status;

            if (dtoUser.Bio != null)
                user.Bio = dtoUser.Bio;

            if (dtoUser.FunFact != null)
                user.FunFact = dtoUser.FunFact;

            await _context.SaveChangesAsync();

            return user;
        }
    }
}
