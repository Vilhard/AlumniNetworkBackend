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
        public async Task<ActionResult<PostReadDTO>> GeUserGroupAndTopicPosts()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var userIsGroupMember = await _context.Groups.Where(g => g.Members.Any(u => u.Id == userId)).ToListAsync();
            List<Post> GroupPosts = userIsGroupMember.SelectMany(p => p.Posts).ToList();

            var userIsTopicMember = await _context.Topics.Where(t => t.Users.Any(u => u.Id == userId)).ToListAsync();
            List<Post> TopicPosts = userIsTopicMember.SelectMany(p => p.Posts).ToList();

            var filteredPostList = new
            {
                GroupPosts,
                TopicPosts
            };

            return _mapper.Map<PostReadDTO>(filteredPostList);
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
        public async Task<ActionResult<PostCreateDTO>> PostPost(PostCreateDTO dtoPost)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            Post domainPost = _mapper.Map<Post>(dtoPost);
            await _postService.AddPostAsync(domainPost);
            //if (dtoPost.TargetGroup != null)
            //{
            //    bool canPost = _context.Groups.Where(g => g.Members.Any(u => u.Id == userId)).Equals(true);
            //    if (canPost)
            //    {
            //       await _postService.AddPostAsync(domainPost);
            //    }
            //    else
            //    {
            //        return new StatusCodeResult(403);
            //    }
            return CreatedAtAction("GetPost", new { id = domainPost.Id }, _mapper.Map<PostReadCreateDTO>(domainPost));

        }
    }
}
