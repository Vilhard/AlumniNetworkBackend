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
        public Task PostUpdateAsync(Post post);
        public bool PostExists(int id);
    }
}
