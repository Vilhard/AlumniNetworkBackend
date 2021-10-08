using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlumniNetworkBackend.Models;
using AlumniNetworkBackend.Models.Domain;

namespace AlumniNetworkBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicMembersController : ControllerBase
    {
        private readonly AlumniNetworkDbContext _context;

        public TopicMembersController(AlumniNetworkDbContext context)
        {
            _context = context;
        }

        // GET: api/TopicMembers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TopicMember>>> GetTopicMember()
        {
            return await _context.TopicMember.ToListAsync();
        }

        // GET: api/TopicMembers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TopicMember>> GetTopicMember(int id)
        {
            var topicMember = await _context.TopicMember.FindAsync(id);

            if (topicMember == null)
            {
                return NotFound();
            }

            return topicMember;
        }

        // PUT: api/TopicMembers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTopicMember(int id, TopicMember topicMember)
        {
            if (id != topicMember.Id)
            {
                return BadRequest();
            }

            _context.Entry(topicMember).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TopicMemberExists(id))
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

        // POST: api/TopicMembers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TopicMember>> PostTopicMember(TopicMember topicMember)
        {
            _context.TopicMember.Add(topicMember);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTopicMember", new { id = topicMember.Id }, topicMember);
        }

        // DELETE: api/TopicMembers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTopicMember(int id)
        {
            var topicMember = await _context.TopicMember.FindAsync(id);
            if (topicMember == null)
            {
                return NotFound();
            }

            _context.TopicMember.Remove(topicMember);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TopicMemberExists(int id)
        {
            return _context.TopicMember.Any(e => e.Id == id);
        }
    }
}
