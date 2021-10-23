using AlumniNetworkBackend.Models;
using AlumniNetworkBackend.Models.Domain;
using AlumniNetworkBackend.Models.DTO.PostDTO;
using Microsoft.AspNetCore.Mvc;
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
             var resultPost = _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            if (post.ReplyParentId != null)
            {
                var resultFromParent = await AddReplyParentList(resultPost.Entity);
                if (resultFromParent == false)
                    return null;
            }
            return post;
        }
        public async Task<bool> AddReplyParentList(Post post)
        {
            if (post == null)
                return false;
            Post prevPost = await _context.Posts.Where(p => p.Id == post.ReplyParentId).FirstAsync();

            List<Post> listOfPosts = (List<Post>)prevPost.TargetPosts;
            listOfPosts.Add(post);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Post> PostUpdateAsync(int id, Post post)
        {
            Post existingPost = await _context.Posts.FindAsync(id);
            if (post.Text != null)
                existingPost.Text = post.Text;

            await _context.SaveChangesAsync();
            return existingPost;
        }

        public bool PostExists(int id)
        {
            return _context.Posts.Any(p => p.Id == id);
        }


    }
}
