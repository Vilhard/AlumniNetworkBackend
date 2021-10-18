using AlumniNetworkBackend.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Models.DTO.TopicDTO
{
    public class TopicReadPostDTO
    {
        public List<Post> Posts { get; set; }
    }
}
