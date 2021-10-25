﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlumniNetworkBackend.Models;
using AlumniNetworkBackend.Models.Domain;
using AutoMapper;
using System.Security.Claims;
using AlumniNetworkBackend.Models.DTO.PostDTO;
using AlumniNetworkBackend.Services;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net.Mime;

namespace AlumniNetworkBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class PostController : ControllerBase
    {
        private readonly AlumniNetworkDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPostService _postService;

        public PostController(AlumniNetworkDbContext context, IMapper mapper, IPostService postService)
        {
            _context = context;
            _mapper = mapper;
            _postService = postService;
        }
        /// <summary>
        /// Api endpoint GET: api/post returns all posts to topics and groups which the requesting user is a member of
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<PostReadTopicGroupDTO>>> GeUserGroupAndTopicPosts()
        {
            string userId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value; // will give the user's userId

            var userTopicPosts = await _context.Posts
                .Include(t => t.TargetTopic)
                .ThenInclude(u => u.Users)
                .Where(u => u.TargetTopic.Users.Any(x => x.Id == userId)).ToListAsync();

            var userGroupPosts = await _context.Posts
                .Include(t => t.TargetGroup)
                .ThenInclude(u => u.Members)
                .Where(u => u.TargetGroup.Members.Any(x => x.Id == userId)).ToListAsync();

            var combinedList = userTopicPosts.Concat(userGroupPosts).OrderByDescending(x => x.TimeStamp).ToList();

            return _mapper.Map<List<PostReadTopicGroupDTO>>(combinedList);
        }
        /// <summary>
        /// Api endpoint GET: api/post/user returns all direct posts that were send to the requesting user
        /// </summary>
        /// <returns></returns>
        [HttpGet("user")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<PostReadDTO>>> GetUserDirectPosts()
        {
            string userId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value; // will give the user's userId

            var userDirectMessages = await _context.Posts
                .Include(u => u.TargetUser)
                .ThenInclude(x => x.Posts)
                .Where(u => u.TargetUserId == userId).OrderByDescending(x => x.TimeStamp).ToListAsync();

            if (userDirectMessages == null)
            {
                return NotFound();
            }

            return _mapper.Map<List<PostReadDTO>>(userDirectMessages);
        }
        /// <summary>
        /// Api endpoint GET: api/posts/user/:user_id returns all direct posts from specific user that were send to the requesting user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("user/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<PostReadDTO>>> GetSpecificDirectPostsFromUser(string id)
        {
           if (id == null)
            {
                return NotFound();
            }
            string userId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value; // will give the user's userId

            var postsFromSpecificUser = await _context.Posts
                .Include(u => u.TargetUser)
                .ThenInclude(x => x.Posts)
                .ThenInclude(s => s.Sender)
                .Where(u => u.TargetUserId == userId)
                .Where(t => t.SenderId == id)
                .OrderByDescending(x => x.TimeStamp).ToListAsync();

            return _mapper.Map<List<PostReadDTO>>(postsFromSpecificUser);
        }
        /// <summary>
        /// Api endpoint api/post/group/:group_id returns a list of posts that were sent with the described :group_id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("group/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<PostReadTopicGroupDTO>>> GetSpecificPostsFromGroup(int id)
        {
            var postsFromGroup = await _context.Posts
                .Include(t => t.TargetGroup)
                .ThenInclude(p => p.Posts)
                .Where(u => u.TargetGroupId == id)
                .OrderByDescending(x => x.TimeStamp).ToListAsync();

            if (postsFromGroup == null)
            {
                return NotFound();
            }

            return _mapper.Map<List<PostReadTopicGroupDTO>>(postsFromGroup);
        }
        /// <summary>
        /// Api endpoint api/post/topic/:topic_id returns a list of posts that were sent with the described :topic_id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Posts/Topic/:topic_id
        [HttpGet("topic/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<PostReadTopicDTO>>> GetSpecificPostsFromTopic(int id)
        {
            var postsFromTopic = await _context.Posts
                .Include(t => t.TargetTopic)
                .ThenInclude(p => p.Posts)
                .Where(u => u.TargetTopicId == id)
                .OrderByDescending(x => x.TimeStamp).ToListAsync();

            if (postsFromTopic == null)
            {
                return NotFound();
            }

            return _mapper.Map<List<PostReadTopicDTO>>(postsFromTopic);
        }
        /// <summary>
        ///  Api endpoint api/post/event/:event_id returns a list of posts that were sent with the described :event_id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("event/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<PostReadEventDTO>>> GetSpecificDirectPostsFromEvent(int id)
        {
            var postsFromEvent = await _context.Posts
                .Include(t => t.TargetEvent)
                .ThenInclude(p => p.Posts)
                .Where(u => u.TargetEventId == id)
                .OrderByDescending(x => x.TimeStamp).ToListAsync();

            if (postsFromEvent == null)
            {
                return NotFound();
            }

            return _mapper.Map<List<PostReadEventDTO>>(postsFromEvent);
        }

        /// <summary>
        /// Updates an existing post.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dtoPost"></param>
        /// <returns></returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<PostReadDTO>> PutPost(int id, PostUpdateDTO dtoPost)
        {
            if (id != dtoPost.Id)
            {
                return BadRequest();
            }
            if (dtoPost.TargetEvent != null || dtoPost.TargetGroup != null || dtoPost.TargetTopic !=null 
                || dtoPost.ReplyParentId != null || dtoPost.TargetUser != null|| dtoPost.TargetPost != null)
            {
                return new StatusCodeResult(403);
            }
            if (!_postService.PostExists(id))
            {
                return NotFound();
            }

            Post domainPost = _mapper.Map<Post>(dtoPost);
           var updatePost = await _postService.PostUpdateAsync(id,domainPost);

            return _mapper.Map<PostReadDTO>(updatePost);
        }

        /// <summary>
        /// Creates a new post. Posts can be only posted to members that the user is member of or as a direct message to other users
        /// </summary>
        /// <param name="dtoPost"></param>
        /// <returns></returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<PostReadDTO>> PostPost(PostCreateDTO dtoPost)
        {
            string userId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value; // will give the user's userId
            bool isMember = dtoPost.Members.Any(u => u.Id == userId);
            
           if (isMember)
            {
                    Post post = new()
                    {
                        SenderId = userId,
                        Text = dtoPost.Text,
                        TargetEventId = dtoPost?.TargetEvent,
                        TargetGroupId = dtoPost?.TargetGroup,
                        TargetTopicId = dtoPost?.TargetTopic,
                        ReplyParentId = dtoPost.ReplyParentId,
                        TargetUserId = dtoPost?.TargetUser,
                        TimeStamp = DateTime.Now
                    };
                    var result = await _postService.AddPostAsync(post);
                    if (result == null)
                        return BadRequest();
                return _mapper.Map<PostReadDTO>(result);
            } else 
            {
            return new StatusCodeResult(403);
            }
        }
    } 
}