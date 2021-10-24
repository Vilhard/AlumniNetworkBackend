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
        /// <summary>
        /// Service returns list of groups that filters out groups that requesting user is not member of and is private.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Group>> GetAllGroups(string userId)
        {
            return await _context.Groups
                .Include(g => g.Members)
                .Where(g => g.IsPrivate == false || g.Members.Any(u => u.Id.Contains(userId)))
                .ToListAsync();
        }
        /// <summary>
        /// Service returns a group that corresponding to id parameter.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Group> GetGroupById(int id)
        {
            return await _context.Groups
                .Include(m => m.Members)
                .Where(x => x.Id == id)
                .SingleAsync();
        }
        /// <summary>
        /// Service adds new parameter defined group to context group list and finds parameter defined user with id.
        /// </summary>
        /// <param name="newGroup"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Service handles finding group to be updated by parameter defined group id and adds requesting user to found member list as first subscriber
        /// </summary>
        /// <param name="group"></param>
        /// <param name="user"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Service handles finding group to be updated by parameter defined group id and adds defined user to that groups member list
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Service checks if group exists in the context
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool GroupExists(int id)
        {
            return _context.Groups.Any(g => g.Id == id);
        }
    }
}
