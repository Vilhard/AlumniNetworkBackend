using AlumniNetworkBackend.Models.Domain;
using AlumniNetworkBackend.Models.DTO.TopicDTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Services
{
    public interface ITopicService
    {
        public Task<List<Topic>> GetAllTopics();
        public Task<Topic> GetTopicById(int id);
        public Task<Topic> Create(Topic newTopic, string userId);
        public Task AddUserToCreatedTopic(Topic topic, User user);
        public Task<Topic> AddUserToTopic(int topicId, string userId);
    }
}
