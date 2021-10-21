using AlumniNetworkBackend.Models;
using AlumniNetworkBackend.Models.Domain;
using AlumniNetworkBackend.Models.DTO.PostDTO;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Post> AddPostToGroup(PostCreateDTO dtoPost, string userId, int targetId )
        {
            Post post = new()
            {
                SenderId = userId,
                TargetGroupId = dtoPost.TargetGroup,
                TimeStamp = DateTime.Now
            };

            var returnValue = _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            Group group = await _context.Groups
                .Include(p => p.Posts)
                .Where(g => g.Id == targetId)
                .FirstAsync();

            if (group == null)
                return null;

            List<Post> posts = group.Posts.ToList();
            posts.Add(returnValue.Entity);
            group.Posts = posts;
            return returnValue.Entity;
        }
        
        public bool PostExists(int id)
        {
            return _context.Posts.Any(p => p.Id == id);
        }

    }
}
