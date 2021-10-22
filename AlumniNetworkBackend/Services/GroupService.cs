using AlumniNetworkBackend.Models;
using AlumniNetworkBackend.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Services
{
    public class GroupService : IGroupService
    {
        private readonly AlumniNetworkDbContext _context;

        public GroupService(AlumniNetworkDbContext context)
        {
            _context = context;
        }

        public async Task<List<Group>> GetAllGroups()
        {
            return await _context.Groups
                .Include(t => t.Members)
                .ToListAsync();
        }

        public async Task<Group> GetGroupById(int id)
        {
            return await _context.Groups.Include(t => t.Members)
                .Where(t => t.Id == id)
                .FirstAsync();
        }

        public async Task<Group> Create(Group newGroup, string userId)
        {
            var returnValue = _context.Groups.Add(newGroup);
            await _context.SaveChangesAsync();

            User user = await _context.Users.FindAsync(userId);

            if (user == null)
                return null;

            await AddUserToCreatedGroup(returnValue.Entity, user);
            return returnValue.Entity;
        }

        public async Task AddUserToCreatedGroup(Group group, User user)
        {
            Group groupToUpdateUsers = await _context.Groups
                .Include(u => u.Members)
                .Where(t => t.Id == group.Id)
                .FirstAsync();

            List<User> userList = new() { user };
            groupToUpdateUsers.Members = userList;
            await _context.SaveChangesAsync();
        }

        public async Task<Group> AddUserToGroup(int groupId, string userId)
        {
            Group groupToUpdate = await _context.Groups
                .Include(t => t.Members)
                .Where(t => t.Id == groupId)
                .FirstAsync();

            if (groupToUpdate == null)
                return null;

            List<User> users = groupToUpdate.Members.ToList();

            User groupNewUser = await _context.Users.FindAsync(userId);

            if (groupNewUser == null)
                return null;

            users.Add(groupNewUser);
            groupToUpdate.Members = users;
            await _context.SaveChangesAsync();

            return groupToUpdate;
        }
    }
}
