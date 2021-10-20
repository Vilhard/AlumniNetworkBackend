using AlumniNetworkBackend.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Models.DTO.PostDTO
{
    public class PostReadDTO
    {
        public List<Post> GroupPosts { get; set; }
        public List<Post> TopicPosts { get; set; }
    }
}
