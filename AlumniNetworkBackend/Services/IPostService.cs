using AlumniNetworkBackend.Models.Domain;
using AlumniNetworkBackend.Models.DTO.PostDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Services
{
    public interface IPostService
    {
        public Task<Post> AddPostAsync(Post post);
        public Task<Post> PostUpdateAsync(int id, Post post);
        public Task<List<Post>> GetRepliesAsync(int id);
        public bool PostExists(int id);
    }
}
