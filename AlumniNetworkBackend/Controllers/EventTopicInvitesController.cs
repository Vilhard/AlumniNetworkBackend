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
    public class EventTopicInvitesController : ControllerBase
    {
        private readonly AlumniNetworkDbContext _context;

        public EventTopicInvitesController(AlumniNetworkDbContext context)
        {
            _context = context;
        }

        // GET: api/EventTopicInvites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventTopicInvite>>> GetEventTopicInvite()
        {
            return await _context.EventTopicInvite.ToListAsync();
        }

        // GET: api/EventTopicInvites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventTopicInvite>> GetEventTopicInvite(int id)
        {
            var eventTopicInvite = await _context.EventTopicInvite.FindAsync(id);

            if (eventTopicInvite == null)
            {
                return NotFound();
            }

            return eventTopicInvite;
        }

        // PUT: api/EventTopicInvites/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventTopicInvite(int id, EventTopicInvite eventTopicInvite)
        {
            if (id != eventTopicInvite.Id)
            {
                return BadRequest();
            }

            _context.Entry(eventTopicInvite).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventTopicInviteExists(id))
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

        // POST: api/EventTopicInvites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EventTopicInvite>> PostEventTopicInvite(EventTopicInvite eventTopicInvite)
        {
            _context.EventTopicInvite.Add(eventTopicInvite);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEventTopicInvite", new { id = eventTopicInvite.Id }, eventTopicInvite);
        }

        // DELETE: api/EventTopicInvites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventTopicInvite(int id)
        {
            var eventTopicInvite = await _context.EventTopicInvite.FindAsync(id);
            if (eventTopicInvite == null)
            {
                return NotFound();
            }

            _context.EventTopicInvite.Remove(eventTopicInvite);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventTopicInviteExists(int id)
        {
            return _context.EventTopicInvite.Any(e => e.Id == id);
        }
    }
}
