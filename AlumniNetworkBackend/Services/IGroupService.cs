using AlumniNetworkBackend.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Services
{
    public interface IGroupService
    {
        public Task<List<Group>> GetAllGroups(string userId);
        public Task<Group> GetGroupById(int id);
        public Task<Group> Create(Group newGroup, string userId);
        public Task AddUserToCreatedGroup(Group group, User user);
        public Task<Group> AddUserToGroup(int groupId, string userId);
        public bool GroupExists(int id);
    }
}
