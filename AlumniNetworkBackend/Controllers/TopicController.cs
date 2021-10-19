using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlumniNetworkBackend.Models;
using AlumniNetworkBackend.Models.Domain;
using AutoMapper;
using AlumniNetworkBackend.Models.DTO.TopicDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using AlumniNetworkBackend.Models.DTO.UserDTO;

namespace AlumniNetworkBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly AlumniNetworkDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TopicController(AlumniNetworkDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor )
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Endpoint api/Topics which returns a list of topics and their
        /// descriptions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<TopicReadDTO>>> GetTopics()
        {
            return _mapper.Map<List<TopicReadDTO>>(await _context.Topics.ToListAsync());
        }

        /// <summary>
        /// Endpoint api/Topics/{id} which returns a specific topic
        /// and its description
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<TopicReadDTO>> GetTopic(int id)
        {
            Topic topic = await _context.Topics.FindAsync(id);

            if (topic == null)
            {
                return NotFound();
            }

            return _mapper.Map<TopicReadDTO>(topic);
        }

        // PUT: api/Topics/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PutTopic(int id, Topic topic)
        {
            if (id != topic.Id)
            {
                return BadRequest();
            }

            _context.Entry(topic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TopicExists(id))
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

        // <summary>
        /// Endpoint api/Topics which posts a new Topic to database with name
        /// and description.
        /// </summary>
        /// <param name="dtoTopic"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<TopicCreateDTO>> PostTopic(TopicCreateDTO dtoTopic)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var creator = await _context.Users.Where(u => u.Id == userId).Select(n => new UserTestDTO
            {
                Id = n.Id,
                Username = n.Username
            }).FirstOrDefaultAsync();
            User domainUser = _mapper.Map<User>(creator);
            Topic domainTopic = _mapper.Map<Topic>(dtoTopic);
            domainTopic.Users.Add(domainUser);
            _context.Topics.Add(domainTopic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTopic", new { id = domainTopic.Id }, _mapper.Map<TopicCreateDTO>(domainTopic));
        }

        // POST: api/Topics
        [HttpPost("{id}/join")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<TopicCreateMemberDTO>> PostTopicMember([FromRoute] int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

            if (!TopicExists(id))
            {
                return new StatusCodeResult(404);
            }
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return new StatusCodeResult(404);
            }
  
            var updatedTopicUsers = _context.Topics.Where(t => t.Id == id)
                .SelectMany(u => u.Users)
                .Append(user);
            await _context.SaveChangesAsync();
            

            return Ok(updatedTopicUsers);
        }
        private bool TopicExists(int id)
        {
            return _context.Topics.Any(e => e.Id == id);
        }
    }
}
