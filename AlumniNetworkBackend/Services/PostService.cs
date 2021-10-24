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
        /// <summary>
        /// Service handles adding post to context and and checking if it is a reply to specific post
        /// in which case it is passed to helper service.
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Service handles finding previously added post from context and ...
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Service handles updating parameter defined post's text field
        /// </summary>
        /// <param name="id"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task<Post> PostUpdateAsync(int id, Post post)
        {
            Post existingPost = await _context.Posts.FindAsync(id);
            if (post.Text != null)
                existingPost.Text = post.Text;

            await _context.SaveChangesAsync();
            return existingPost;
        }
        /// <summary>
        /// Service checks if post exists in the context
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool PostExists(int id)
        {
            return _context.Posts.Any(p => p.Id == id);
        }
    }
}
