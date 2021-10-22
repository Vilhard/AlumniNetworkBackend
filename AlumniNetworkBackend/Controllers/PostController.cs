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

        // GET: api/Posts
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<PostReadTopicGroupDTO>>> GeUserGroupAndTopicPosts()
        {
            string userId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value; // will give the user's userId

            var userTopicPosts = await _context.Posts
                .Include(t => t.TargetTopic).ThenInclude(u => u.Users)
            .Where(u => u.TargetTopic.Users.Any(x => x.Id == userId)).ToListAsync();

            var userGroupPosts = await _context.Posts
                .Include(t => t.TargetGroup).ThenInclude(u => u.Members)
            .Where(u => u.TargetGroup.Members.Any(x => x.Id == userId)).ToListAsync();


            var combinedList = userTopicPosts.Concat(userGroupPosts).OrderByDescending(x => x.TimeStamp).ToList();

            return _mapper.Map<List<PostReadTopicGroupDTO>>(combinedList);
        }
        // GET: api/Posts/user
        [HttpGet("/user")]
        public async Task<ActionResult<PostReadDirectDTO>> GetUserDirectPost()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var userPosts = await _context.Users.Where(u => u.Id == userId)
                .SelectMany(p => p.Posts)
                .Where(t => t.TargetUser.Id == userId)
                .ToListAsync();
            if (userPosts == null)
            {
                return NotFound();
            }

            return _mapper.Map<PostReadDirectDTO>(userPosts);
        }

        // GET: api/Posts/user/user_id
        [HttpGet("user/{id}")]
        public async Task<ActionResult<PostReadDirectDTO>> GetSpecificDirectPostsFromUser(string id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var postsFromSpecificUser = await _context.Users.Where(u => u.Id == userId)
                .SelectMany(p => p.Posts)
                .Where(t => t.SenderId == id)
                .ToListAsync();

            if (postsFromSpecificUser == null)
            {
                return NotFound();
            }

            return _mapper.Map<PostReadDirectDTO>(postsFromSpecificUser);
        }

        // GET: api/Posts/Group/:group_id
        [HttpGet("Group/{id}")]
        public async Task<ActionResult<PostReadDirectDTO>> GetSpecificDirectPostsFromGroup(int id)
        {
            var postFromGroupAsTarget = await _context.Groups.Where(g => g.Id == id)
                .SelectMany(p => p.Posts)
                .Where(t => t.TargetGroup.Id == id)
                .ToListAsync();

            if (postFromGroupAsTarget == null)
            {
                return NotFound();
            }

            return _mapper.Map<PostReadDirectDTO>(postFromGroupAsTarget);
        }
        // GET: api/Posts/Topic/:topic_id
        [HttpGet("Topic/{id}")]
        public async Task<ActionResult<PostReadDirectDTO>> GetSpecificDirectPostsFromTopic(int id)
        {
            var postFromTopicAsTarget = await _context.Topics.Where(t => t.Id == id)
                .SelectMany(p => p.Posts)
                .Where(tt => tt.TargetTopic.Id == id)
                .ToListAsync();

            if (postFromTopicAsTarget == null)
            {
                return NotFound();
            }

            return _mapper.Map<PostReadDirectDTO>(postFromTopicAsTarget);
        }
        // GET: api/Posts/Event/:event_id
        [HttpGet("Event/{id}")]
        public async Task<ActionResult<PostReadDirectDTO>> GetSpecificDirectPostsFromEvent(int id)
        {
            var postFromEventAsTarget = await _context.Events.Where(e => e.Id == id)
                .SelectMany(p => p.Posts)
                .Where(te => te.TargetEvent.Id == id)
                .ToListAsync();

            if (postFromEventAsTarget == null)
            {
                return NotFound();
            }

            return _mapper.Map<PostReadDirectDTO>(postFromEventAsTarget);
        }

        // PUT: api/Posts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_postService.PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
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