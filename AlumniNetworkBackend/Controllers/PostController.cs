using System.Collections.Generic;
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

namespace AlumniNetworkBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        // GET: api/Post
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
        // GET: api/Post/user
        [HttpGet("User")]
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

        // GET: api/Posts/user/user_id
        [HttpGet("User/{id}")]
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

        // GET: api/Posts/Group/:group_id
        [HttpGet("Group/{id}")]
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
        // GET: api/Posts/Topic/:topic_id
        [HttpGet("Topic/{id}")]
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
        // GET: api/Posts/Event/:event_id
        [HttpGet("Event/{id}")]
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

        // PUT: api/Post/5
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

        // POST: api/Posts
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