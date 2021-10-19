using AlumniNetworkBackend.Models;
using AlumniNetworkBackend.Models.Domain;
using AlumniNetworkBackend.Models.DTO.PostDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Services
{
    public class PostService : IPostService
    {
        private readonly AlumniNetworkDbContext _context;
        public PostService(AlumniNetworkDbContext context)
        {
            _context = context;
        }
        public async Task<Post> AddPostAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public Task findPostTarget(PostCreateDTO post)
        {
            if (post.TargetEvent != null)
            {

            } else if (post.TargetGroup != null)
            {
                
            } else if (post.TargetTopic != null)
            {

            } else if (post.TargetUser != null)
            {

            }

            throw new NotImplementedException();
        }

        public bool PostExists(int id)
        {
            return _context.Posts.Any(p => p.Id == id);
        }
    }
}
