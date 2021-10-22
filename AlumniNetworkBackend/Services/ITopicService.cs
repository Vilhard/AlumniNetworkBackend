using AlumniNetworkBackend.Models.Domain;
using AlumniNetworkBackend.Models.DTO.TopicDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Services
{
    public interface ITopicService
    {
        public Task<Topic> Create(Topic newTopic, User user);
        public Task<Topic> UpdateUsers(Topic topic, User user);
    }
}
