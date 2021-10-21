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

        public async Task<List<Topic>> GetAllTopics()
        {
            return await _context.Topics.Include(t => t.Users).ToListAsync();
        }

        public async Task<Topic> GetTopicById(int id)
        {
            return await _context.Topics.Include(t => t.Users).Where(t => t.Id == id).FirstAsync();
        }

        public async Task<Topic> Create(Topic newTopic, User user)
        {
            var returnValue = _context.Topics.Add(newTopic);
            await _context.SaveChangesAsync();
            await AddUserToCreatedTopic(returnValue.Entity, user);
            return returnValue.Entity;
        }

        public async Task AddUserToCreatedTopic(Topic topic, User user)
        {
            
            Topic topicToUpdateUsers = await _context.Topics.Include(u => u.Users).Where(t => t.Id == topic.Id).FirstAsync();
            List<User> userList = new() { user };
            topicToUpdateUsers.Users = userList;
            await _context.SaveChangesAsync();
        }

        //public async bool AddUserToTopic(int topicId, string userId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
