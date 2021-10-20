using AlumniNetworkBackend.Models;
using AlumniNetworkBackend.Models.Domain;
using AlumniNetworkBackend.Models.DTO.TopicDTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Services
{
    public class TopicService: ITopicService
    {
        private readonly AlumniNetworkDbContext _context;

        public TopicService(AlumniNetworkDbContext context)
        {
            _context = context;
        }

        public async Task<Topic> Create(Topic newTopic, User user)
        {
            var returnValue = _context.Topics.Add(newTopic);
            await _context.SaveChangesAsync();
            await UpdateUsers(returnValue.Entity, user);
            return returnValue.Entity;
        }

        public async Task<Topic> UpdateUsers(Topic topic, User user)
        {
            
            Topic topicToUpdateUsers = await _context.Topics.Include(u => u.Users).Where(t => t.Id == topic.Id).FirstAsync();
            List<User> userList = new() { user };
            topicToUpdateUsers.Users = userList;
            await _context.SaveChangesAsync();

            return topicToUpdateUsers;
        }
    }
}
